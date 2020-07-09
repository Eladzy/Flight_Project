using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    static class ConfigurationUtils
    {
        /// <summary>
        /// holds app crucial data
        /// </summary>
        public static string connectionString =
            "Data Source=.;Initial Catalog = FlightCom; Integrated Security = True";
        public static string adminUserName = "Admin";
        public static string adminPassword = "9999";
        public static int updateHour = 12;
        public static int CLEAN_DB_INTERVAL = 1000 * 60 * 60 * 4;
        //public static int CLEAN_DB_INTERVAL = 1000 * 60 ;
        public static string errorLog = @"D:\git\Flight_Project\FlightProject\CoreControls\FlightManagerProject\Resurces/ErrorLogger.txt";
      
        
       
    }
}
