using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    /// <summary>
    /// User model
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// User Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "username")]
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "email")]
        public string Email { get; set; }

        /// <summary>
        /// First name
        /// </summary>
        [StringLength(150)]
        [Display(Name = "first name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name
        /// </summary>
        [StringLength(150)]
        [Display(Name = "last name")]
        public string LastName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// If the email address is confirmed
        /// </summary>
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// Roles list
        /// </summary>
        public IList<string> Roles { get; set; }
    }
}
