/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Kastra.Core.Dto;

namespace Kastra.Core.Business
{
	public interface IParameterManager
	{
        /// <summary>
        /// Gets the site configuration.
        /// </summary>
        /// <returns>The site configuration.</returns>
		SiteConfigurationInfo GetSiteConfiguration();

        /// <summary>
        /// Saves the site configuration.
        /// </summary>
        /// <param name="siteConfiguration">Site configuration.</param>
		void SaveSiteConfiguration(SiteConfigurationInfo siteConfiguration);
	}
}
