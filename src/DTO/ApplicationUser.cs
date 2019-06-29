using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kastra.Core.Dto
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the firstname.
        /// </summary>
        /// <value></value>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the lastname.
        /// </summary>
        /// <value></value>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the displayed name.
        /// </summary>
        /// <value></value>
        public string DisplayedName { get; set; }

        /// <summary>
        /// Gets or sets the date when an user is created. 
        /// </summary>
        /// <value></value>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date when an user is modified.
        /// </summary>
        /// <value></value>
        public DateTime DateModified { get; set; }

        /// <summary>
        /// Navigation property for the roles this user belongs to.
        /// </summary>
        public virtual ICollection<IdentityUserRole<string>> Roles { get; } = new List<IdentityUserRole<string>>();
        
        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; } = new List<IdentityUserClaim<string>>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; } = new List<IdentityUserLogin<string>>();
    }
}