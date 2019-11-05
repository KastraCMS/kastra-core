/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.IO;
using Kastra.Core.Business;
using Kastra.Core.Constants;
using Kastra.Core.Dto;
using Kastra.Core.Modules.Configuration;
using Microsoft.AspNetCore.Builder;
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

        /// <summary>
        /// Adds the de
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IMvcBuilder AddKastraModule(this IMvcBuilder builder)
        {
            if (builder is null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

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
