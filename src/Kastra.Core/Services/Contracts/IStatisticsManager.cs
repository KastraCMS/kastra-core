/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/KastraCMS/kastra-core for more information concerning
 * the license and the contributors participating to this project.
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kastra.Core.DTO;

namespace Kastra.Core.Services.Contracts
{
    public interface IStatisticsManager
    {
        /// <summary>
        /// Saves the visitor.
        /// </summary>
        /// <returns><c>true</c>, if visitor was saved, <c>false</c> otherwise.</returns>
        /// <param name="visitor">Visitor.</param>
        Task<bool> SaveVisitorAsync(VisitorInfo visitor);

        /// <summary>
        /// Counts the visits in a period.
        /// </summary>
        /// <returns>The visits from to.</returns>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        Task<int> CountVisitsFromToAsync(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets the visits by user identifier.
        /// </summary>
        /// <returns>The visits by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        Task<IList<VisitorInfo>> GetVisitsByUserIdAsync(Guid userId);

        /// <summary>
        /// Gets the visits from date.
        /// </summary>
        /// <returns>The visits from a date to a date</returns>
        /// <param name="fromDate">From date.</param>
        Task<IList<VisitorInfo>> GetVisitsFromDateAsync(DateTime fromDate, DateTime toDate);
    }
}
