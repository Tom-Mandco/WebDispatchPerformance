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

        public ExcelHandler(ILog logger, IExcelWriter excelWriter)
        {
            this.logger = logger;
            this.excelWriter = excelWriter;
        }
        #endregion


        #region Main Functions
        public void writeToExcel()
        {
            logger.Info("Excel Hander - Write to Excel: Started");
            try
            {
                excelWriter.WriteToExcel();
            }
            catch(Exception ex)
            {
                logger.Error(ex.Message);
                logger.Error(ex.StackTrace);
            }
        }
        #endregion

        #region Utilities
        #endregion
    }
}
