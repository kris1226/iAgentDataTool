using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iAgentTool.DataLayer
{
    public class SwitchMultiConnection
    {
        private static BooleanSwitch Switch =
            new BooleanSwitch("Provider", "Supports switching between data providers");
        public static IDbConnection GetConnection()
        {
            if (Switch.Enabled)
            {
                return new OleDbConnection(GetConnectionString());
            }
            else
            {
                return new SqlConnection(GetConnectionString());
            }
        }
        public static string GetConnectionString()
        {
            if (Switch.Enabled)
            {
                return @"Provider=Microsoft.Jet.OLEDB.4.0;" +
                       @"Data Source=G:\dev\Outputs from Rules Engine" +
                       @"\HAIRULES.mdb;" +
                       @"Persist Security Info=False";
            }
            else
            {
                return @"Data Source=localhost;Initial Catalog=iAgentRemix; Integrated Security=true";
            }
        }
        public static string GetSqlConnectionString()
        {
            if (Switch.Enabled)
            {
                return @"Data Source=localhost;Initial Catalog=iAgentRemix; Integrated Security=true";
            }
            else
            {
                throw new Exception("Male sure you have a MS Sql Server instance available.");
            }
        }
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
        public static IDbConnection Connect()
        {
            var connection = GetConnection();
            connection.Open();
            return connection;
        }
    }
}
