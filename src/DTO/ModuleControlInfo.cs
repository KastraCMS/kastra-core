/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Dto
{
	public class ModuleControlInfo
	{
		public int ModuleControlId { get; set; }
		public int ModuleDefId { get; set; }
		public string KeyName { get; set; }
		public string Path { get; set; }

		public ModuleDefinitionInfo ModuleDefinition { get; set; }
	}
}
