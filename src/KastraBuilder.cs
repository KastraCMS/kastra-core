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
            string path = null;

            foreach (ModuleDefinitionInfo moduleDefinition in viewManager.GetModuleDefsList())
            {
                path = Path.Combine(Directory.GetCurrentDirectory(), moduleDirectoryPath, moduleDefinition.Path, "res");

                if (Directory.Exists(path))
                {
                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(path),
                        RequestPath = Path.Combine($"/{virtualModuleDirectoryPath}", moduleDefinition.KeyName)
                    });
                }
            }

            return app;
        }
    }
}
