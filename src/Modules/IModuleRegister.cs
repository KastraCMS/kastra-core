/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using Kastra.Core.Business;

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
        void Install(IServiceProvider serviceProvider, IViewManager viewManager);

        /// <summary>
        /// Executed when a module is uninstalled.
        /// </summary>
        void Uninstall();
    }
}