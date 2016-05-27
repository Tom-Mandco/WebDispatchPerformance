using System;
using System.Collections.Generic;
using MCO.Data.WebDispatchPerformance.Models;

namespace MCO.Services.WebDispatchPerformance
{
    using Interfaces;
    using MCO.Data.WebDispatchPerformance.Interfaces;

    public class PerformLookup : IPerformLookup
    {
        #region Initialization
        private readonly IRepository oracleRepository;
        private IEnumerable<DispatchDetails> dispatchDetails;


        public PerformLookup(IRepository oracleRepository, IEnumerable<DispatchDetails> dispatchDetails)
        {
            this.dispatchDetails = dispatchDetails;
            this.oracleRepository = oracleRepository;
        }
        #endregion

        #region Main Classes
        public IEnumerable<DispatchDetails> GetLastWeeksDispatchDetail()
        {
            dispatchDetails = oracleRepository.GetLastWeekWebDispatchDetails();
            return dispatchDetails;
        }
        #endregion

        #region functions

        #endregion
    }
}