using Microsoft.AspNetCore.Http;
using System.IO;

namespace Kastra.Core.Utils.Helpers
{
    public static class ModuleHelpers
    {
        /// <summary>
        /// Get the virtual binary directory path of a module.
        /// </summary>
        /// <param name="virtualModuleDirectoryPath">Virtual module directory path</param>
        /// <param name="moduleKeyname">Module keyname</param>
        /// <param name="basePath">Base path of the application</param>
        /// <returns></returns>
        public static PathString GetModuleBinaryPath(
            string virtualModuleDirectoryPath, 
            string moduleKeyname, 
            string basePath = null)
        {
            return new PathString($"/{virtualModuleDirectoryPath}/{moduleKeyname}/{basePath}/bin");
        }

        /// <summary>
        /// Get the physical binary directory path of a module.
        /// </summary>
        /// <param name="moduleDirectoryPath">Modules directory path</param>
        /// <param name="contentDirectoryPath">Content directory path</param>
        /// <param name="basePath">Base path of the application</param>
        /// <returns></returns>
        public static string GetPhysicalModuleBinaryPath(
            string moduleDirectoryPath,
            string contentDirectoryPath,
            string basePath = null)
        {
            return Path.Combine(moduleDirectoryPath, contentDirectoryPath, basePath, "bin");
        }
    }
}
