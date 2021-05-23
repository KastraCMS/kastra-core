using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class PageModel
    {
        /// <summary>
        /// Page Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Page title
        /// </summary>
        [Required]
        [StringLength(150)]
        public string Title { get; set; }
        
        /// <summary>
        /// Page key name
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Keyname { get; set; }

        /// <summary>
        /// Template Id
        /// </summary>
        [Required]
        [Display(Name = "template")]
        public int? TemplateId { get; set; }

        /// <summary>
        /// Meta keywords
        /// </summary>
        [StringLength(250)]
        public string MetaKeywords { get; set; }

        /// <summary>
        /// Meta description
        /// </summary>
        [StringLength(160)]
        public string MetaDescription { get; set; }

        /// <summary>
        /// Meta robot
        /// </summary>
        [StringLength(50)]
        public string MetaRobot { get; set; }
    }
}
