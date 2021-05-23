using Kastra.Admin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IModuleService
    {
        /// <summary>
        /// Load the module definitions.
        /// </summary>
        /// <returns>IList<ModuleDefinitionModel></returns>
        Task<IList<ModuleDefinitionModel>> LoadModuleDefinitionList();

        /// <summary>
        /// Load the list of installed and unninstalled modules.
        /// </summary>
        /// <returns>IList<ModuleDefinitionModel></returns>
        Task<IList<ModuleDefinitionModel>> LoadAllModuleDefinitionList();

        /// <summary>
        /// Load a module.
        /// </summary>
        /// <param name="id">Module Id</param>
        /// <returns>ModuleModel</returns>
        Task<ModuleModel> LoadModule(int id);

        /// <summary>
        /// Load the module list.
        /// </summary>
        /// <returns></returns>
        Task<IList<ModuleModel>> LoadModuleList();

        /// <summary>
        /// Load the urls list of the modules asssemblies.
        /// </summary>
        /// <returns></returns>
        Task<IList<string>> LoadModuleAssemblyUrlList();

        /// <summary>
        /// Load the module navigation list for the administration navigation.
        /// </summary>
        /// <returns>Module navigation list</returns>
        Task<IList<NavigationModel>> LoadAdminModuleNavigationModelList();
        
        /// <summary>
        /// Load the module navigation list for the administration navigation.
        /// </summary>
        /// <returns>Module navigation list</returns>
        Task<IList<NavigationModel>> LoadModuleNavigationModelList();

        /// <summary>
        /// Save a module.
        /// </summary>
        /// <param name="module">Module model</param>
        /// <returns>Is success</returns>
        Task<bool> SaveModule(ModuleModel module);

        /// <summary>
        /// Delete a module.
        /// </summary>
        /// <param name="moduleId">Module Id</param>
        /// <returns></returns>
        Task<bool> DeleteModule(int moduleId);

        /// <summary>
        /// Install a module.
        /// </summary>
        /// <param name="assemblyName">Assembly name</param>
        /// <returns>Is success</returns>
        Task<bool> InstallModule(string assemblyName);

        /// <summary>
        /// Uninstall a module.
        /// </summary>
        /// <param name="assemblyName">Assembly name</param>
        /// <param name="moduleDefinitionId">Module Definition Id</param>
        /// <returns>Is success</returns>
        Task<bool> UninstallModule(string assemblyName, int moduleDefinitionId);
    }
}
