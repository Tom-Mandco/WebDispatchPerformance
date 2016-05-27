namespace MCO.Applications.WebDispatchPerformance.Classes
{
    using Interfaces;
using MCO.Applications.WebDispatchPerformance.Interfaces;
using MCO.Data.WebDispatchPerformance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

    class ExcelHandler : IExcelHandler
    {
        #region Initialization
        private readonly ILog logger;
        private IExcelWriter excelWriter;
        private IExcelCreater excelCreater;

        public ExcelHandler(ILog logger, IExcelWriter excelWriter)
        {
            this.logger = logger;
            this.excelWriter = excelWriter;
        }
        #endregion


        #region Main Functions
        public void writeToExcel(IEnumerable<DispatchDetails> dispatchDetails)
        {
            logger.Info("Excel Hander - Write to Excel: Started");
            if(dispatchDetails != null)
            {
                logger.Info("Dispatch Details passed successfully");
            }
            
            if (!DoesExcelFileExist())
            {
                excelCreater.CreateNewSpreadsheet();
            }

            excelWriter.WriteToExcel();
        }
        #endregion

        #region Utilities
        bool DoesExcelFileExist()
        {
            return File.Exists(String.Format("{0}{4}-{5}-{6} {1} - [{2}]{3}",
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
