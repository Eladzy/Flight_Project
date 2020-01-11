﻿using System;
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
        public static int interval=250;
        public static string errorLog = @"Resources\ErrorLog.txt";
        
        
       
    }
}