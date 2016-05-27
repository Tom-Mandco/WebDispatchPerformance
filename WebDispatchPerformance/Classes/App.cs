namespace MCO.Applications.WebDispatchPerformance.Classes
{
    using Interfaces;
    using MCO.Data.WebDispatchPerformance.Models;
    using MCO.Services.WebDispatchPerformance.Interfaces;
    using System.Collections.Generic;

    class App : IApp
    {
        #region Initialization
        private readonly ILog Logger;
        private IEnumerable<DispatchDetails> dispatchDetails;
        private IPerformLookup performLookup;

        public App(ILog Logger, IPerformLookup performLookup, IEnumerable<DispatchDetails> dispatchDetails)
        {
            this.dispatchDetails = dispatchDetails;
            this.Logger = Logger;
        }
        #endregion

        #region Main Classes
        public void Run()
        {
            dispatchDetails = performLookup.GetLastWeeksDispatchDetail();

        }
        #endregion
    }

}
