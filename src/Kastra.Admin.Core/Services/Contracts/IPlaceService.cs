using Kastra.Admin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IPlaceService
    {
        Task<IList<PlaceModel>> LoadPlaceList(int? pageId);
    }
}
