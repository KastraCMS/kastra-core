﻿using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class SettingsModel
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        /// [Required]
        [StringLength(50)]
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
        /// Gets or sets the website theme.
        /// </summary>
        /// <value>The theme</value>
        public string Theme { get; set; }

        public string[] ThemeList { get; set; } 

        #region Password

        /// <summary>
        /// Gets or sets a value indicating whether the user password
        /// require digit.
        /// </summary>
        /// <value><c>true</c> if password require digit; otherwise, <c>false</c>.</value>
        public bool PasswordRequireDigit { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user password
        /// require lowercase.
        /// </summary>
        /// <value><c>true</c> if password require lowercase; otherwise, <c>false</c>.</value>
        public bool PasswordRequireLowercase { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user password
        /// require non alphanumeric.
        /// </summary>
        /// <value><c>true</c> if password require non alphanumeric; otherwise, <c>false</c>.</value>
        public bool PasswordRequireNonAlphanumeric { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user password
        /// require uppercase.
        /// </summary>
        /// <value><c>true</c> if password require uppercase; otherwise, <c>false</c>.</value>
        public bool PasswordRequireUppercase { get; set; }

        /// <summary>
        /// Gets or sets the length of the password required.
        /// </summary>
        /// <value>The length of the password required.</value>
        public int PasswordRequiredLength { get; set; }

        /// <summary>
        /// Gets or sets the number of distinct characters in the password.
        /// </summary>
        /// <value>The password required unique chars.</value>
        public int PasswordRequiredUniqueChars { get; set; }

        #endregion

        #region User

        /// <summary>
        /// Gets or sets a value indicating whether a confirmed email is required for login.
        /// </summary>
        /// <value><c>true</c> if require confirmed email; otherwise, <c>false</c>.</value>
        public bool RequireConfirmedEmail { get; set; }

        /// <summary>
        /// Disable the default registration.
        /// </summary>
        public bool DisableRegistration { get; set; }

        /// <summary>
        /// Gets or sets the user allowed user name characters.
        /// </summary>
        /// <value>The user allowed user name characters.</value>
        [StringLength(150)]
        public string UserAllowedUserNameCharacters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the user
        /// require unique email.
        /// </summary>
        /// <value><c>true</c> if user require unique email; otherwise, <c>false</c>.</value>
        public bool UserRequireUniqueEmail { get; set; }

        #endregion

        #region Cookie

        /// <summary>
        /// Gets or sets the access denied path.
        /// </summary>
        /// <value>The access denied path.</value>
        public string AccessDeniedPath { get; set; }

        /// <summary>
        /// Gets or sets the name of the cookie.
        /// </summary>
        /// <value>The name of the cookie.</value>
        public string CookieName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cookie is
        /// http only.
        /// </summary>
        /// <value><c>true</c> if cookie http only; otherwise, <c>false</c>.</value>
        public bool CookieHttpOnly { get; set; }

        /// <summary>
        /// Gets or sets the expire time span in minutes.
        /// </summary>
        /// <value>The expire time span minutes.</value>
        public int ExpireTimeSpanMinutes { get; set; }

        /// <summary>
        /// Gets or sets the login path.
        /// </summary>
        /// <value>The login path.</value>
        public string LoginPath { get; set; }

        /// <summary>
        /// Gets or sets the return URL parameter.
        /// </summary>
        /// <value>The return URL parameter.</value>
        public string ReturnUrlParameter { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the cookie has a sliding expiration.
        /// </summary>
        /// <value><c>true</c> if sliding expiration; otherwise, <c>false</c>.</value>
        public bool SlidingExpiration { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the consent is mandatory to use cookies
        /// </summary>
        /// <value></value>
        public bool CheckConsentNeeded { get; set; }

        /// <summary>
        /// Gets or sets the consent notice text for the cookies
        /// </summary>
        /// <value></value>
        public string ConsentNotice { get; set; }

        /// <summary>
        /// Gets or sets the url for the cookie use policy
        /// </summary>
        /// <value></value>
        public string CookieUsePolicyUrl { get; set; }

        #endregion

        #region Lockout settings

        /// <summary>
        /// Gets or sets the amount of time a user is locked out when a lockout occurs.
        /// </summary>
        /// <value>The default lockout time span in minutes.</value>
        public int DefaultLockoutTimeSpanInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the lockout max failed access attempts.
        /// </summary>
        /// <value>The lockout max failed access attempts.</value>
        public int LockoutMaxFailedAccessAttempts { get; set; }

        /// <summary>
        /// Determines if a new user can be locked out.
        /// </summary>
        /// <value><c>true</c> if lockout allowed for new users; otherwise, <c>false</c>.</value>
        public bool LockoutAllowedForNewUsers { get; set; }

        #endregion
    }
}
