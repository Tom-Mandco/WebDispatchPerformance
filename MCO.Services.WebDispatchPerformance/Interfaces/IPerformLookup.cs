using MCO.Data.WebDispatchPerformance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCO.Services.WebDispatchPerformance.Interfaces
{
    public interface IPerformLookup
    {
        IEnumerable<DispatchDetails> GetLastWeeksDispatchDetail();
    }
}
