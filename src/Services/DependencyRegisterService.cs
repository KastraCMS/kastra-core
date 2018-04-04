/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Kastra.Core.Services;

namespace Kastra.Core
{
    public static class DependencyRegisterService
    {
        /// <summary>
        /// Adds the dependency injection.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        /// <param name="assemblies">Assemblies.</param>
        public static void AddDependencyInjection(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            IDependencyRegister dependancyRegister = null;
            Type type = null;
            Type serviceType = typeof(IDependencyRegister);

            for (int i = 0; i < assemblies.Length; i++)
            {
                Type[] types = assemblies[i].GetTypes();

                for (int j = 0; j < types.Length; j++)
                {
                    type = types[j];

                    if(serviceType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                    {
                        dependancyRegister = Activator.CreateInstance(type) as IDependencyRegister;
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
    }
}
