using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataBaseFiller
{
    public class GeneralDataGenerator
    {
        private static Random rnd;
        private static object Key = new object();
        private static string Chars
        {
            get
            {
                return "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            }
        }
        private static string Digits
        {
            get
            {
                return "0123456789";
            }
        }
        public static string Generator(int length)
        {
            rnd = new Random();
            string dataString=string.Empty;
            for (int i = 0; i < length; i++)
            {
                dataString += Chars[rnd.Next(0, Chars.Length)];
            }
            return dataString;
        }
        public static string NumericGenerator(int length)
        {
            lock (Key)
            {
                
                rnd = new Random();
                string dataString = string.Empty;
                for (int i = 0; i < length; i++)
                {
                    dataString += Digits[rnd.Next(0, Digits.Length)];
                }
                return dataString;
            }
        }
    }
}
