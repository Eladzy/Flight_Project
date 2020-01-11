﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FlightManagerProject
{
    public class ErrorLogger
    {
        /// <summary>
        /// Logs Error messages and stack trace+timestamp
        /// </summary>

        private static string Path = ConfigurationUtils.errorLog;
       
        public static void Logger(Exception e)//TODO improve to a daily report
        {
            
            Task.Run(() =>
            {
                
                if (!File.Exists(Path))
                {
                    File.Create(Path);
                }
                using (StreamWriter writer = File.AppendText(Path))
                {
                    writer.WriteLine("==========Error Log==========");
                    writer.WriteLine($"====={DateTime.Now}=====");
                    writer.WriteLine("Error Message: " + e.Message);
                    writer.WriteLine("Stack Trace: " + e.StackTrace);
                    writer.WriteLine("==========End==========");
                }
            });
        }
    }
}