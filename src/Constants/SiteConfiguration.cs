/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Constants
{
    public static class SiteConfiguration
    {
        /// <summary>
        /// Default path for a page template.
        /// </summary>
        public const string PageTemplatePath = "/Views/Template/";

        /// <summary>
        /// Default keyname for a page template.
        /// </summary>
        public const string DefaultPageTemplateKeyName = "DefaultTemplate";

        /// <summary>
        /// Default folder path for the modules.
        /// </summary>
        public const string DefaultModulesPath = "~/Modules/";

        /// <summary>
        /// Default view name for the modules.
        /// </summary>
        public const string DefaultModuleViewName = "Index";

        /// <summary>
        /// Default path for the resources in a module.
        /// </summary>
        public const string DefaultModuleResourcesPath = "_module";

        /// <summary>
        /// Default cache key for the site configuration.
        /// </summary>
        public const string SiteConfigCacheKey = "SiteConfig";

        /// <summary>
        /// Default theme.
        /// </summary>
        public const string DefaultTheme = "default";

        /// <summary>
        /// Default path for the file directory.
        /// </summary>
        public const string DefaultFileDirectoryPath = "~/Data/";
    }
}
