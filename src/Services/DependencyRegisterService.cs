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
            Type serviceType = typeof(IDependencyRegister);

            IEnumerable<Type> assembliesToRegister = assemblies.SelectMany(assembly => assembly.GetTypes())
                                                 .Where(type => serviceType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract);

            foreach (Type implementationType in assembliesToRegister)
            {
                dependancyRegister = Activator.CreateInstance(implementationType) as IDependencyRegister;
                dependancyRegister.SetDependencyInjections(services, configuration);
            }
        }

        /// <summary>
        /// Adds the application parts.
        /// </summary>
        /// <param name="mvcBuilder">Mvc builder.</param>
        public static void AddApplicationParts(this IMvcBuilder mvcBuilder)
        {
            var assemblies = KastraAssembliesContext.Instance.GetModuleAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
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
