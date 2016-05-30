using MCO.Data.WebDispatchPerformance.Models;
using System.Collections.Generic;
using System.Data;

namespace MCO.Applications.WebDispatchPerformance.Interfaces
{
    interface IDataHandler
    {
        DataTable BindDispatchDetails();
    }
}
