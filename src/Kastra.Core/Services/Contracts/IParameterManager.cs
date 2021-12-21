/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Kastra.Core.DTO;
using System.Threading.Tasks;

namespace Kastra.Core.Services.Contracts
{
	public interface IParameterManager
	{
        /// <summary>
        /// Gets the site configuration.
        /// </summary>
        /// <returns>The site configuration.</returns>
		Task<SiteConfigurationInfo> GetSiteConfigurationAsync();

        /// <summary>
        /// Saves the site configuration.
        /// </summary>
        /// <param name="siteConfiguration">Site configuration.</param>
		Task SaveSiteConfiguration(SiteConfigurationInfo siteConfiguration);
	}
}
