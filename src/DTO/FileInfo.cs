/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;

namespace Kastra.Core.DTO
{
	public class FileInfo
	{
		public Guid FileId { get; set; }
		public string Name { get; set; }
		public string Path { get; set; }
		public DateTime DateCreated { get; set; }
	}
}