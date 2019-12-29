using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FlightManagerProject
{
     class DailyCLeanUpTool
    {
        private static string connectionString = ConfigurationUtils.connectionString;
        private static DailyCLeanUpTool _instance;
        private static object key = new object();
        /// <summary>
        /// clean up query
        /// </summary>
        public static void TimedCleanUp()
        {
            string query = "CLEAN_UP";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// single instance of a tool can be activated
        /// </summary>
        /// <returns></returns>
        public static DailyCLeanUpTool GetInstance()
        {
            if (_instance==null)
            {
                lock (key)
                {
                    if(_instance==null)
                    return _instance=new DailyCLeanUpTool();
                }
            }
            return _instance;
        }
        public void MoveToHistory()
        {
            TimedCleanUp();
        }
     }
}
