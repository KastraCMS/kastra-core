/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using System.IO;
using Kastra.Core.Services.Contracts;
using Kastra.Core.Constants;
using Kastra.Core.DTO;
using Kastra.Core.Modules.Configuration;
using Kastra.Core.Utils.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

namespace Kastra.Core.Configuration
{
    public static class KastraBuilder
    {
        public static IApplicationBuilder UseModuleStaticFiles(
            this IApplicationBuilder app, 
            IViewManager viewManager, 
            string moduleDirectoryPath, 
            string virtualModuleDirectoryPath = SiteConfiguration.DefaultModuleResourcesPath)
        {
            IList<ModuleDefinitionInfo> modulesDefinitionList = viewManager.GetModuleDefsList();

            if (modulesDefinitionList is null || modulesDefinitionList.Count == 0)
            {
                return app;
            }

            var extensionProvider = new FileExtensionContentTypeProvider();
            extensionProvider.Mappings.Add(".dll", "application/octet-stream");

            foreach (ModuleDefinitionInfo moduleDefinition in modulesDefinitionList)
            {
                string path = Path.Combine(
                        Directory.GetCurrentDirectory(), 
                        moduleDirectoryPath, 
                        moduleDefinition.Path, 
                        ModuleConfiguration.ModuleContentFolder
                    );

                if (Directory.Exists(path))
                {
                    string virtualPath = $"/{virtualModuleDirectoryPath}/{moduleDefinition.KeyName}";

                    app.UseStaticFiles(new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(path),
                        RequestPath = new PathString(virtualPath),
                        ContentTypeProvider = extensionProvider
                    });
                }
            }

            return app;
        }

        /// <summary>
        /// Adds the de
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddKastraModule(this IMvcBuilder builder)
        {
            builder.ThrowIfArgumentNull(nameof(builder));

            AddKastraServices(builder.Services);

            return builder;
        }

        /// <summary>
        /// Adds the Kastra default services.
        /// </summary>
        /// <param name="services"></param>
        internal static void AddKastraServices(IServiceCollection services)
        {
            services.TryAddEnumerable(
                ServiceDescriptor.Transient<IConfigureOptions<ModuleViewEngineOptions>, ModuleViewEngineOptionsSetup>());
        }
    }
}
