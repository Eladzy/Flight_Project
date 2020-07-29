using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;

namespace FlightManagerProject
{
    public class ErrorLogger
    {
        /// <summary>
        /// Logs Error messages and stack trace+timestamp
        /// </summary>

        private static readonly string Path = ConfigurationUtils.errorLog;//ConfigurationManager.AppSettings["errorLog"];
        private static readonly object key = new object();

        public static void Logger(Exception e)//TODO improve to a daily report
        {

            lock (key)
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
            }
        }
    }
}
