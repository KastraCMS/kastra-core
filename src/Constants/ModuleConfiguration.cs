/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Constants
{
    public static class ModuleConfiguration
    {
        /// <summary>
        /// Default keyname for a module.
        /// </summary>
        public const string DefaultModuleKeyName = "Default";

        /// <summary>
        /// Full name of the default module.
        /// </summary>
        public const string DefaultModuleFullName = "Kastra.Core.Modules.ViewComponents.Default.Default";

        /// <summary>
        /// Cache key for a module.
        /// </summary>
        public const string ModuleCacheKey = "Module_{0}";

        /// <summary>
        /// Key for a module view component parameter.
        /// </summary>
        public const string ModuleViewComponentParameters = "Module_ViewComponent_Parameters_{0}";

        /// <summary>
        /// Default directory for the modules.
        /// </summary>
        public const string ModuleRootDirectory = "Modules";

        /// <summary>
        /// Module permission type.
        /// </summary>
        public const string ModulePermissionType = "PERMISSION";

        /// <summary>
        /// Value of access permission.
        /// </summary>
        public const int AccessPermission = 1;

        /// <summary>
        /// Value of granted access permission.
        /// </summary>
        public const string GrantedAccessPermission = "GrantedAccess";

        /// <summary>
        /// Value of read permission.
        /// </summary>
        public const string ReadPermission = "Read";

        /// <summary>
        /// Value of edit permission.
        /// </summary>
        public const string EditPermission = "Edit";
    }
}
