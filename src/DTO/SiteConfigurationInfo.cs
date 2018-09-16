/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Dto
{
    public class SiteConfigurationInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HostUrl { get; set; }
        public bool CacheActivated { get; set; }
        public string SmtpHost { get; set; }
        public int SmtpPort { get; set; }
        public bool SmtpEnableSsl { get; set; }
        public string StmpCredentialsUser { get; set; }
        public string SmtpCredentialsPassword { get; set; }
    }
}