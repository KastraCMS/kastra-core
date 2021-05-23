/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Microsoft.Extensions.DependencyInjection;

namespace Kastra.Admin.Core.Modules
{
    public interface IDependencyRegister
    {
        /// <summary>
        /// Sets the dependency injections for a module.
        /// </summary>
        /// <param name="services">Services.</param>
        void SetDependencyInjections(IServiceCollection services);
    }
}
