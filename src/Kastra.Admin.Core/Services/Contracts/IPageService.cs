using Kastra.Admin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IPageService
    {
        Task<PageModel> LoadPage(int pageId);

        Task<bool> SavePage(PageModel page);

        Task<IList<PageModel>> LoadPageList();

        Task<bool> DeletePage(int pageId);
    }
}
