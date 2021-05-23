using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class UserModel
    {
        public Guid? Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "username")]
        public string Username { get; set; }

        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "email")]
        public string Email { get; set; }

        [StringLength(150)]
        [Display(Name = "first name")]
        public string FirstName { get; set; }

        [StringLength(150)]
        [Display(Name = "last name")]
        public string LastName { get; set; }

        public string Password { get; set; }

        public IList<Guid> Roles { get; set; }
    }
}
