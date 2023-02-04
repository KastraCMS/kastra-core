/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using Kastra.Core.Constants;

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
            HCaptcha = new HCaptcha();
            ClamAV = new ClamAV();
            JsonLocalization = new JsonLocalization();
        }

        /// <summary>
        /// Application configuration
        /// </summary>
        public Configuration Configuration { get; set; }
		
        /// <summary>
        /// Application CORS
        /// </summary>
        public Cors Cors { get; set; }
        
        /// <summary>
        /// HCaptcha settings
        /// </summary>
        public HCaptcha HCaptcha { get; set; }
        
        /// <summary>
        /// ClamAV settings
        /// </summary>
        public ClamAV ClamAV { get; set; }
        
        /// <summary>
        /// Json localization configuration
        /// </summary>
        public JsonLocalization JsonLocalization { get; set; }
    }

    /// <summary>
    /// Application configuration
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Development mode.
        /// </summary>
        public bool DevelopmentMode { get; set; }

        /// <summary>
        /// Business dll path (must be in the library directory)
        /// </summary>
        public string BusinessDllPath { get; set; }

        /// <summary>
        /// DAL dll path (must be in the library directory)
        /// </summary>
        public string DALDllPath { get; set; }

        /// <summary>
        /// Library directory path
        /// </summary>
        public string LibraryDirectoryPath { get; set; } = SiteConfiguration.DefaultLibsPath;
        
        /// <summary>
        /// Libraries to load
        /// </summary>
        public string[] Libraries { get; set; } 
        
        /// <summary>
        /// Module directory path
        /// </summary>
        public string ModuleDirectoryPath { get; set; } = ModuleConfiguration.ModuleRootDirectory;
        
        /// <summary>
        /// Directory path where the files are stored
        /// </summary>
        public string FileDirectoryPath { get; set; } = SiteConfiguration.DefaultFileDirectoryPath;
        
        /// <summary>
        /// Enable the automatic update of the database when the application is starting
        /// </summary>
        public bool EnableDatabaseUpdate { get; set; }
        
        /// <summary>
        /// If the application should use the default fallback (used for single page application)
        /// </summary>
        public bool HasDefaultFallback { get; set; }
    }

    /// <summary>
    /// CORS settings
    /// </summary>
    public class Cors
	{
        /// <summary>
        /// Enable the CORS
        /// </summary>
        public bool EnableCors { get; set; }
		
        /// <summary>
        /// Allow any origin
        /// </summary>
        public bool AllowAnyOrigin { get; set; }

        /// <summary>
        /// Origins
        /// </summary>
		public string Origins { get; set; }
	}

    /// <summary>
    /// HCaptcha configuration
    /// </summary>
    public class HCaptcha
    {
        /// <summary>
        /// Enable HCaptcha
        /// </summary>
        public bool EnableHCaptcha { get; set; }

        /// <summary>
        /// Secret
        /// </summary>
        public string Secret { get; set; }
    }

    /// <summary>
    /// ClamAV configuration
    /// </summary>
    public class ClamAV
    {
        /// <summary>
        /// Enable ClamAV
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// ClamAV host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// ClamAV port
        /// </summary>
        public int Port { get; set; }
    }

    /// <summary>
    /// Json localization
    /// </summary>
    public class JsonLocalization
    {
        /// <summary>
        /// Path where the resources are stored
        /// </summary>
        public string ResourcesPath { get; set; }

        /// <summary>
        /// Default culture
        /// </summary>
        public string DefaultCulture { get; set; }
    }
}
