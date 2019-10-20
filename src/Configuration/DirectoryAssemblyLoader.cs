/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Kastra.Core.Configuration;
using Microsoft.Extensions.DependencyModel;

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
            int nbModules = 0;
            Assembly assembly = null;
            List<string> dllPaths = null;
            List<string> moduleDllPaths = null;

            if (appSettings == null)
            {
                throw new ArgumentNullException(nameof(appSettings));
            }

            if (appSettings.Configuration == null)
            {
                throw new NullReferenceException(nameof(appSettings.Configuration));
            }

            moduleDllPaths = GetModulePaths(appSettings.Configuration.ModuleDirectoryPath)?.ToList();

            if (moduleDllPaths != null)
            {
                nbModules = moduleDllPaths.Count;
            }

            dllPaths = new List<string>(nbModules + 2);
            dllPaths.Add($"{_rootPath}/{appSettings.Configuration.BusinessDllPath}");
            dllPaths.Add($"{_rootPath}/{appSettings.Configuration.DALDllPath}");
            dllPaths.AddRange(moduleDllPaths);

            foreach (string dllPath in dllPaths)
            {
				if ((assembly = LoadAssembly(dllPath)) != null)
                {
                    KastraAssembliesContext.Instance.AddAssembly(assembly);
                }
            }

            // Get Kastra assemblies
            LoadKastraAssemblies();
        }

        /// <summary>
        /// Loads an assembly with a dll path.
        /// </summary>
        /// <returns>The assembly.</returns>
        /// <param name="dllPath">Dll path.</param>
        public static Assembly LoadAssembly(string dllPath)
        {
            try
            {
                return AssemblyLoadContext.Default.LoadFromAssemblyPath(dllPath);
            }
            catch
            {
                return null;
            }
        }


        #region Private methods

        private static IEnumerable<string> GetModulePaths(string moduleDirectory)
        {
            string path = $"{_rootPath}/{moduleDirectory}";

            return Directory.EnumerateFiles(path, "*.dll", SearchOption.AllDirectories);
        }

		private static void LoadKastraAssemblies()
		{
			var dependencies = DependencyContext.Default.RuntimeLibraries;
			foreach (var library in dependencies)
			{
				if (IsCandidateCompilationLibrary(library))
				{
					var assembly = Assembly.Load(new AssemblyName(library.Name));
                    KastraAssembliesContext.Instance.AddAssembly(assembly);
				}
			}
		}

		private static bool IsCandidateCompilationLibrary(RuntimeLibrary compilationLibrary)
		{
			return compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("Kastra", StringComparison.Ordinal));
		}
       
		#endregion
	}
}
