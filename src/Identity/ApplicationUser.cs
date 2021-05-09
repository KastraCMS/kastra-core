/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Kastra.Core.Identity
{
    /// <summary>
    /// Add profile data for application users by adding properties to the ApplicationUser class
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
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
        public virtual ICollection<IdentityUserRole<Guid>> Roles { get; } = new List<IdentityUserRole<Guid>>();
        
        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityUserClaim<Guid>> Claims { get; } = new List<IdentityUserClaim<Guid>>();

        /// <summary>
        /// Navigation property for this users login accounts.
        /// </summary>
        public virtual ICollection<IdentityUserLogin<Guid>> Logins { get; } = new List<IdentityUserLogin<Guid>>();
    }
}