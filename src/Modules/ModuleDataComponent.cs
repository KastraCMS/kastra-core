/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using Kastra.Core.DTO;
using Kastra.Core.Services;

namespace Kastra.Core.Modules
{
    public class ModuleDataComponent
    {
        /// <summary>
        /// Module view component name.
        /// </summary>
        public string ModuleViewComponent { get; set; }

        /// <summary>
        /// Module action.
        /// </summary>
        public string ModuleAction { get; set; }

        /// <summary>
        /// Module information object.
        /// </summary>
        public ModuleInfo Module { get; set; }

        /// <summary>
        /// Page information object.
        /// </summary>
        public PageInfo Page { get; set; }

        /// <summary>
        /// Cache engine.
        /// </summary>
        public CacheEngine CacheEngine { get; set; }

        /// <summary>
        /// Required claims to display the view component.
        /// </summary>
        public IList<PermissionInfo> RequiredClaims { get; set; }
    }
}