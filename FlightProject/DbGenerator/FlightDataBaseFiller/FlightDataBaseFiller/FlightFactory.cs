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
        private long ID;
        private int RemainingTickets=Int32.Parse(ConfigurationManager.AppSettings["TicketsPerFlight"]);
        private DateTime DepartureTime;
        private DateTime LandTime;
       

       

        private void Init()
        {
            this.ID = long.Parse( GeneralDataGenerator.NumericGenerator(18));
            Random rnd = new Random();
            this.DepartureTime = DateTime.Now.AddDays(rnd.Next(0, 5)).AddHours(rnd.Next(0, 10));
            this.LandTime = this.DepartureTime.AddHours(rnd.Next(0, 5));
            
        }

        public Flight Generate(AirLine airline, Country c1, Country c2)
        {
            Init();
            Flight flight = new Flight
            {
                Id = this.ID,
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
