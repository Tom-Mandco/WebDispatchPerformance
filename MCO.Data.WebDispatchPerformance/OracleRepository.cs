using MCO.Data.WebDispatchPerformance.Interfaces;
using MCO.Data.WebDispatchPerformance.Models;
using System.Collections.Generic;
using System.Linq;

namespace MCO.Data.WebDispatchPerformance
{
    public class OracleRepository : OracleBase, IRepository
    {
        #region Set Connection String
        public OracleRepository(string connectionString)
            : base(connectionString)
        {
        }
        #endregion

        #region Return Model Data
        public IEnumerable<DispatchDetails> GetLastWeekWebDispatchDetails()
        {
            using (new SharedConnection(dbConnection))
            {
                var result = dbConnection.Query<DispatchDetails>(SqlLoader.GetSql("web despatch package peerformance"));
                return result.Any() ? result : null;
            }
        }
        #endregion

        public string getConnectionStringFromOR()
        {
            using (new SharedConnection(dbConnection))
            {
                return dbConnection.Connection.ConnectionString.ToString();
            }
        }
    }
}