/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Threading.Tasks;
using Kastra.Core.ViewComponents;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace Kastra.Core.Helpers
{
    public class ModuleViewComponentHelper : IModuleViewComponentHelper
    {
        public readonly IViewComponentHelper _viewComponentHelper;
        
        public ModuleViewComponentHelper(IViewComponentHelper viewComponentHelper)
        {
            _viewComponentHelper = viewComponentHelper;
        }

        public Task<IHtmlContent> InvokeAsync(ModuleDataComponent moduleData)
        {
			if (moduleData != null && !String.IsNullOrEmpty(moduleData.ModuleViewComponent))
			{
				return _viewComponentHelper.InvokeAsync(moduleData.ModuleViewComponent, new { data = moduleData });
			}

		    return Task.FromResult<IHtmlContent>(null);                        
        }
    }
}