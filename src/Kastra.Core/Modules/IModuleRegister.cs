/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Threading.Tasks;
using Kastra.Core.Services.Contracts;

namespace Kastra.Core.Modules
{
    public interface IModuleRegister
    {
        /// <summary>
        /// Executed when a module is installed.
        /// </summary>
        /// <returns>The install.</returns>
        /// <param name="serviceProvider">Service provider.</param>
        /// <param name="viewManager">View manager.</param>
        Task InstallAsync(IServiceProvider serviceProvider, IViewManager viewManager);

        /// <summary>
        /// Executed when a module is uninstalled.
        /// </summary>
        Task UninstallAsync();
    }
}