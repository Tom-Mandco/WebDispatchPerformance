namespace MCO.Applications.WebDispatchPerformance.Classes
{
    using ClosedXML.Excel;
    using Interfaces;
    using MCO.Data.WebDispatchPerformance.Interfaces;
    using MCO.Data.WebDispatchPerformance.Models;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;

    class ExcelWriter : IExcelWriter
    {
        #region Initialization
        private readonly IRepository repository;
        private readonly IDataHandler dataHandler;
        private readonly ILog Logger;
        private DataTable dispatchDetails;

        public ExcelWriter(ILog Logger, IRepository repository, IDataHandler dataHandler, DataTable dispatchDetails)
        {
            this.Logger = Logger;
            this.repository = repository;
            this.dataHandler = dataHandler;
            this.dispatchDetails = dispatchDetails;
        }
        #endregion

        #region Main
        public void WriteToExcel()
        {
            Logger.Info("Writing to Excel");
            XLWorkbook workbook = new XLWorkbook(ConfigurationManager.AppSettings["ExcelFileTemplate"]);
            Logger.Trace("Using template: {0}", ConfigurationManager.AppSettings["ExcelFileTemplate"]);

            dispatchDetails = dataHandler.BindDispatchDetails();
            Logger.Trace("DataTable complete, found {0} rows", dispatchDetails.Rows.Count);

            var worksheet = workbook.Worksheet("Data");

            worksheet.Clear();
            Logger.Trace("Data worksheet cleared");

            worksheet.Cell(1, 1).InsertTable(dispatchDetails, "dispatchDetails", false);
            Logger.Trace("Data worksheet populated");

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(String.Format("{0}{4}-{5}-{6} {1} - {2}{3}",
                    ConfigurationManager.AppSettings["ExcelFilePath"],
                    ConfigurationManager.AppSettings["ExcelFileName"],
                    ConfigurationManager.AppSettings["RunLevel"],
                    ConfigurationManager.AppSettings["FileExtension"],
                    System.DateTime.Now.Day,
                    System.DateTime.Now.Month,
                    System.DateTime.Now.Year));
            Logger.Info("Workbook saved in:{7}{0}{4}-{5}-{6} {1} - {2}{3}",
                    ConfigurationManager.AppSettings["ExcelFilePath"],
                    ConfigurationManager.AppSettings["ExcelFileName"],
                    ConfigurationManager.AppSettings["RunLevel"],
                    ConfigurationManager.AppSettings["FileExtension"],
                    System.DateTime.Now.Day,
                    System.DateTime.Now.Month,
                    System.DateTime.Now.Year,
                    Environment.NewLine);
        }
        #endregion
    }
}
