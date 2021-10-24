/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Kastra.Core.Services.Contracts;
using Kastra.Core.DTO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Kastra.Core.Modules
{
    public class ModuleBase : IDependencyRegister, IModuleRegister
    {
        public virtual void SetDependencyInjections(IServiceCollection services, IConfiguration configuration)
        {
            
        }

        public virtual void Install(IServiceProvider serviceProvider, IViewManager viewManager)
        {
            // Get module config json
            Assembly assembly = Assembly.GetCallingAssembly();
            string path = $"{Path.GetDirectoryName(assembly.Location)}/../moduleconfig.json";
            
            if(!File.Exists(path))
            {
                return;
            }

            string configJson = File.ReadAllText(path);
            dynamic dynamicObject = JsonConvert.DeserializeObject<dynamic>(configJson);

            if (dynamicObject.Modules == null)
            {
                return;
            }

            foreach(dynamic module in dynamicObject.Modules)
            {
                InstallModule(viewManager, module);
            }
        }

        public virtual void Uninstall()
        {
            
        }

        protected virtual Boolean IsNewOrUpdate(string oldVersion, string newVersion)
        {
            string[] nVersion = newVersion.Split('.');
            string[] oVersion = oldVersion.Split('.');

            if(nVersion.Length >= oVersion.Length)
            {
                for(int i = 0; i < oVersion.Length; i++)
                {
                    if(int.Parse(oVersion[i]) > int.Parse(nVersion[i]))
                    {
                        return false;
                    }
                }
            }
            else
            {
                for(int i = 0; i < nVersion.Length; i++)
                {
                    if(int.Parse(oVersion[i]) > int.Parse(nVersion[i]))
                    {
                        return false;
                    }
                }
            }

            if(oVersion.Length > nVersion.Length && int.Parse(oVersion[^1]) > 0)
            {
                return false;
            }

            return true;
        }

        #region Private methods

        private void InstallModule(IViewManager viewManager, dynamic module)
        {
            string keyName = module.Definition.SystemName;

            ModuleDefinitionInfo definitionInfo = viewManager.GetModuleDefsList().SingleOrDefault(md => md.KeyName == keyName);

            // Check module version
            if(definitionInfo != null && !IsNewOrUpdate(definitionInfo.Version, module.Definition.Version))
            {
                return;
            }

            if(definitionInfo == null)
            {
                definitionInfo = new ModuleDefinitionInfo();
            }

            definitionInfo.KeyName = keyName;
            definitionInfo.Name = module.Definition.DisplayName;
            definitionInfo.Namespace = module.Definition.Namespace;
            definitionInfo.Path = module.Definition.Path;
            definitionInfo.Version = module.Definition.Version;
        
            viewManager.SaveModuleDef(definitionInfo);

            definitionInfo = viewManager.GetModuleDefsList().SingleOrDefault(md => md.KeyName == keyName);
            
            if(definitionInfo.ModuleDefId == 0)
            {
                return;
            }

            // Get module controls
            string controlKeyName = null;

            foreach(dynamic control in module.Controls)
            {
                controlKeyName = control.KeyName.ToString();

                ModuleControlInfo cControl = viewManager.GetModuleControlsList(definitionInfo.ModuleDefId)
                                .SingleOrDefault(mc => mc.KeyName == controlKeyName);

                if(cControl is not null)
                {
                    cControl.Path = control.Path;

                    viewManager.SaveModuleControl(cControl);
                }
                else
                {
                    cControl = new ModuleControlInfo()
                    {
                        KeyName = control.KeyName,
                        Path = control.Path,
                        ModuleDefId = definitionInfo.ModuleDefId
                    };

                    viewManager.SaveModuleControl(cControl);
                }
            }

            // Get module navigations
            foreach (dynamic navigation in module.Navigations)
            {
                string navigationUrl = navigation.Url;
                string navigationType = navigation.Type;

                ModuleNavigationInfo moduleNavigation = viewManager.GetModuleNavigationList(definitionInfo.ModuleDefId)
                                .SingleOrDefault(n => n.Url == navigationUrl && n.Type == navigationType);

                if (moduleNavigation is not null)
                {
                    moduleNavigation.Name = navigation.Name;
                }
                else
                {
                    moduleNavigation = new ModuleNavigationInfo()
                    {
                        Name = navigation.Name,
                        Url = navigationUrl,
                        Type = navigationType,
                        Icon = navigation.Icon,
                        ModuleDefinitionId = definitionInfo.ModuleDefId
                    };
                }

                viewManager.SaveModuleNavigation(moduleNavigation);
            }
        }

        #endregion
    }
}