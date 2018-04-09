using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  System.Configuration;
using System.Data;
using Common.Advisor.Entities;
using Dapper;

namespace Data.Advisor
{
    public class CategoryData
    {
        private static readonly string ApplicationConnectionStringName;
        private static readonly string ApplicationConnectionStringNameReadOnly;

        static CategoryData()
        {
            ApplicationConnectionStringName =
                ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            ApplicationConnectionStringNameReadOnly =
                ConfigurationManager.ConnectionStrings["DefaultConnectionReadOnly"].ConnectionString;

        }


        public static List<Category> GetCategories()
        {
            const string sprocName = "dbo.GetCategories";
            using (var sqlConn = ConnectionManager.GetConnection(ApplicationConnectionStringName, ApplicationConnectionStringNameReadOnly, sprocName))
            {
                return sqlConn.Query<Category>(sprocName, null, commandType: CommandType.StoredProcedure).ToList();
            }
        }

    }

    
}
