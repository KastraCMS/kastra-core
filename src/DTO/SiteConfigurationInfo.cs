/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

namespace Kastra.Core.Dto
{
    public class SiteConfigurationInfo
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the host URL.
        /// </summary>
        /// <value>The host URL.</value>
        public string HostUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cache activated.
        /// </summary>
        /// <value><c>true</c> if cache activated; otherwise, <c>false</c>.</value>
        public bool CacheActivated { get; set; }

        /// <summary>
        /// Gets or sets the smtp host.
        /// </summary>
        /// <value>The smtp host.</value>
        public string SmtpHost { get; set; }

        /// <summary>
        /// Gets or sets the smtp port.
        /// </summary>
        /// <value>The smtp port.</value>
        public int SmtpPort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ssl is enabled for smtp.
        /// </summary>
        /// <value><c>true</c> if smtp enable ssl; otherwise, <c>false</c>.</value>
        public bool SmtpEnableSsl { get; set; }

        /// <summary>
        /// Gets or sets the smtp credentials user.
        /// </summary>
        /// <value>The smtp credentials user.</value>
        public string SmtpCredentialsUser { get; set; }

        /// <summary>
        /// Gets or sets the smtp credentials password.
        /// </summary>
        /// <value>The smtp credentials password.</value>
        public string SmtpCredentialsPassword { get; set; }

        /// <summary>
        /// Gets or sets the email sender.
        /// </summary>
        /// <value>The email sender.</value>
        public string EmailSender { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a confirmed email is required for login.
        /// </summary>
        /// <value><c>true</c> if require confirmed email; otherwise, <c>false</c>.</value>
        public bool RequireConfirmedEmail { get; set; }

        /// <summary>
        /// Gets or sets the website theme.
        /// </summary>
        /// <value>The theme</value>
        public string Theme { get; set; }
    }
}