using Kastra.Admin.Core.Models;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IConfigurationService
    {
        /// <summary>
        /// Load the application version information.
        /// </summary>
        /// <returns>ApplicationVersionModel</returns>
        Task<ApplicationVersionModel> LoadApplicationVersion();

        /// <summary>
        /// Load the application settings.
        /// </summary>
        /// <returns></returns>
        Task<SettingsModel> LoadSettings();

        /// <summary>
        /// Save the application settings.
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task<bool> SaveSettings(SettingsModel settings);
    }
}
