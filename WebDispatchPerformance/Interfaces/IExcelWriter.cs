using MCO.Data.WebDispatchPerformance.Models;
using System.Collections.Generic;
namespace MCO.Applications.WebDispatchPerformance.Interfaces
{
    interface IExcelWriter
    {
        void WriteToExcel();
        void WriteDataWithPivot();
    }
}
