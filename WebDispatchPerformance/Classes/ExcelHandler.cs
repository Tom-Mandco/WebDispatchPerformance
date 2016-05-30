﻿namespace MCO.Applications.WebDispatchPerformance.Classes
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

        public ExcelHandler(ILog logger, IExcelWriter excelWriter, IExcelCreater excelCreater)
        {
            this.logger = logger;
            this.excelWriter = excelWriter;
            this.excelCreater = excelCreater;
        }
        #endregion


        #region Main Functions
        public void writeToExcel()
        {
            logger.Info("Excel Hander - Write to Excel: Started");
            if (!DoesExcelFileExist())
            {
                //excelCreater.CreateNewSpreadsheet();
            }
            excelWriter.WriteDataWithPivot();
            //excelWriter.WriteToExcel();
        }
        #endregion

        #region Utilities
        bool DoesExcelFileExist()
        {
            return File.Exists(String.Format("{0}{4}-{5}-{6} {1} - ({2}){3}",
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
