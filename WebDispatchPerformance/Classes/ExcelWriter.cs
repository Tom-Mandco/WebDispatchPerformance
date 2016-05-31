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
        private IRepository repository;
        private IDataHandler dataHandler;
        private DataTable dispatchDetails;

        public ExcelWriter(IRepository repository, IDataHandler dataHandler, DataTable dispatchDetails)
        {
            this.repository = repository;
            this.dataHandler = dataHandler;
            this.dispatchDetails = dispatchDetails;
        }
        #endregion

        #region Main
        public void WriteToExcel()
        {
            XLWorkbook workbook = new XLWorkbook(ConfigurationManager.AppSettings["ExcelFileTemplate"]);

            dispatchDetails = dataHandler.BindDispatchDetails();
            var worksheet = workbook.Worksheet("Data");

            worksheet.Clear();
            worksheet.Cell(1, 1).InsertTable(dispatchDetails, "dispatchDetails", false);

            worksheet.Columns().AdjustToContents();

            workbook.SaveAs(String.Format("{0}{4}-{5}-{6} {1} - {2}{3}",
                    ConfigurationManager.AppSettings["ExcelFilePath"],
                    ConfigurationManager.AppSettings["ExcelFileName"],
                    ConfigurationManager.AppSettings["RunLevel"],
                    ConfigurationManager.AppSettings["FileExtension"],
                    System.DateTime.Now.Day,
                    System.DateTime.Now.Month,
                    System.DateTime.Now.Year));
        }
        #endregion
    }
}
