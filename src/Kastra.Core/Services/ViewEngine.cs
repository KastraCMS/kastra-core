/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Kastra.Core.Configuration;
using Kastra.Core.Constants;
using Kastra.Core.DTO;
using Kastra.Core.Modules;

namespace Kastra.Core.Services
{
    public sealed class ViewEngine
    {
        private readonly PageInfo _page;
        private readonly CacheEngine _cacheEngine;
        
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
            return CreateView(string.Empty, 0, string.Empty);
        }

        /// <summary>
        /// Creates the Kastra view by returning a model to fill a template.
        /// </summary>
        /// <returns>The view model.</returns>
        /// <param name="moduleControlKeyName">Module control key name.</param>
        /// <param name="moduleId">Module identifier.</param>
        /// <param name="moduleAction">Module action.</param>
        public object CreateView(string moduleControlKeyName, int moduleId, string moduleAction)
        {
            object model = null;
            PropertyInfo property = null;

            ModuleInfo module = null;
            ModuleControlInfo moduleControl = null;
            ModuleDataComponent moduleData = null;
            
            // Get template
            if(_page is null || _page.PageTemplate is null)
            {
                return null;
            }

            TemplateInfo template = _page.PageTemplate;

            // Get the template model in cache
            string templateCacheKey = string.Format(
                    TemplateConfiguration.TemplateModelTypeCacheKey, 
                    _page.PageId, 
                    moduleControlKeyName, 
                    moduleId, 
                    moduleAction
                );

            if (_cacheEngine.GetCacheObject(templateCacheKey, out model))
            {
                return model;
            }

            // Instanciate the template model
            Type type = KastraAssembliesContext.Instance.GetType(template.ModelClass);

            if (type is null)
            {
                throw new NullReferenceException($"Invalid template path for {template.ModelClass}");
            }

            model = Activator.CreateInstance(type);

            // Get places and complete the model with modules
            IList<PlaceInfo> places = template.Places.ToList();
            
            foreach(PlaceInfo place in places)
            {
                property = type.GetProperty(place.KeyName);
                
                // Get default module
                if(property is null || property.PropertyType != typeof(ModuleDataComponent))
                {
                    continue;
                }

                module = place.Modules?.SingleOrDefault(m => 
                        !m.IsDisabled 
                        && m.PlaceId == place.PlaceId 
                        && m.PageId == _page.PageId
                    );

                // If there is no module but if the place has a static module
                if (module is null && place.StaticModule is not null && !place.StaticModule.IsDisabled)
                {
                    module = place.StaticModule;
                }

                if(module is null)
                {
                    module = new ModuleInfo()
                    {
                        Place = place,
                        ModuleDefinition = new ModuleDefinitionInfo()
                    };
                }

                moduleData = new ModuleDataComponent()
                {
                    Module = module,
                    Page = _page,
                    ModuleAction = moduleAction,
                    CacheEngine = _cacheEngine
                };

                if(module.ModulePermissions is not null)
                {
                    moduleData.RequiredClaims = module.ModulePermissions
                        .Where(mp => mp.Permission != null)
                        .Select(mp => mp.Permission)
                        .ToList();
                }

                // Get module control
                if(!string.IsNullOrEmpty(moduleControlKeyName) && moduleId > 0 && module.ModuleId == moduleId)
                {
                    moduleControl = module.ModuleDefinition.ModuleControls
                        .SingleOrDefault(mc => mc.KeyName.ToLower() == moduleControlKeyName.ToLower());
                    
                    if(moduleControl is not null)
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

			if (type is not null)
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
        public ModuleDataComponent GetModuleDataByModuleId(
            PageInfo page, 
            ModuleInfo module, 
            string moduleControlKeyName, 
            string moduleAction)
        {
            ModuleControlInfo moduleControl = null;
            ModuleDataComponent moduleData = new ModuleDataComponent()
            {
                Module = module,
                Page = page,
                ModuleAction = moduleAction,
                CacheEngine = _cacheEngine
            };

			if (module.ModulePermissions is not null)
            {
                moduleData.RequiredClaims = module.ModulePermissions
                    .Where(mp => mp.Permission is not null)
                    .Select(mp => mp.Permission)
                    .ToList();
            }

            // Get module control
            if(!string.IsNullOrEmpty(moduleControlKeyName) && module != null)
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
        public static string GetModelFullname(string nameSpace,string name)
        {
            if (string.IsNullOrEmpty(nameSpace) || string.IsNullOrEmpty(name))
            {
                return ModuleConfiguration.DefaultModuleFullName;
            }

            return $"{nameSpace}.{name}";
        }
    }
}