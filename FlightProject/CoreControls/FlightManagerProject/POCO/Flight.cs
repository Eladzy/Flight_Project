using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class Flight : IPoco
    {
        public long Id { get; set; }
        public long AirLine_Id { get; set; }
        public long Origin_Country_Code { get; set; }
        public long Destination_Country_Code { get; set; }
        public DateTime Departure_Time { get; set; }
        public DateTime Landing_Time { get; set; }
        public int Remaining_Tickets { get; set; }
        public Flight()
        {

        }

        public Flight(long id, long airLine_Id, long origin_Country_Code, long destination_Country_Code, DateTime departure_Time, DateTime landing_Time, int remaining_Tickets)
        {
            Id = id;
            AirLine_Id = airLine_Id;
            Origin_Country_Code = origin_Country_Code;
            Destination_Country_Code = destination_Country_Code;
            Departure_Time = departure_Time;
            Landing_Time = landing_Time;
            Remaining_Tickets = remaining_Tickets;
        }

        public Flight( long airLine_Id, long origin_Country_Code, long destination_Country_Code, DateTime departure_Time, DateTime landing_Time, int remaining_Tickets)
        {
           
            AirLine_Id = airLine_Id;
            Origin_Country_Code = origin_Country_Code;
            Destination_Country_Code = destination_Country_Code;
            Departure_Time = departure_Time;
            Landing_Time = landing_Time;
            Remaining_Tickets = remaining_Tickets;
        }

        public static bool operator ==(Flight flight1, Flight flight2)
        {
            if (Equals(flight1, null) && Equals(flight2, null))
                return true;
            if (Equals(flight1, null) || Equals(flight2, null))
                return false;
            if (Equals(flight1.Id, flight2.Id))
                return true;
            return false;
        }
        public static bool operator !=(Flight flight1, Flight flight2)
        {
            return !(flight1 == flight2);
        }
        public override bool Equals(object obj)
        {
            if (this == null & obj == null)
                return true;
            if (this == null || obj == null)
                return false;
            if (obj is Flight)
            {
                var obj1 = obj as Flight;
                return (Id == obj1.Id);
            }
            else
                return false;
        }

        public override int GetHashCode()
        {
            return (int)Id;
        }
    }
}
