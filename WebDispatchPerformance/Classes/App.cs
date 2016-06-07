namespace MCO.Applications.WebDispatchPerformance.Classes
{
    using Interfaces;
    using MCO.Data.WebDispatchPerformance.Models;
    using MCO.Services.WebDispatchPerformance.Interfaces;
    using System;
    using System.Collections.Generic;

    class App : IApp
    {
        #region Initialization
        private readonly ILog Logger;
        private readonly IProcessHandler processHandler;
        private IEnumerable<DispatchDetails> dispatchDetails;

        public App(ILog Logger, IEnumerable<DispatchDetails> dispatchDetails, IProcessHandler processHandler)
        {
            this.dispatchDetails = dispatchDetails;
            this.Logger = Logger;
            this.processHandler = processHandler;
        }
        #endregion

        #region Main Classes
        public void Run()
        {
            try
            {
                Logger.Info("Starting run of Data Handler");
                processHandler.ProcessDataToExcel();
                Logger.Info("Data Handler run successfully.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                Logger.Error(ex.StackTrace);
            }
            Logger.Info("Program Ending{0}",
                        Environment.NewLine);
        }
        #endregion
    }

}
