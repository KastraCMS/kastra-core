using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kastra.Admin.Core.Models
{
    public class ModuleModel
    {
        public ModuleModel()
        {
            Permissions = new List<string>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "module name")]
        public string Name { get; set; }

        [Required]
        public int? PageId { get; set; }

        [Required]
        [Display(Name = "module definition")]
        public int? DefinitionId { get; set; }

        [Required]
        [Display(Name = "place")]
        public int? PlaceId { get; set; }
        
        public string PageName { get; set; }
        
        public bool IsStatic { get; set; }
        
        public bool IsDisabled { get; set; }

        public NavigationModel SettingsNavigation { get; set; }
        
        public IList<string> Permissions { get; set; }
    }
}
