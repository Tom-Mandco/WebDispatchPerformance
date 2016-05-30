using MCO.Applications.WebDispatchPerformance.Interfaces;

namespace MCO.Applications.WebDispatchPerformance.Classes
{
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
