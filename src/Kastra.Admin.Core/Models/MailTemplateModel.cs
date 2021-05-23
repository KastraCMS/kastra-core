using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class MailTemplateModel
    {
        public int Id { get; set; }

        [Display(Name = "key name")]
        public string Keyname { get; set; }

        [Required]
        [Display(Name = "subject")]
        [StringLength(50)]
        public string Subject { get; set; }

        [Required]
        [Display(Name = "message")]
        public string Message { get; set; }
    }
}
