/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core
{
    public interface IApplicationManager
    {
        /// <summary>
        /// Install a module.
        /// </summary>
        void Install();

        /// <summary>
        /// Installs the default page.
        /// </summary>
        void InstallDefaultPage();

        /// <summary>
        /// Installs the default template.
        /// </summary>
        void InstallDefaultTemplate();

        /// <summary>
        /// Installs the default permissions.
        /// </summary>
        void InstallDefaultPermissions();
    }
}