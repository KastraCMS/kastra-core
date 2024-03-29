﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Kastra.Core.Services.Contracts;
using Kastra.Core.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Kastra.Core.Modules;
using Kastra.Core.Configuration;
using System.Threading.Tasks;

namespace Kastra.Core.Services
{
    public static class DependencyRegisterService
    {
        /// <summary>
        /// Adds the dependency injection.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="assemblies">Assemblies.</param>
        public static void AddDependencyInjection(
            this IServiceCollection services, 
            IConfiguration configuration, 
            params Assembly[] assemblies)
        {
            Type serviceType = typeof(IDependencyRegister);

            for (int i = 0; i < assemblies.Length; i++)
            {
                Type[] types = assemblies[i].GetTypes();

                for (int j = 0; j < types.Length; j++)
                {
                    Type type = types[j];

                    if(serviceType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                    {
                        IDependencyRegister dependancyRegister = Activator.CreateInstance(type) as IDependencyRegister;
                        dependancyRegister.SetDependencyInjections(services, configuration);
                    }
                }
            }
        }

        /// <summary>
        /// Adds the application parts.
        /// </summary>
        /// <param name="mvcBuilder">Mvc builder.</param>
        public static void AddApplicationParts(this IMvcBuilder mvcBuilder)
        {
            IList<Assembly> assemblies = KastraAssembliesContext.Instance.GetModuleAssemblies();

            for (int i = 0; i < assemblies.Count; i++)
            {
                mvcBuilder.AddApplicationPart(assemblies[i]);
            }
        }

        /// <summary>
        /// Adds the kastra default services.
        /// </summary>
        /// <param name="services">Services.</param>
        public static void AddKastraServices(this IServiceCollection services)
        {
            services.AddSingleton<CacheEngine>();
            services.AddScoped<ViewEngine>();
        }

        /// <summary>
        /// Configures the kastra options.
        /// </summary>
        /// <param name="services">Services.</param>
        public static async Task ConfigureKastraOptions(this IServiceCollection services)
        {
            // Get site configuration
            using ServiceProvider serviceProvider = services.BuildServiceProvider();
            IParameterManager parameterManager = serviceProvider.GetService<IParameterManager>();
            SiteConfigurationInfo siteConfiguration = await parameterManager.GetSiteConfigurationAsync();

            // Set configuration in identity options
            services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = siteConfiguration.RequireConfirmedEmail;

                options.User.RequireUniqueEmail = siteConfiguration.UserRequireUniqueEmail;
                options.User.AllowedUserNameCharacters = siteConfiguration.UserAllowedUserNameCharacters;
                
                options.Password.RequireDigit = siteConfiguration.PasswordRequireDigit;
                options.Password.RequiredLength = siteConfiguration.PasswordRequiredLength;
                options.Password.RequireLowercase = siteConfiguration.PasswordRequireLowercase;
                options.Password.RequireNonAlphanumeric = siteConfiguration.PasswordRequireNonAlphanumeric;
                options.Password.RequireUppercase = siteConfiguration.PasswordRequireUppercase;
                options.Password.RequiredUniqueChars = siteConfiguration.PasswordRequiredUniqueChars;
            });

            // Set cookie configuration
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => siteConfiguration.CheckConsentNeeded;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }
    }
}
