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
    public class ApplicationRole : IdentityRole<Guid>
    {
        /// <summary>
        /// Navigation property for the claims this user possesses.
        /// </summary>
        public virtual ICollection<IdentityRoleClaim<Guid>> Claims { get; } = new List<IdentityRoleClaim<Guid>>();
    }
}
