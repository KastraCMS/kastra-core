/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.DTO
{
	public class PageInfo
	{
		public int PageId { get; set; }
		public int PageTemplateId { get; set; }
		public string Title { get; set; }
		public string KeyName { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaRobot { get; set; }

		public TemplateInfo PageTemplate { get; set; }
	}
}
