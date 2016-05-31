namespace MCO.Applications.WebDispatchPerformance.Classes
{
    using Interfaces;
    using Data.WebDispatchPerformance.Models;
    using System.Collections.Generic;
    using Services.WebDispatchPerformance;
    using Services.WebDispatchPerformance.Interfaces;
using System.Data;
    using System;

    class DataHandler : IDataHandler
    {
        private readonly ILog Logger;
        private readonly IPerformLookup performLookup;
        private IEnumerable<DispatchDetails> dispatchDetails;

        public DataHandler(ILog Logger, IPerformLookup performLookup, IEnumerable<DispatchDetails> dispatchDetails)
        {
            this.Logger = Logger;
            this.performLookup = performLookup;
            this.dispatchDetails = dispatchDetails;
        }

        public DataTable BindDispatchDetails()
        {
            DataTable resultDT = new DataTable();

            dispatchDetails = performLookup.GetLastWeeksDispatchDetail();

            if (dispatchDetails != null)
            {
                #region Set Columns
                resultDT.Columns.Add("Dispatch Day");
                resultDT.Columns.Add("Day of Week");
                resultDT.Columns.Add("Dispatch By");
                resultDT.Columns.Add("Start Time");
                resultDT.Columns.Add("Orders", typeof(int));
                resultDT.Columns.Add("Quantity", typeof(int));
                #endregion

                foreach (var detail in dispatchDetails)
                {
                    resultDT.Rows.Add(detail.DESPATCH_DAY.Replace("_", " "),
                                      detail.DAYOFWEEK,
                                      detail.DESPATCH_BY,
                                      detail.START_TIME,
                                      detail.ORDS,
                                      detail.QTY);
                }
            }
            else
            {
            }
            return resultDT;
        }
    }
}
