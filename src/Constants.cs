/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core
{
    public class Constants
    {
        public class SiteConfig
        {
            public const string PageTemplatePath = "/Views/Template/";
            public const string DefaultPageTemplateKeyName = "DefaultTemplate";
            public const string DefaultPageTemplatePath  = "Kastra.Web.Models.Template";
            public const string DefaultModulesPath = "~/Modules/";
            public const string DefaultModuleViewName = "Index";
            public const string SiteConfigCacheKey = "SiteConfig";   
        }

        public class ModuleConfig
        {
            public const string DefaultModuleKeyName = "Default";
            public const string DefaultModuleFullName = "Kastra.Core.Default.Default";
            public const string DefaultModulePath = "Default/Default";
            public const string ModuleCacheKey = "Module_{0}";
            public const string ModuleViewComponentParameters = "Module_ViewComponent_Parameters_{0}";
            public const string ModuleRootDirectory = "Modules";
            public const string ModulePermissionType = "PERMISSION";
            public const int AccessPermission = 1;
            public const string GrantedAccessPermission = "GrantedAccess";
            public const string ReadPermission = "Read";
            public const string EditPermission = "Edit";
		}

        public class PageConfig
        {
            public const string PageCacheKey = "Page_{0}";
            public const string PageByKeyCacheKey = "Page_Key_{0}";
            public const string Index = "index";
            public const string NoIndex = "noindex";
            public const string Follow = "follow";
            public const string NoFollow = "nofollow";
        }

        public class ModuleActions
        {
            public const string None = "";
            public const string Add = "add";
            public const string Update = "update";
            public const string Delete = "delete";
        }

        public class TemplateConfig
        {
            public const string TemplateModelTypeCacheKey = "TemplateModel_{0}_ModuleControl_{1}_Module_{2}_ModuleAction_{3}";
        }
    }
}