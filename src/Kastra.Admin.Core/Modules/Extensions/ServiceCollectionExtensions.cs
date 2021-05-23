/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Kastra.Admin.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Kastra.Admin.Core.Modules.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add the services of the modules.
        /// </summary>
        /// <param name="serviceCollection">Service collection</param>
        public static void AddModuleServices(this IServiceCollection serviceCollection)
        {
            Type serviceType = typeof(IDependencyRegister);
            IList<Assembly> moduleAssemblies = AssembliesContext.Instance.GetList();

            if (moduleAssemblies is null)
            {
                return;
            }

            for (int i = 0; i < moduleAssemblies.Count; i++)
            {
                Type[] types = moduleAssemblies[i].GetTypes();

                for (int j = 0; j < types.Length; j++)
                {
                    Type type = types[j];

                    if (serviceType.IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
                    {
                        IDependencyRegister dependancyRegister = Activator.CreateInstance(type) as IDependencyRegister;
                        dependancyRegister.SetDependencyInjections(serviceCollection);
                    }
                }
            }
        }
    }
}
