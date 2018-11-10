/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kastra.Core.Dto;
using Kastra.Core.ViewComponents;

namespace Kastra.Core.Services
{
    public sealed class ViewEngine
    {
        private readonly PageInfo _page = null;
        private readonly CacheEngine _cacheEngine = null;
        
        public ViewEngine(CacheEngine cacheEngine)
        {
            _cacheEngine = cacheEngine;
        }
        
        public ViewEngine(PageInfo page, CacheEngine cacheEngine)
        {
            _page = page;
            _cacheEngine = cacheEngine;
        }

        /// <summary>
        /// Creates an empty view.
        /// </summary>
        /// <returns>The view model.</returns>
        public object CreateView()
        {
            return CreateView(String.Empty, 0, String.Empty);
        }

        /// <summary>
        /// Creates the Kastra view by returning a model to fill a template.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <param name="moduleControlKeyName">Module control key name.</param>
        /// <param name="moduleId">Module identifier.</param>
        /// <param name="moduleAction">Module action.</param>
        public object CreateView(String moduleControlKeyName, Int32 moduleId, String moduleAction)
        {
            object model = null;
            Type type = null;
            PropertyInfo property = null;
            ModuleInfo module = null;
            ModuleControlInfo moduleControl = null;
            TemplateInfo template = null;
            ModuleDataComponent moduleData = null;
            string templateCacheKey = null;
            IList<PlaceInfo> places = null;
            
            // Get template
            if(_page == null || _page.PageTemplate == null)
            {
                return null;
            }
                
            template = _page.PageTemplate;

            templateCacheKey = String.Format(Constants.TemplateConfig.TemplateModelTypeCacheKey, _page.PageId, moduleControlKeyName, moduleId, moduleAction);

            // Instanciate the template model
            if (_cacheEngine.GetCacheObject(templateCacheKey, out model))
            {
                return model;
            }

            type = KastraAssembliesContext.Instance.GetType($"{template.Path}.{template.KeyName}ViewModel");

            if (type == null)
            {
                throw new NullReferenceException($"Invalid template path for {template.Path}.{template.KeyName}ViewModel");
            }

            model = Activator.CreateInstance(type);
                
            // Get places and complete the model with modules
            places = template.Places.ToList();
            
            foreach(PlaceInfo place in places)
            {
                property = type.GetProperty(place.KeyName);
                
                // Get default module
                if(property == null || property.PropertyType != typeof(ModuleDataComponent))
                {
                    continue;
                }

                // If place has a static module
                if(place.ModuleId != null)
                {
                    module = place.StaticModule;
                }
                else
                {
                    module = place.Modules.SingleOrDefault(m => m.PlaceId == place.PlaceId && m.PageId == _page.PageId);
                }

                if(module == null)
                {
                    module = new ModuleInfo();
                    module.Place = place;
                    module.ModuleDefinition = new ModuleDefinitionInfo();
                }

                moduleData = new ModuleDataComponent();
                moduleData.Module = module;
                moduleData.Page = _page;
                moduleData.ModuleAction = moduleAction;
                moduleData.CacheEngine = _cacheEngine;

                if(module.ModulePermissions != null)
                {
                    moduleData.RequiredClaims = module.ModulePermissions
                                                .Where(mp => mp.Permission != null)
                                                .Select(mp => mp.Permission).ToList();
                }
                    

                // Get module control
                if(!String.IsNullOrEmpty(moduleControlKeyName) && moduleId != 0 && module.ModuleId == moduleId)
                {
                    moduleControl = module.ModuleDefinition.ModuleControls
                                      .SingleOrDefault(mc => mc.KeyName.ToLower() == moduleControlKeyName.ToLower());
                    
                    if(moduleControl != null)
                    {
                        moduleData.ModuleViewComponent = GetModelFullname(module.ModuleDefinition.Namespace, moduleControl.Path);
                    }
                    else
                    {
                        moduleData.ModuleViewComponent = GetModelFullname(module.ModuleDefinition.Namespace, module.ModuleDefinition.KeyName);
                    }
                }
                else
                {
                    moduleData.ModuleViewComponent = GetModelFullname(module.ModuleDefinition.Namespace, module.ModuleDefinition.KeyName);
                }
                    
                // Set the place to find module
                property.SetValue(model, moduleData);
            }

			if (type != null)
            {
                _cacheEngine.SetCacheObject(templateCacheKey, model);
            }
            
            return model;
        }

        /// <summary>
        /// Gets the module data by module identifier.
        /// </summary>
        /// <returns>The module data by module identifier.</returns>
        /// <param name="page">Page.</param>
        /// <param name="module">Module.</param>
        /// <param name="moduleControlKeyName">Module control key name.</param>
        /// <param name="moduleAction">Module action.</param>
        public ModuleDataComponent GetModuleDataByModuleId(PageInfo page, ModuleInfo module, String moduleControlKeyName, String moduleAction)
        {
            ModuleControlInfo moduleControl = null;
            ModuleDataComponent moduleData = new ModuleDataComponent();
            moduleData.Module = module;
            moduleData.Page = page;
            moduleData.ModuleAction = moduleAction;
            moduleData.CacheEngine = _cacheEngine;

			if (module.ModulePermissions != null)
            {
                moduleData.RequiredClaims = module.ModulePermissions
                                            .Where(mp => mp.Permission != null)
                                            .Select(mp => mp.Permission).ToList();
            }

            // Get module control
            if(!String.IsNullOrEmpty(moduleControlKeyName) && module != null)
            {
                moduleControl = module.ModuleDefinition.ModuleControls
                                        .SingleOrDefault(mc => mc.KeyName.ToLower() == moduleControlKeyName.ToLower());
                
                if(moduleControl != null)
                {
                    moduleData.ModuleViewComponent = GetModelFullname(module.ModuleDefinition.Namespace, moduleControl.Path);
                }
                else
                {
                    moduleData.ModuleViewComponent = GetModelFullname(module.ModuleDefinition.Namespace, module.ModuleDefinition.KeyName);
                }
            }
            else
            {
                moduleData.ModuleViewComponent = GetModelFullname(module.ModuleDefinition.Namespace, module.ModuleDefinition.KeyName);
            }

            return moduleData;
        }

        /// <summary>
        /// Gets the module fullname for the view component.
        /// </summary>
        /// <returns>The model fullname.</returns>
        /// <param name="nameSpace">Namespace.</param>
        /// <param name="name">Name.</param>
        public static String GetModelFullname(String nameSpace, String name)
        {
            if (String.IsNullOrEmpty(nameSpace) || String.IsNullOrEmpty(name))
            {
                return Constants.ModuleConfig.DefaultModuleFullName;
            }

            return $"{nameSpace}.{name}";
        }
    }
}