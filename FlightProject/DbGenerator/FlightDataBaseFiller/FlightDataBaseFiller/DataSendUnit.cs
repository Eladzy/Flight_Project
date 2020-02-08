using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    public static class DataSendUnit
    {
       static DataSendUnit()
       {

       }
         public static Task AddData(List<Customer> customers, List<AirLine> airLines, List<Country> countries, List<Flight> flights, List<Ticket> tickets)
         {
            Task StartAddToDb = new Task(() => {
                Console.WriteLine("add to db will be here!"); 
            });
            //StartAddToDb.Start();
            return StartAddToDb;
         }
    }
}
