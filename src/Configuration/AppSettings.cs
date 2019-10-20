﻿/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Configuration
{
    /// <summary>
    /// Default app settings for a Kastra website.
    /// </summary>
    public class AppSettings
    {
        public AppSettings()
        {
            Configuration = new Configuration();
			Cors = new Cors();
        }

        public Configuration Configuration { get; set; }
		public Cors Cors { get; set; }
    }

    public class Configuration
    {
        public bool DevelopmentMode { get; set; }
        public string BusinessDllPath { get; set; }
        public string DALDllPath { get; set; }
        public string ModuleDirectoryPath { get; set; } = Constants.ModuleConfig.ModuleRootDirectory;
        public string FileDirectoryPath { get; set; } = Constants.SiteConfig.DefaultFileDirectoryPath;
        public bool EnableDatabaseUpdate { get; set; }
    }

    public class Cors
	{
        public bool EnableCors { get; set; }
		public bool AllowAnyOrigin { get; set; }
		public string Origins { get; set; }
	}
}