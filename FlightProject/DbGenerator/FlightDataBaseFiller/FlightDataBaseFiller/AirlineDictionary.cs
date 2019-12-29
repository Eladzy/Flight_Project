using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDataBaseFiller
{
    class AirlineDictionary
    {
       static string[] prefix=new string[] {"Air","Vesper","Jet","Wings","Sky","Star","Fly","United","Delta","Alpha","Gamma","Omega","Airway","Top","Aviation" ,"Aero","Flight"};
       static Random rnd = new Random();
        public static string GetPrefix()
        {
            return prefix[rnd.Next(0, prefix.Length)];
        }
    }
}
