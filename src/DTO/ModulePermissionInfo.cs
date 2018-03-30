/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core
{
	public class ModulePermissionInfo
	{
		public int ModulePermissionId { get; set; }
		public int PermissionId { get; set; }
		public int ModuleId { get; set; }

		public ModuleInfo Module { get; set; }
		public PermissionInfo Permission { get; set; }
	}
}
