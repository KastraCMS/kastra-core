using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.Extensions.Options;

namespace Kastra.Core.Modules.Configuration
{
    internal class ModuleViewEngineOptionsSetup : IConfigureOptions<ModuleViewEngineOptions>
    {
        public void Configure(ModuleViewEngineOptions options)
        {
            options.ModuleViewLocationFormats.Add($"/Modules/{{0}}{ModuleViewEngine.ModuleViewExtension}");

            options.ModuleViewLocationExpanders.Add(new PageViewLocationExpander());
        }
    }
}
