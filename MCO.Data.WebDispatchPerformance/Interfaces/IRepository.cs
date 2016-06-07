using MCO.Data.WebDispatchPerformance.Models;
using System.Collections.Generic;

namespace MCO.Data.WebDispatchPerformance.Interfaces
{
    public interface IRepository
    {
        IEnumerable<DispatchDetails> GetLastWeekWebDispatchDetails();
        IEnumerable<ReturnDetails> GetLastWeekWebReturnDetails();
    }
}
