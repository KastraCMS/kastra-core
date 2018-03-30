/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Threading.Tasks;
using Kastra.Core.ViewComponents;
using Microsoft.AspNetCore.Html;

namespace Kastra.Core.Helpers
{
    public interface IModuleViewComponentHelper
    {
        Task<IHtmlContent> InvokeAsync(ModuleDataComponent moduleData);
    }
}
