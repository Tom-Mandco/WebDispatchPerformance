using MCO.Data.WebDispatchPerformance.Models;
using System.Collections.Generic;

namespace MCO.Applications.WebDispatchPerformance.Interfaces
{
    interface IDataHander
    {
        public void ProcessDataToExcel(IEnumerable<DispatchDetails> dispatchDetails);
    }
}
