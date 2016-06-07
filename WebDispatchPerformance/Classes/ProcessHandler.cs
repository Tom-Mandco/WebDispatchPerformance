namespace MCO.Applications.WebDispatchPerformance.Classes
{
    using MCO.Applications.WebDispatchPerformance.Interfaces;

    class ProcessHandler : IProcessHandler
    {
        private readonly IExcelHandler excelHandler;

        public ProcessHandler(IExcelHandler excelHandler)
        {
            this.excelHandler = excelHandler;
        }

        public void ProcessDataToExcel()
        {
            excelHandler.writeToExcel();
        }
    }
}
