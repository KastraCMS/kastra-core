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
using System.Collections.Generic;
using System.Linq;
using Kastra.Core.DTO;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Kastra.Core.Constants;

namespace Kastra.Core.Modules.ViewComponents
{
    /// <summary>
    /// Allow to create a module.
    /// </summary>
    public class ModuleViewComponent: ViewComponent
    {
        #region Properties
        
        public string Action { get { return _action; } }
        
        public CacheEngine CacheEngine { get { return _cacheEngine; } }

        public ModuleInfo Module { get { return _module; } }

        public PageInfo Page { get { return _page; } }
        
        #endregion
        
        #region Private members
        
        private string _action;
        private CacheEngine _cacheEngine;
        private ModuleInfo _module;
        private PageInfo _page;

        private IEnumerable<PermissionInfo> _requiredClaims { get; set; }

        #endregion

        public async Task<IViewComponentResult> InvokeAsync(ModuleDataComponent data)
        {
            _action = data.ModuleAction;
            _cacheEngine = data.CacheEngine;
            _module = data.Module;
            _page = data.Page;
            _requiredClaims = data.RequiredClaims;

            // Check module rights
            if (!HasRequiredClaim() || !ValidViewComponentRights())
            {
                return Content(string.Empty);
            }

            if(HttpContext.Request.QueryString.HasValue)
            {
                LoadQueryString();
            }

            switch(data.ModuleAction)
            {
                case ModuleActions.Add:
                    return await OnViewComponentLoad();
                case ModuleActions.Update:
                    return await OnViewComponentUpdate();
                case ModuleActions.Delete:
                    return await OnViewComponentDelete();
                default:
                    return await OnViewComponentLoad();
            }
        }

        /// <summary>
        /// Returns a result which will render the default module view.
        /// </summary>
        /// <returns></returns>
        public ModuleViewComponentResult ModuleView()
        {
            return ModuleView(null, ViewData.Model);
        }
        
        /// <summary>
        /// Returns a result which will render the module view corresponding to a view name.
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ModuleViewComponentResult ModuleView(string viewName)
        {
            return ModuleView(viewName, ViewData.Model);
        }
        
        /// <summary>
        /// Returns a result which will render the module view filled by a model object. 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public ModuleViewComponentResult ModuleView<TModel> (string viewName, TModel model)
        {   
            if(string.IsNullOrEmpty(viewName))
            {
                viewName = SiteConfiguration.DefaultModuleViewName;
            }

            ViewDataDictionary viewData = new ViewDataDictionary<TModel>(ViewData, model);

            return new ModuleViewComponentResult
            {
                ModuleDefinitionPath = _module.ModuleDefinition.Path,
                ViewName = viewName,
                ViewData = viewData
            };
        }
        
        /// <summary>
        /// Returns a result which will render the default component view.
        /// </summary>
        /// <returns></returns>
        public virtual Task<ModuleViewComponentResult> OnViewComponentLoad()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a result which will render the module view for an update action.
        /// </summary>
        /// <returns></returns>
        public virtual Task<ModuleViewComponentResult> OnViewComponentUpdate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a result which will render the module view for a delete action.
        /// </summary>
        /// <returns></returns>
        public virtual Task<ModuleViewComponentResult> OnViewComponentDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Has the required claim.
        /// </summary>
        /// <returns><c>true</c>, if required claim was hased, <c>false</c> otherwise.</returns>
        public bool HasRequiredClaim()
        {
            int requiredPermissionId = 0;

            if (_requiredClaims == null)
            {
                return false;
            }

            foreach(PermissionInfo permission in _requiredClaims)
            {
				if (permission.Name == ModuleConfiguration.GrantedAccessPermission
                    || (UserClaimsPrincipal.HasClaim(v => v.Type == ModuleConfiguration.ModulePermissionType 
                                                        && int.TryParse(v.Value, out requiredPermissionId) 
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

            if(!attributes.Any())
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

        /// <summary>
        /// Loads the query string data in model objects.
        /// </summary>
        private void LoadQueryString()
        {
            string parameterValue;
            object moduleComponent = (object)this;
            ParameterAttribute parameterAttribute;
            PropertyInfo[] properties;

            string moduleParametersKey = string.Format(ModuleConfiguration.ModuleViewComponentParameters, moduleComponent.GetType().FullName);

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

                if(string.IsNullOrEmpty(parameterValue))
                {
                    continue;
                }

                property.SetValue((object) this, Convert.ChangeType(parameterValue,property.PropertyType), (object[]) null);
            }
        }
    }
} 