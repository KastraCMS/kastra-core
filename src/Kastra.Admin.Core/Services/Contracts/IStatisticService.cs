using Kastra.Admin.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kastra.Admin.Core.Services.Contracts
{
    public interface IStatisticService
    {
        /// <summary>
        /// Load global statistics.
        /// </summary>
        /// <returns>GlobalStatsModel</returns>
        Task<GlobalStatsModel> LoadGlobalStatistics();

        /// <summary>
        /// Load the recent user list.
        /// </summary>
        /// <param name="index">Page index</param>
        /// <returns>IList<UserModel></returns>
        Task<IList<UserModel>> LoadRecentUserList(int index = 0);

        /// <summary>
        /// Load the visitor list.
        /// </summary>
        /// <param name="index">Page index</param>
        /// <returns>IList<VisitModel></returns>
        Task<IList<VisitModel>> LoadVisitorList(int index = 0);

        /// <summary>
        /// Load the data for the user graphic.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>ChartModel</returns>
        Task<ChartModel> LoadGraphData(int index = 0);
    }
}
