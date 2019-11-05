/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Constants
{
    public static class PageConfiguration
    {
        /// <summary>
        /// Cache key to store page information.
        /// </summary>
        public const string PageCacheKey = "Page_{0}";

        /// <summary>
        /// Cache key to store page information with a page key..
        /// </summary>
        public const string PageByKeyCacheKey = "Page_Key_{0}";

        /// <summary>
        /// Index value for the robots meta tag.
        /// </summary>
        public const string Index = "index";

        /// <summary>
        /// NoIndex value for the robots meta tag.
        /// </summary>
        public const string NoIndex = "noindex";

        /// <summary>
        /// Follow value for the robots meta tag.
        /// </summary>
        public const string Follow = "follow";

        /// <summary>
        /// NoFollow value for the robots meta tag.
        /// </summary>
        public const string NoFollow = "nofollow";
    }
}
