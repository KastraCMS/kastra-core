﻿using System;
using System.Collections.Generic;
using Kastra.Core.DTO;

namespace Kastra.Core.Business
{
    public interface IStatisticsManager
    {
        /// <summary>
        /// Counts the visits in a period.
        /// </summary>
        /// <returns>The visits from to.</returns>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        int CountVisitsFromTo(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Gets the visits by user identifier.
        /// </summary>
        /// <returns>The visits by user identifier.</returns>
        /// <param name="userId">User identifier.</param>
        IList<VisitorInfo> GetVisitsByUserId(string userId);

        /// <summary>
        /// Gets the visits from date.
        /// </summary>
        /// <returns>The visits from date.</returns>
        /// <param name="fromDate">From date.</param>
        IList<VisitorInfo> GetVisitsFromDate(DateTime fromDate);
    }
}
