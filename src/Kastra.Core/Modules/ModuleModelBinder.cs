/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.ComponentModel;
using System.Reflection;
using Kastra.Core.Modules.ViewComponents;
using Microsoft.AspNetCore.Http;

namespace Kastra.Core.Modules
{
    public class ModuleModelBinder
    {
        #region Model properties

        /// <summary>
        /// Module ID.
        /// </summary>
        private int _currentModuleId = 0;
        public int CurrentModuleId { get { return _currentModuleId; } } 

        public int Id { get; set; }

        #endregion

        public ModuleModelBinder(ModuleViewComponent moduleView)
        {
            LoadModuleInfo(moduleView);

            if(!moduleView.HttpContext.Request.HasFormContentType)
                return;
                
            LoadForm(moduleView.HttpContext.Request.Form);
        }

        /// <summary>
        /// Loads the form in model object.
        /// </summary>
        /// <param name="form">Form.</param>
        internal void LoadForm(IFormCollection form)
        {
            string obj = null; 
            TypeConverter typeConverter = null;

            if(form is null)
            {
                return;
            }

            foreach(PropertyInfo property in this.GetType().GetProperties())
            {
                obj = form[property.Name].ToString();

                if(String.IsNullOrEmpty(obj))
                {
                    continue;
                }
                
                typeConverter = TypeDescriptor.GetConverter(property.PropertyType);
                
                if(typeConverter is null)
                {
                    continue;
                }

                property.SetValue((object) this, typeConverter.ConvertFromString(obj), (object[]) null);
            }
        }

        /// <summary>
        /// Loads the module info.
        /// </summary>
        /// <param name="moduleView">Module view.</param>
        internal void LoadModuleInfo(ModuleViewComponent moduleView)
        {
            if(moduleView is null || moduleView.Module is null)
            {
                return;
            }

            _currentModuleId = moduleView.Module.ModuleId;
        }
    }
}