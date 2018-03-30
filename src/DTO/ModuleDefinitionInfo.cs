/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;

namespace Kastra.Core
{
	public class ModuleDefinitionInfo
	{
		public ModuleDefinitionInfo()
		{
			ModuleControls = new List<ModuleControlInfo>();
			Modules = new List<ModuleInfo>();
		}

		public int ModuleDefId { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public string KeyName { get; set; }
        public string Namespace { get; set; }
		public string Version { get; set; }

		public IList<ModuleControlInfo> ModuleControls { get; set; }
		public IList<ModuleInfo> Modules { get; set; }
	}
}
