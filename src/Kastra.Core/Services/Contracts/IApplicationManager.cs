/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Threading.Tasks;

namespace Kastra.Core.Services.Contracts
{
    public interface IApplicationManager
    {
        /// <summary>
        /// Install a module.
        /// </summary>
        Task InstallAsync();

        /// <summary>
        /// Installs the default page.
        /// </summary>
        Task InstallDefaultPageAsync();

        /// <summary>
        /// Installs the default template.
        /// </summary>
        Task InstallDefaultTemplateAsync();

        /// <summary>
        /// Installs the default permissions.
        /// </summary>
        Task InstallDefaultPermissionsAsync();

        /// <summary>
        /// Installs the default parameters.
        /// </summary>
        Task InstallDefaultParametersAsync();

        /// <summary>
        /// Install the default mail templates.
        /// </summary>
        Task InstallDefaultMailTemplatesAsync();
    }
}