/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kastra.Core
{
    public interface IDependencyRegister
    {
        /// <summary>
        /// Sets the dependency injections for a module.
        /// </summary>
        /// <param name="services">Services.</param>
        /// <param name="configuration">Configuration.</param>
        void SetDependencyInjections(IServiceCollection services, IConfiguration configuration);
    }
}
