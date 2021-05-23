using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Username { get; set; }

        [Required]
        [StringLength(150)]
        public string Password { get; set; }
        
        public bool RememberMe { get; set; }
    }
}
