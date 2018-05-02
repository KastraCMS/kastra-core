/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Reflection;
using System.Threading.Tasks;
using Kastra.Core.Attributes;
using Kastra.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Collections.Generic;
using System.Linq;
using Kastra.Core.Dto;

namespace Kastra.Core.ViewComponents
{
    public class ModuleViewComponent: ViewComponent
    {
        #region Properties
        
        public ModuleInfo Module { get { return _module; } }
        
        public PageInfo Page { get { return _page; } }

        public string Action { get { return _action; } }

        public CacheEngine CacheEngine { get { return _cacheEngine; } }


        
        #endregion
        
        #region Private members
        
        private ModuleInfo _module = null;
        private PageInfo _page = null;
        private string _action = null;
        private CacheEngine _cacheEngine = null;
        private IEnumerable<PermissionInfo> _requiredClaims { get; set; }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(ModuleDataComponent data)
        {
            _module = data.Module;
            _page = data.Page;
            _action = data.ModuleAction;
            _cacheEngine = data.CacheEngine;
            _requiredClaims = data.RequiredClaims;

            // Check module rights
            if (!HasRequiredClaim() || !ValidViewComponentRights())
            {
                return Content(String.Empty);
            }

            if(HttpContext.Request.QueryString.HasValue)
            {
                LoadQueryString();
            }

            switch(data.ModuleAction)
            {
                case Constants.ModuleActions.Add:
                    return OnViewComponentLoad();
                case Constants.ModuleActions.Update:
                    return OnViewComponentUpdate();
                case Constants.ModuleActions.Delete:
                    return OnViewComponentDelete();
                default:
                    return OnViewComponentLoad();
            }
        }


        public ViewViewComponentResult ModuleView()
        {
            string viewPath = String.Format("{0}{1}/Views/{2}.cshtml",
                                            Constants.SiteConfig.DefaultModulesPath, _module.ModuleDefinition.Path, Constants.SiteConfig.DefaultModuleViewName);
            
            return View(viewPath);
        }
        
        public ViewViewComponentResult ModuleView(string viewName)
        {
            string viewPath = String.Format("{0}{1}/Views/{2}.cshtml",
                                            Constants.SiteConfig.DefaultModulesPath, _module.ModuleDefinition.Path, viewName);
            
            return View(viewPath);
        }
        
        public ViewViewComponentResult ModuleView<TModel> (string viewName, TModel model)
        {
            string viewPath = null;
            
            if(String.IsNullOrEmpty(viewPath))
            {
                viewPath = Constants.SiteConfig.DefaultModuleViewName;
            }
            
            viewPath = String.Format("{0}{1}/Views/{2}.cshtml", Constants.SiteConfig.DefaultModulesPath, _module.ModuleDefinition.Path, viewName);
            
            return View(viewPath, model);
        }
        
        
        public virtual ViewViewComponentResult OnViewComponentLoad()
        {
            return ModuleView();
        }

        public virtual ViewViewComponentResult OnViewComponentUpdate()
        {
            return ModuleView();
        }

        public virtual ViewViewComponentResult OnViewComponentDelete()
        {
            return ModuleView();
        }

        /// <summary>
        /// Loads the query string data in model objects.
        /// </summary>
        private void LoadQueryString()
        {
            string parameterValue = String.Empty;
            object moduleComponent = (object)this;
            ParameterAttribute parameterAttribute = null;
            PropertyInfo[] properties = null;
            string moduleParametersKey = String.Format(Constants.ModuleConfig.ModuleViewComponentParameters, moduleComponent.GetType().FullName);

            if(!_cacheEngine.GetCacheObject(moduleParametersKey, out properties))
                properties = _cacheEngine.SetCacheObject(moduleParametersKey, moduleComponent.GetType().GetProperties());

            if(properties == null)
            {
                return;
            }
                
            // Select all properties which have parameterattributes
            foreach(PropertyInfo property in properties)
            {
                if(!property.CanRead)
                {
                    continue;
                }

                parameterAttribute = property.GetCustomAttribute(typeof(ParameterAttribute), false) as ParameterAttribute;

                if(parameterAttribute == null)
                {
                    continue;
                }

                parameterValue = HttpContext.Request.Query[parameterAttribute.Name];

                if(String.IsNullOrEmpty(parameterValue))
                {
                    continue;
                }

                property.SetValue((object) this, Convert.ChangeType(parameterValue,property.PropertyType), (object[]) null);
            }
        }

        /// <summary>
        /// Hase the required claim.
        /// </summary>
        /// <returns><c>true</c>, if required claim was hased, <c>false</c> otherwise.</returns>
        public Boolean HasRequiredClaim()
        {
            Int32 requiredPermissionId = 0;

            if (_requiredClaims == null)
            {
                return false;
            }

            foreach(PermissionInfo permission in _requiredClaims)
            {
				if (permission.Name == Constants.ModuleConfig.GrantedAccessPermission
                    || (UserClaimsPrincipal.HasClaim(v => v.Type == Constants.ModuleConfig.ModulePermissionType 
                                                        && Int32.TryParse(v.Value, out requiredPermissionId) 
                                                        && requiredPermissionId == permission.PermissionId)))
				{
					return true;
				}
            }

            return false;
        }

        /// <summary>
        /// Check if user can have access to the view component with his permissions.
        /// </summary>
        /// <returns><c>true</c>, if view component rights was valided, <c>false</c> otherwise.</returns>
        public bool ValidViewComponentRights()
        {
            string[] claims = null;
            string[] roles = null;
            IEnumerable<ViewComponentAuthorizeAttribute> attributes = this.GetType().GetCustomAttributes<ViewComponentAuthorizeAttribute>();

            if(attributes == null || !attributes.Any())
            {
                return true;
            }

            foreach(ViewComponentAuthorizeAttribute attribute in attributes)
            {
                // Get claims
                claims = attribute.GetClaims();

                if(claims != null)
                {
                    foreach(string claim in claims)
                    {
                        if (UserClaimsPrincipal.HasClaim(c => c.Value == claim))
                        {
                            return true;
                        } 
                    }
                }

                // Get roles
                if(roles != null)
                {
                    foreach(string role in roles)
                    {
                        if (User.IsInRole(role))
                        {
                            return true;
                        } 
                    }
                }
            }

            return false;
        }
    }
} 