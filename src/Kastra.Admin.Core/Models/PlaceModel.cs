using System.Collections.Generic;

namespace Kastra.Admin.Core.Models
{
    public class PlaceModel
    {
        public int Id { get; set; }
        public string Keyname { get; set; }
        public int TemplateId { get; set; }
        public int? ModuleId { get; set; }

        public IList<ModuleModel> PlaceModuleList { get; set; }
    }
}
