using Kastra.Admin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface ITemplateService
    {
        Task<IList<TemplateModel>> LoadTemplateList();

        Task<IList<string>> LoadTemplateViewList(string viewPath);

        Task<IList<string>> LoadTemplateControllerList();

        Task<TemplateModel> LoadTemplate(int templateId);

        Task<bool> SaveTemplate(TemplateModel template);

        Task<bool> DeleteTemplate(int templateId);
    }
}
