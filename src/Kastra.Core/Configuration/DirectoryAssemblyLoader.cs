/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;
using Kastra.Core.Configuration;
using Kastra.Core.Utils.Extensions;
using Serilog;

namespace Kastra.Core
{
    /// <summary>
    /// Directory assembly loader
    /// </summary>
    public static class DirectoryAssemblyLoader
    {
        /// <summary>
        /// Curretn directory of the web application
        /// </summary>
        private static readonly string _rootPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// Loads all assemblies from the application settings.
        /// </summary>
        /// <param name="appSettings">Application settings.</param>
        /// <param name="logger">Logger</param>
        public static void LoadAllAssemblies(AppSettings appSettings, ILogger logger)
        {
            appSettings.ThrowIfArgumentNull(nameof(appSettings));
            logger.ThrowIfArgumentNull(nameof(logger));

            var configuration = appSettings.Configuration;
            configuration.ThrowIfReferenceNull(nameof(configuration));

            // Load current assembly
            KastraAssembliesContext.Instance.AddAssembly(Assembly.GetCallingAssembly());

            string libsPath = Path.Combine(_rootPath, configuration.LibraryDirectoryPath);
            
            // Load the business dll
            LoadAssembly(Path.Combine(libsPath, configuration.BusinessDllPath));

            // Load the DAL dll
            LoadAssembly(Path.Combine(libsPath, configuration.DALDllPath));

            // Load all dependencies
            if (configuration.Libraries is not null)
            {
                foreach (string dependency in configuration.Libraries)
                {
                    try
                    {
                        LoadAssembly(Path.Combine(libsPath, dependency));
                    }
                    catch (FileNotFoundException ex)
                    {
                        logger.Error(ex, "Dependency cannot be loaded : {dependency}", dependency);
                    }
                }
            }

            // Load module dlls
            IEnumerable<string> moduleDllPaths = GetModulePaths(configuration.ModuleDirectoryPath);

            foreach (string dllPath in moduleDllPaths)
            {
                if (dllPath.Contains("wwwroot"))
                {
                    continue;
                }

                try
                {
                    LoadAssembly(dllPath);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Assembly cannot be loaded : {dllPath}", dllPath);
                }
            }
        }

        /// <summary>
        /// Loads an assembly with a dll path.
        /// </summary>
        /// <param name="dllPath">Dll path.</param>
        /// <returns></returns>
        public static void LoadAssembly(string dllPath)
        {
            Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);

            if (assembly is not null)
            {
                KastraAssembliesContext.Instance.AddAssembly(assembly);
            }
        }

        #region Private methods

        /// <summary>
        /// Get the module paths
        /// </summary>
        /// <param name="moduleDirectory">Module directory</param>
        /// <returns>Module paths</returns>
        private static IEnumerable<string> GetModulePaths(string moduleDirectory)
        {
            moduleDirectory.ThrowIfArgumentNull(nameof(moduleDirectory));

            string path = Path.Combine(_rootPath, moduleDirectory);

            return Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);
        }

		#endregion
	}
}
