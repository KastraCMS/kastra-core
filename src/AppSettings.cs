/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core
{
    /// <summary>
    /// Default app settings for a Kastra website.
    /// </summary>
    public class AppSettings
    {
        public AppSettings()
        {
            Configuration = new Configuration();
        }

        public Configuration Configuration { get; set; }
    }

    public class Configuration
    {
        public string BusinessDllPath { get; set; }
        public string DALDllPath { get; set; }
        public string ModuleDirectoryPath { get; set; } = Constants.ModuleConfig.ModuleRootDirectory;
    }
}
