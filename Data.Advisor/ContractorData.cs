using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Advisor.Entities;
using Dapper;

namespace Data.Advisor
{
    public  class ContractorData
    {
        private static readonly string ApplicationConnectionStringName;
        private static readonly string ApplicationConnectionStringNameReadOnly;

        static ContractorData()
        {
            ApplicationConnectionStringName =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            ApplicationConnectionStringNameReadOnly =
                ConfigurationManager.ConnectionStrings["DefaultConnectionReadOnly"].ConnectionString;

        }


        public static List<Contractor> GetContractors(int categoryId)
        {
            const string sprocName = "dbo.GetActiveContractors";
            using (var sqlConn = ConnectionManager.GetConnection(ApplicationConnectionStringName, ApplicationConnectionStringNameReadOnly, sprocName))
            {
                return sqlConn.Query<Contractor>(sprocName, new {categoryId}, commandType: CommandType.StoredProcedure).ToList();
            }
        }


        public static Contractor GetContractor(int contractorId)
        {
            const string sprocName = "dbo.GetContractorById";
            using (var sqlConn = ConnectionManager.GetConnection(ApplicationConnectionStringName, ApplicationConnectionStringNameReadOnly, sprocName))
            {
                return sqlConn.Query<Contractor>(sprocName, new { contractorId }, commandType: CommandType.StoredProcedure).Single();
            }
        }
    }
}
