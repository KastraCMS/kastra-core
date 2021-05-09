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

namespace Kastra.Core
{
    public static class DirectoryAssemblyLoader
    {
        private static string _rootPath = Directory.GetCurrentDirectory();

        /// <summary>
        /// Loads all assemblies from the application settings.
        /// </summary>
        /// <param name="appSettings">App settings.</param>
        public static void LoadAllAssemblies(AppSettings appSettings)
        {
            appSettings.ThrowIfArgumentNull(nameof(appSettings));

            var configuration = appSettings.Configuration;
            configuration.ThrowIfReferenceNull(nameof(configuration));

            // Load the business dll
            LoadAssembly($"{_rootPath}/{appSettings.Configuration.BusinessDllPath}");

            // Load the DAL dll
            LoadAssembly($"{_rootPath}/{appSettings.Configuration.DALDllPath}");

            // Load module dlls
            IEnumerable<string> moduleDllPaths = GetModulePaths(configuration.ModuleDirectoryPath);

            foreach (string dllPath in moduleDllPaths)
            {
                LoadAssembly(dllPath);
            }
        }

        /// <summary>
        /// Loads an assembly with a dll path.
        /// </summary>
        /// <param name="dllPath">Dll path.</param>
        /// <returns></returns>
        public static void LoadAssembly(string dllPath)
        {
            try
            {
                Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);

                if (assembly is not null)
                {
                    KastraAssembliesContext.Instance.AddAssembly(assembly);
                }
            }
            catch
            {
                throw new InvalidOperationException($"Assembly cannot be loaded : {dllPath}");
            }
        }

        #region Private methods

        private static IEnumerable<string> GetModulePaths(string moduleDirectory)
        {
            moduleDirectory.ThrowIfArgumentNull(nameof(moduleDirectory));

            string path = $"{_rootPath}/{moduleDirectory}";

            return Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);
        }

		#endregion
	}
}
