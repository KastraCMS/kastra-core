/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;

namespace Kastra.Core.DTO
{
	public class TemplateInfo
	{
		public TemplateInfo()
		{
			Pages = new List<PageInfo>();
			Places = new List<PlaceInfo>();
		}

		public int TemplateId { get; set; }
		public string KeyName { get; set; }
		public string Name { get; set; }
		public string ViewPath { get; set; }
		public string ModelClass { get; set; }

		public IList<PageInfo> Pages { get; set; }
		public IList<PlaceInfo> Places { get; set; }
	}
}
