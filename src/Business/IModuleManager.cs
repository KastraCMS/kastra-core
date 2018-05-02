/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Business
{
    public interface IModuleManager
    {
        /// <summary>
        /// Installs the modules present in the folder modules.
        /// </summary>
        void InstallModules();

        /// <summary>
        /// Uninstalls the modules.
        /// </summary>
        void UninstallModules();
    }
}