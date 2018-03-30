/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;

namespace Kastra.Core
{
	public class ModuleInfo
	{
		public ModuleInfo()
		{
			ModulePermissions = new List<ModulePermissionInfo>();
		}

		public int ModuleId { get; set; }
		public int ModuleDefId { get; set; }
		public int PlaceId { get; set; }
		public int PageId { get; set; }
		public string Name { get; set; }

		public IList<ModulePermissionInfo> ModulePermissions { get; set; }
		public ModuleDefinitionInfo ModuleDefinition { get; set; }
		public PlaceInfo Place { get; set; }
	}
}
