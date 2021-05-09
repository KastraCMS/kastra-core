/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using System.Reflection;
using Kastra.Core.Utils.Extensions;

namespace Kastra.Admin.Core.Configuration
{
    public class AssembliesContext
    {
        #region Properties

        private static Dictionary<string, Assembly> _assemblies = new Dictionary<string, Assembly>();
        public Dictionary<string, Assembly> Assemblies
        {
            get 
            {
                return _assemblies;
            }
        }

        private static AssembliesContext _instance;
        public static AssembliesContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AssembliesContext();
                }

                return _instance;
            }
        }

        #endregion

        private AssembliesContext() { }

        /// <summary>
        /// Adds the assembly.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        public void AddAssembly(Assembly assembly)
        {
            assembly.ThrowIfArgumentNull(nameof(assembly));

            if (_assemblies.ContainsKey(assembly.FullName))
            {
                _assemblies[assembly.FullName] = assembly;
            }
            else
            {
                _assemblies.Add(assembly.FullName, assembly);
            }
        }

        public IList<Assembly> GetList() => new List<Assembly>(Assemblies.Values);
    }
}