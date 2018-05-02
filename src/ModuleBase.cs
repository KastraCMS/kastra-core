/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Kastra.Core.Business;
using Kastra.Core.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Kastra.Core
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
            String path = $"{Path.GetDirectoryName(assembly.Location)}/../moduleconfig.json";
            
            if(!File.Exists(path))
            {
                return;
            }

            String configJson = File.ReadAllText(path);
            dynamic dynamicObject = JsonConvert.DeserializeObject<dynamic>(configJson);

            if(dynamicObject.Modules == null)
                return;

            foreach(dynamic module in dynamicObject.Modules)
            {
                InstallModule(serviceProvider, viewManager, module);
            }
        }

        public virtual void Uninstall()
        {
            
        }

        protected virtual Boolean IsNewOrUpdate(string oldVersion, string newVersion)
        {
            String[] nVersion = newVersion.Split('.');
            String[] oVersion = oldVersion.Split('.');

            if(nVersion.Length >= oVersion.Length)
            {
                for(int i = 0; i < oVersion.Length; i++)
                {
                    if(Int32.Parse(oVersion[i]) > Int32.Parse(nVersion[i]))
                        return false;
                }
            }
            else
            {
                for(int i = 0; i < nVersion.Length; i++)
                {
                    if(Int32.Parse(oVersion[i]) > Int32.Parse(nVersion[i]))
                        return false;
                }
            }

            if(oVersion.Length > nVersion.Length && Int32.Parse(oVersion[oVersion.Length-1]) > 0)
                return false;

            return true;
        }

        #region Private methods

        private void InstallModule(IServiceProvider serviceProvider, IViewManager viewManager, dynamic module)
        {
            try
            {
                
                String keyName = module.Definition.SystemName;

                ModuleDefinitionInfo definitionInfo = viewManager.GetModuleDefsList().SingleOrDefault(md => md.KeyName == keyName);

                // Check module version
                if(definitionInfo != null && !IsNewOrUpdate(definitionInfo.Version, module.Definition.Version))
                    return;

                if(definitionInfo == null)
                    definitionInfo = new ModuleDefinitionInfo();

                definitionInfo.KeyName = keyName;
                definitionInfo.Name = module.Definition.DisplayName;
                definitionInfo.Namespace = module.Definition.Namespace;
                definitionInfo.Path = module.Definition.Path;
                definitionInfo.Version = module.Definition.Version;
           
                viewManager.SaveModuleDef(definitionInfo);

                definitionInfo = viewManager.GetModuleDefsList().SingleOrDefault(md => md.KeyName == keyName);
                
                if(definitionInfo.ModuleDefId == 0)
                    return;

                // Get module controls
                ModuleControlInfo cControl = null;

                foreach(dynamic control in module.Controls)
                {
                    cControl = viewManager.GetModuleControlsList(definitionInfo.ModuleDefId)
                                    .SingleOrDefault(mc => mc.KeyName == control.KeyName);

                    if(cControl != null)
                    {
                        cControl.Path = control.Path;
                        viewManager.SaveModuleControl(cControl);
                    }
                    else
                    {
                        cControl = new ModuleControlInfo();
                        cControl.KeyName = control.KeyName;
                        cControl.Path = control.Path;
                        cControl.ModuleDefId = definitionInfo.ModuleDefId;

                        viewManager.SaveModuleControl(cControl);
                    }
                }
            }
            catch(Exception ex)
            {
                // Logs
            }
        }

        #endregion
    }
}