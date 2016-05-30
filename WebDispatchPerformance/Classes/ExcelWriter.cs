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
        public void WriteDataWithPivot()
        {
            XLWorkbook workbook = new XLWorkbook();

            dispatchDetails = dataHandler.BindDispatchDetails();
            var worksheet = workbook.Worksheets.Add("Data");
            var source = worksheet.Cell(1, 1).InsertTable(dispatchDetails, "dispatchDetails", false);

            var range = source.DataRange;
            //var header = worksheet.Range(1, 1, 6, 1);
            var dataRange = worksheet.Range(range.FirstCell(), range.LastCell());

            //var ptSheet = workbook.Worksheets.Add("PivotTable");
            //var pt = ptSheet.PivotTables.AddNew("PivotTable", ptSheet.Cell(1, 1), dataRange);

            CreatePivot(workbook);

            worksheet.Columns().AdjustToContents();

            var numericRange = worksheet.Range("E:F");
            

            workbook.SaveAs(String.Format("{0}{4}-{5}-{6} {1} - {2}{3}",
                    ConfigurationManager.AppSettings["ExcelFilePath"],
                    ConfigurationManager.AppSettings["ExcelFileName"],
                    ConfigurationManager.AppSettings["RunLevel"],
                    ConfigurationManager.AppSettings["FileExtension"],
                    System.DateTime.Now.Day,
                    System.DateTime.Now.Month,
                    System.DateTime.Now.Year));
        }

        public void WriteToExcel()
        {
            XLWorkbook workbook = new XLWorkbook(String.Format("{0}{4}-{5}-{6} {1} - ({2}){3}",
                    ConfigurationManager.AppSettings["ExcelFilePath"],
                    ConfigurationManager.AppSettings["ExcelFileName"],
                    ConfigurationManager.AppSettings["RunLevel"],
                    ConfigurationManager.AppSettings["FileExtension"],
                    System.DateTime.Now.Day,
                    System.DateTime.Now.Month,
                    System.DateTime.Now.Year));

            var worksheet = workbook.Worksheet(ConfigurationManager.AppSettings["ExcelDataSheetName"]);

            int numberOfLastRow = worksheet.LastRowUsed().RowNumber();
            IXLCell cellForNewData = worksheet.Cell(numberOfLastRow + 1, 1);

            dispatchDetails = dataHandler.BindDispatchDetails();

            worksheet.Row(numberOfLastRow).InsertRowsBelow(dispatchDetails.Rows.Count);
            cellForNewData.InsertData(dispatchDetails.Rows);

            worksheet.Columns().AdjustToContents();

            workbook.Save();

            CreatePivot(workbook);
        }

        public void CreatePivot(XLWorkbook workbook)
        {
            var ws = workbook.Worksheets.Add("PivotTable");
            var dataSheet = workbook.Worksheet("Data");

            var lastRowIndex = dataSheet.LastRowUsed().RowNumber();
            var lastColumnIndex = dataSheet.LastColumnUsed().ColumnNumber();

            //ws.Cell("A1").Value = "Category";
            //ws.Cell("A2").Value = "A";
            //ws.Cell("A3").Value = "B";
            //ws.Cell("A4").Value = "B";
            //ws.Cell("B1").Value = "Number";
            //ws.Cell("B2").Value = 100;
            //ws.Cell("B3").Value = 150;
            //ws.Cell("B4").Value = 75;

            //ws.Cell("C1").Value = "Type";
            //ws.Cell("C2").Value = "Croissant";
            //ws.Cell("C3").Value = "Doughnut";
            //ws.Cell("C4").Value = "Bearclaw";
            //ws.Cell("D1").Value = "Month";
            //ws.Cell("D2").Value = "Apr";
            //ws.Cell("D3").Value = "May";
            //ws.Cell("D4").Value = "June";

            IXLRange wsRange = dataSheet.Range(1, 1, 3335, 6);

            var pivotTable = ws.PivotTables.AddNew("PivotTable", ws.Cell(1, 1), wsRange);

            pivotTable.RowLabels.Add("Despatch Day");
            pivotTable.RowLabels.Add("Day of Week");
            pivotTable.RowLabels.Add("Despatch By");



            pivotTable.Values.Add("Quantity");
            //pivotTable.Values.Add(XLPivotCalculation.RunningTotal.ToString());
            //pivotTable.Values.Add("Orders");

            pivotTable.ColumnLabels.Add("Start Time");

            ws.Columns().AdjustToContents();
        }

        private void GeneratePivotTable(XLWorkbook workbook)
        {
            IXLWorksheet pivotSheet = workbook.Worksheets.Add("Pivot Table");
            var dataSheet = workbook.Worksheet(ConfigurationManager.AppSettings["ExcelDataSheetName"]);
            var lastRowIndex = dataSheet.LastRowUsed().RowNumber();
            var lastColumnIndex = dataSheet.LastColumnUsed().ColumnNumber();

            var ptRange = dataSheet.Range(1, 1, lastRowIndex, lastColumnIndex);

            var dataRange = dataSheet.Range(ptRange.FirstCell(), ptRange.LastCell());

            IXLPivotTable xlPvTable = pivotSheet.PivotTables.AddNew("PivotTable", pivotSheet.Cell(1, 1), dataSheet.Range(1, 1, (lastRowIndex-1), lastColumnIndex));

            //xlPvTable.RowLabels.Add("Dispatch Day");
            //xlPvTable.RowLabels.Add("Day of Week");
            //xlPvTable.RowLabels.Add("Dispatch By");

            //var pt = pivotSheet.PivotTables.AddNew("Pivot Table", pivotSheet.Cell(1, 1), dataRange);

            //pt.RowLabels.Add("Dispatch Day");
            //pt.RowLabels.Add("Day of Week");
            //pt.RowLabels.Add("Dispatch By");

            //pt.ColumnLabels.Add("Start Time");

            //pt.Values.Add("Quantity");
            //pt.Values.Add("Orders");
        }
        #endregion
    }
}
