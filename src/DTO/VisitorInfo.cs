/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
namespace Kastra.Core.DTO
{
    public class VisitorInfo
    {
        public Guid Id { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public string UserId { get; set; }
        public DateTime LastVisitAt { get; set; }
    }
}
