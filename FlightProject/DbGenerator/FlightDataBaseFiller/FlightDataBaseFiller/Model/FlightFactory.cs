using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;
using System.Configuration;

namespace FlightDataBaseFiller
{
    class FlightFactory
    {
        private int RemainingTickets=Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]);
        private DateTime DepartureTime;
        private DateTime LandTime;
        private static object Key = new object();

       

        private void Init()
        {
            Random rnd = new Random();
            lock (Key)
            {
                this.DepartureTime = DateTime.Now.AddDays(rnd.Next(0, 5)).AddHours(rnd.Next(0, 10));
                this.LandTime = this.DepartureTime.AddHours(rnd.Next(1, 10));
                rnd = null;
            }
            
        }

        public Flight Generate(AirLine airline, Country c1, Country c2)
        {
            lock (Key)
            {
                Init();
                Flight flight = new Flight
                {
                    Id =Math.Abs( Guid.NewGuid().GetHashCode()),
                    AirLine_Id = airline.Id,
                    Origin_Country_Code = c1.Id,
                    Destination_Country_Code = c2.Id,
                    Departure_Time = this.DepartureTime,
                    Landing_Time = this.LandTime,
                    Remaining_Tickets = this.RemainingTickets
                };
                return flight;
            }
          
        }
            
    }
}
