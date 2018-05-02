/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Reflection;
using Kastra.Core.Business;

namespace Kastra.Core
{
    public class KastraAssembliesContext
    {
        #region Properties

        private static Dictionary<String, Assembly> _assemblies = new Dictionary<String, Assembly>();
        public Dictionary<String, Assembly> Assemblies
        {
            get 
            {
                return _assemblies;
            }
        }

        private static KastraAssembliesContext _instance = null;
        public static KastraAssembliesContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new KastraAssembliesContext();
                }

                return _instance;
            }
        }

        #endregion

        private KastraAssembliesContext() { }

        /// <summary>
        /// Adds the assembly.
        /// </summary>
        /// <param name="assembly">Assembly.</param>
        public void AddAssembly(Assembly assembly)
        {
            if (assembly == null)
                return;

            if (_assemblies.ContainsKey(assembly.FullName))
            {
                _assemblies[assembly.FullName] = assembly;
            }
            else
            {
                _assemblies.Add(assembly.FullName, assembly);
            }
        }

        /// <summary>
        /// Gets the module assemblies.
        /// </summary>
        /// <returns>The module assemblies.</returns>
        public IList<Assembly> GetModuleAssemblies()
        {
            Type moduleType = typeof(IModuleManager);
            IList<Assembly> assemblies = new List<Assembly>(_assemblies.Count);

            foreach(Assembly assembly in _assemblies.Values)
            {
                foreach(Type type in assembly.GetTypes())
                {
                    if(type == moduleType)
                    {
                        assemblies.Add(assembly);
                        break;
                    }
                }
            }

            return assemblies;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <returns>The type.</returns>
        /// <param name="typeName">Type name.</param>
		public Type GetType(string typeName)
		{
			Type type = Type.GetType(typeName);

			if (type != null) 
                return type;
            
            foreach (var a in _assemblies.Values)
			{
				type = a.GetType(typeName);
				if (type != null)
					return type;
			}

			return null;
		}
    }
}