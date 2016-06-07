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
        private IEnumerable<ReturnDetails> returnDetails;

        public DataHandler(ILog Logger, IPerformLookup performLookup, IEnumerable<DispatchDetails> dispatchDetails, IEnumerable<ReturnDetails> returnDetails)
        {
            this.Logger = Logger;
            this.performLookup = performLookup;
            this.dispatchDetails = dispatchDetails;
            this.returnDetails = returnDetails;
        }

        public DataTable BindDispatchDetails()
        {
            DataTable resultDT = new DataTable();

            dispatchDetails = performLookup.GetLastWeeksDispatchDetail();

            if (dispatchDetails != null)
            {
                #region Set Columns
                resultDT.Columns.Add("Dispatch Day", typeof(DateTime));
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

        public DataTable BindReturnDetails()
        {
            DataTable resultDT = new DataTable();

            returnDetails = performLookup.GetLastWeeksReturnsDetail();

            if (returnDetails != null)
            {
                #region Set Columns
                resultDT.Columns.Add("Return Day", typeof(DateTime));
                resultDT.Columns.Add("Day of Week");
                resultDT.Columns.Add("Returned By");
                resultDT.Columns.Add("Start Hour");
                resultDT.Columns.Add("Orders", typeof(int));
                resultDT.Columns.Add("Returned", typeof(int));
                resultDT.Columns.Add("Fit", typeof(int));
                resultDT.Columns.Add("Gala", typeof(int));
                resultDT.Columns.Add("Writeoff", typeof(int));
                resultDT.Columns.Add("Attention", typeof(int));
                #endregion

                foreach (var detail in returnDetails)
                {
                    resultDT.Rows.Add(detail.RETURN_DAY.Replace("_", " "),
                                      detail.DAYOFWEEK,
                                      detail.RETURNED_BY,
                                      String.Format("{0}:00", detail.START_HOUR),
                                      detail.NUMBER_OF_ORDERS,
                                      detail.QTY_RETURNED,
                                      detail.QTY_FIT,
                                      detail.QTY_GALA,
                                      detail.QTY_WRITEOFF,
                                      detail.QTY_ATTENTN);
                }
            }
            else
            {
            }
            return resultDT;
        }
    }
}
