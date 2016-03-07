using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace iAgentTool.DataLayer
{
    public class DB
    {
        //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings)
        public static string ConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["AgentProdDb"].ConnectionString.ToString();
                
                //Ability to modify connection string property
                var sb = new SqlConnectionStringBuilder(connectionString);
                sb.ApplicationName = ApplicationName ?? sb.ApplicationName;
                sb.ConnectTimeout = (ConnectionTimeout > 0) ? ConnectionTimeout : sb.ConnectTimeout;

                return sb.ToString();
            }
        }
        public static string SmartAgentDevConnectionString
        {
            get
            {
                var conn = ConfigurationManager.ConnectionStrings["SmartAgentDev"].ConnectionString.ToString();
                var sb = new SqlConnectionStringBuilder(conn);
                sb.ApplicationName = ApplicationName ?? sb.ApplicationName;
                sb.ConnectTimeout = (ConnectionTimeout > 0) ? ConnectionTimeout : sb.ConnectTimeout;
                return sb.ToString();
            }
        }
        public static string AgentDevConnectionString
        {
            get
            {
                var connectionString = ConfigurationManager.ConnectionStrings["AgentDevDb"].ConnectionString.ToString();

                //Ability to modify connection string property
                var sb = new SqlConnectionStringBuilder(connectionString);
                sb.ApplicationName = ApplicationName ?? sb.ApplicationName;
                sb.ConnectTimeout = (ConnectionTimeout > 0) ? ConnectionTimeout : sb.ConnectTimeout;

                return sb.ToString();
            }
        }
        /// <summary>
        ///Returns a open connection to the caller
        /// </summary>
        public static SqlConnection GetSqlConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();
            return conn;
        }
        public static SqlConnection GetDevAgentConnection()
        {
            SqlConnection conn = new SqlConnection(AgentDevConnectionString);
            conn.Open();
            return conn;
        }
        public static SqlConnection GetSmartAgentDevConnection()
        {
            var conn = new SqlConnection(SmartAgentDevConnectionString);
            conn.Open();
            return conn;
        }
        public static SqlConnection GetAsyncSqlConnection()
        {
            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.OpenAsync();
            return conn;
        }

        public static int ConnectionTimeout { get; set; }
            /// <summary>
            /// Modify the connection String
            /// Property used to override the name of the application
            /// </summary>
        public static string ApplicationName
        {
            get;
            set;
        }
    }
}
