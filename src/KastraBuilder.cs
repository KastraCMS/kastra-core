/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.IO;
using Kastra.Core.Business;
using Kastra.Core.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace Kastra.Core
{
    public static class KastraBuilder
    {
        public static IApplicationBuilder UseModuleStaticFiles(
            this IApplicationBuilder app, 
            IViewManager viewManager, 
            string moduleDirectoryPath, 
            string virtualModuleDirectoryPath = Constants.SiteConfig.DefaultModuleResourcesPath)
        {
            foreach (ModuleDefinitionInfo moduleDefinition in viewManager.GetModuleDefsList())
            {
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), moduleDirectoryPath, moduleDefinition.Path, "res")),
                    RequestPath = Path.Combine($"/{virtualModuleDirectoryPath}", moduleDefinition.KeyName)
                });
            }

            return app;
        }
    }
}
