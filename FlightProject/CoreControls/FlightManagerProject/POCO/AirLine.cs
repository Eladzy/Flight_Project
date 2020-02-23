using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
     public class AirLine : IUser, IPoco
    {
        public long Id { get; set; }//todo automatic id generator
        public string AirLine_Name { get; set; }
        public string User_Name { get; set; }
        public string Password { get; set; }
        public long CountryCode { get; set; }


        public AirLine()
        {

        }

        public AirLine( string airLine_Name, string user_Name, string password, long countryCode)
        { 
            AirLine_Name = airLine_Name;
            User_Name = user_Name;
            Password = password;
            CountryCode = countryCode;
        }

        public AirLine(long id, string airLine_Name, string user_Name, string password, long countryCode)
        {
            Id = id;
            AirLine_Name = airLine_Name;
            User_Name = user_Name;
            Password = password;
            CountryCode = countryCode;
        }



        public static bool operator ==(AirLine airLine1, AirLine airLine2)
        {
            if (Equals(airLine1, null) && Equals(airLine2, null))
                return true;
            if (Equals(airLine1, null) || Equals(airLine2, null))
                return false;
            if (Equals(airLine1.Id, airLine2.Id))
                return true;
            return false;
        }



        public static bool operator !=(AirLine airLine1, AirLine airLine2)
        {
            return !(airLine1 == airLine2);
        }



        public override bool Equals(object obj)
        {
            if (this == null & obj == null)
                return true;
            if (this == null || obj == null)
                return false;
            if (obj is AirLine)
            {
                var obj1 = obj as AirLine;
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
