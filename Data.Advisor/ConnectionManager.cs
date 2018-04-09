using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Advisor;
using Microsoft.Win32;

namespace Data.Advisor
{
    public class ConnectionManager
    {
       

      
        private static readonly bool IsDatabaseOnHaCluster;

        private static ConcurrentDictionary<string, ConnectionContainer> ConnectionStrings { get; set; }



        static ConnectionManager()
        {
            ConnectionStrings = new ConcurrentDictionary<string, ConnectionContainer>();
            LoadConnectionStrings();
          
            IsDatabaseOnHaCluster = ConfigurationManager.AppSettings["IsDatabaseOnHACluster"].ToBool(false);

           
        }

        /// <summary>
        /// This method will load the connection string from app/web config.. If encrypted, it will decrypt it otherwise it will encrypt it and update the file for future use.
        /// The encryption keys will be stored in the registry @ HKEY_LOCAL_MACHINE\SOFTWARE\{ApplicationName}\{CustomerName}\..keys
        /// </summary>
        /// <param name="connectionStringName">the connection string to pick up from app/web config</param>
        /// <returns></returns>
        public static string GetConnectionString(string connectionStringName)
        {
            if (ConfigurationManager.ConnectionStrings[connectionStringName] == null)
            {
                throw new Exception("Invalid connection string name");
            }

         
            var connectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
           
            return connectionString;
           

        }

        /// <summary>
        /// This method will load the connection from app/web config.. If encrypted, it will decrypt it otherwise it will encrypt it and update the file for future use.
        /// The encryption keys will be stored in the registry @ HKEY_LOCAL_MACHINE\SOFTWARE\{ApplicationName}\{CustomerName}\..keys
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns></returns>
        public static IDbConnection GetConnection(string connectionName)
        {
            var databaseConnectionString = GetConnectionString(connectionName);
            IDbConnection connection = new SqlConnection(databaseConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return connection;
        }
       

        /// <summary>
        /// get a connection-string using name and a stored proc name to check for read-only replica catalog.
        /// </summary>
        /// <param name="connectionStringName">Name of connection string to get</param>
        /// <param name="readOnlyReplicaConnectionStringName">Name of connection string to get</param>
        /// <param name="storedProcName">a stored-procedure name to check against the read-only replica catalog</param>
        /// <returns>Connection as string</returns>
        public static string GetConnectionString(string connectionStringName, string readOnlyReplicaConnectionStringName, string storedProcName)
        {
            var connectionString = IsDatabaseOnHaCluster && ReadOnlyReplicaCatalog.IsElligible(storedProcName) ? GetConnectionString(readOnlyReplicaConnectionStringName, true) : GetConnectionString(connectionStringName, false);

            if (string.IsNullOrEmpty(connectionString))
            {
                connectionString = GetConnectionString(connectionStringName, false);
            }

            return connectionString;
        }

        public static IDbConnection GetConnection(string connectionStringName, string readOnlyReplicaConnectionStringName, string storedProcName)
        {
            var databaseConnectionString = GetConnectionString(connectionStringName, readOnlyReplicaConnectionStringName, storedProcName);
            IDbConnection connection = new SqlConnection(databaseConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return connection;
        }


        /// <summary>
        /// Get a connection-string based on connection-string-name and a flag indicating whether it will
        /// </summary>
        /// <param name="connectringName">Name of connection string to get</param>
        /// <param name="readOnlyReplica">A flag indicating if read-only-replica has to be used</param>
        /// <returns>Connection as string</returns>
        public static string GetConnectionString(string connectringName, bool readOnlyReplica)
        {
            if (ConnectionStrings == null || ConnectionStrings.IsEmpty)
            {
                LoadConnectionStrings();
                if (ConnectionStrings == null || ConnectionStrings.IsEmpty)
                {
                    return string.Empty;
                }
            }

            return connectringName;
        }

        private static void LoadConnectionStrings()
        {
            lock (ConnectionStrings)
            {
                if (ConnectionStrings == null)
                {
                    ConnectionStrings = new ConcurrentDictionary<string, ConnectionContainer>();
                }

                foreach (ConnectionStringSettings connectionString in ConfigurationManager.ConnectionStrings.Cast<ConnectionStringSettings>().Where(connectionString => connectionString != null))
                {
                    ConnectionStrings.TryAdd
                        (
                            connectionString.Name,
                            new ConnectionContainer { ConnectionName = connectionString.Name, ConnectionString = connectionString.ConnectionString, ReadOnlyReplica = ForReadOnlyReplica(connectionString) }
                        );
                }
            }
        }

        private static bool ForReadOnlyReplica(ConnectionStringSettings connectionString)
        {
            if (connectionString == null || string.IsNullOrEmpty(connectionString.ConnectionString))
            {
                return false;
            }

            var attributes = connectionString.ConnectionString.ToLower().Split(';');

            return (from att in attributes
                    where !string.IsNullOrEmpty(att) && att.IndexOf("applicationintent", StringComparison.CurrentCultureIgnoreCase) >= 0
                    select att.IndexOf("readonly", StringComparison.CurrentCultureIgnoreCase) > 0).FirstOrDefault();
        }

        internal class ConnectionContainer
        {
            public bool ReadOnlyReplica { get; set; }

            public string ConnectionString { get; set; }

            public string ConnectionName { get; set; }
        }

    }
}
