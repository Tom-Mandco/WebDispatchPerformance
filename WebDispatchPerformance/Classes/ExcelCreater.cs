namespace MCO.Applications.WebDispatchPerformance.Classes
{
    using ClosedXML.Excel;
    using System.Configuration;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MCO.Applications.WebDispatchPerformance.Interfaces;

    public class ExcelCreater : IExcelCreater
    {
        private ILog logger;

        public ExcelCreater(ILog logger)
        {
            this.logger = logger;
        }

        public void CreateNewSpreadsheet()
        {
            logger.Info(String.Format("Excel Creater - Generating Spreadsheet{2} Path : {0}", ConfigurationManager.AppSettings["ExcelFilePath"], Environment.NewLine));

            DataTable dt = GetHeadersInTable();

            XLWorkbook xlwb = new XLWorkbook();
            xlwb.Worksheets.Add(dt, "Missed Orders");

            xlwb.SaveAs(String.Format("{0}{4}-{5}-{6} {1} - [{2}]{3}",
                    ConfigurationManager.AppSettings["ExcelFilePath"],
                    ConfigurationManager.AppSettings["ExcelFileName"],
                    ConfigurationManager.AppSettings["RunLevel"],
                    ConfigurationManager.AppSettings["FileExtension"],
                    System.DateTime.Now.Day,
                    System.DateTime.Now.Month,
                    System.DateTime.Now.Year));
        }

        List<string> GetTabHeaders()
        {
            List<string> result = new List<string>();

            result.Add("Dispatch Day");
            result.Add("Day of Week");
            result.Add("Dispatch By");
            result.Add("Start Time");
            result.Add("Orders");
            result.Add("Quantity");

            return result;
        }


        DataTable GetHeadersInTable()
        {
            List<string> list = GetTabHeaders();
            DataTable table = new DataTable();

            foreach (string s in list)
            {
                table.Columns.Add(s);
            }

            return table;
        }
    }
}
