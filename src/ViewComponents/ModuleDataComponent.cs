/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using Kastra.Core.Dto;
using Kastra.Core.Services;

namespace Kastra.Core.ViewComponents
{
    public class ModuleDataComponent
    {
        public string ModuleViewComponent { get; set; }
        public string ModuleAction { get; set; }
        public ModuleInfo Module { get; set; }
        public PageInfo Page { get; set; }
        public CacheEngine CacheEngine { get; set; }
        public IList<PermissionInfo> RequiredClaims { get; set; }
    }
}