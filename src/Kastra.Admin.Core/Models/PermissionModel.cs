using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class PermissionModel
    {
        /// <summary>
        /// Permission Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Permission name
        /// </summary>
        [Required]
        [StringLength(150)]
        public string Name { get; set; }
    }
}
