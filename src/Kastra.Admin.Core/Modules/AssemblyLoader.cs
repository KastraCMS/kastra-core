using Kastra.Admin.Core.Configuration;
using Kastra.Core.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Modules
{
    public static class AssemblyLoader
    {
        /// <summary>
        /// Load the assemblies with an url list.
        /// </summary>
        /// <param name="http">Http client</param>
        /// <param name="dllUrlList">Urls of the DLL files</param>
        /// <returns></returns>
        public static async Task<IList<Assembly>> LoadAssemblyListAsync(HttpClient http, ICollection<string> dllUrlList)
        {
            http.ThrowIfArgumentNull(nameof(http));
            dllUrlList.ThrowIfArgumentNull(nameof(dllUrlList));

            foreach (string dllPath in dllUrlList)
            {
                var assembly = await LoadFromUrlAsync(http, dllPath);

                if (assembly is not null)
                {
                    AssembliesContext.Instance.AddAssembly(assembly);
                }
            }

            return AssembliesContext.Instance.GetList();
        }

        /// <summary>
        /// Load an assembly from an url.
        /// </summary>
        /// <param name="http">Http client</param>
        /// <param name="dllUrl">Url of the DLL file</param>
        /// <returns>Loaded assembly</returns>
        public static async Task<Assembly> LoadFromUrlAsync(HttpClient http, string dllUrl)
        {
            try
            {
                var bytes = await http.GetByteArrayAsync(dllUrl);

                return AssemblyLoadContext.Default.LoadFromStream(new MemoryStream(bytes));
            }
            catch (Exception)
            {
                Console.WriteLine($"Assembly cannot be loaded : {dllUrl}");

                return null;
            }
        }
    }
}
