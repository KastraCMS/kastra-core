using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class RoleModel
    {
        public RoleModel()
        {
            Permissions = new List<string>();
        }

        /// <summary>
        /// Role Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// Role name
        /// </summary>
        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Permission Id list
        /// </summary>
        public IList<string> Permissions { get; set; }
    }
}
