using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class Country : IPoco
    {
        public long Id { get; set; }
        public string Country_Name { get; set; }


        public Country()
        {

        }



        public Country(long id, string country_Name)
        {
            Id = id;
            Country_Name = country_Name;
        }



        public static bool operator ==(Country country1, Country country2)
        {
            if (Equals(country1, null) && Equals(country2, null))
                return true;
            if (Equals(country1, null) || Equals(country2, null))
                return false;
            if (Equals(country1.Id, country2.Id))
                return true;
            return false;
        }



        public static bool operator !=(Country country1, Country country2)
        {
            return !(country1 == country2);
        }



        public override bool Equals(object obj)
        {
            if (this == null & obj == null)
                return true;
            if (this == null || obj == null)
                return false;
            if (obj is Country)
            {
                var obj1 = obj as Country;
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
