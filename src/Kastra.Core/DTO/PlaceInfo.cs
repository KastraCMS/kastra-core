/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;

namespace Kastra.Core.DTO
{
	public class PlaceInfo
	{
		public PlaceInfo()
		{
			Modules = new List<ModuleInfo>();
		}

		public int PlaceId { get; set; }
		public int PageTemplateId { get; set; }
		public string KeyName { get; set; }

		public int? ModuleId { get; set; }

		public IList<ModuleInfo> Modules { get; set; }
		public TemplateInfo Template { get; set; }
		public ModuleInfo StaticModule { get; set; }
	}
}
