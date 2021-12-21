/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Reflection;
using System.Threading.Tasks;

namespace Kastra.Core.Services.Contracts
{
    public interface IModuleManager
    {
        /// <summary>
        /// Installs the modules present in the folder modules.
        /// </summary>
        Task InstallModulesAsync();

        /// <summary>
        /// Uninstalls the modules.
        /// </summary>
        Task UninstallModulesAsync();

        /// <summary>
        /// Installs the module.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        Task InstallModuleAsync(Assembly assembly);

        /// <summary>
        /// Uninstalls the module.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        Task UninstallModuleAsync(Assembly assembly);
    }
}