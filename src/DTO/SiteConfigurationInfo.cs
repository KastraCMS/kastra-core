/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core
{
    public class SiteConfigurationInfo
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string HostUrl { get; set; }
        public bool CacheActivated { get; set; }
    }
}