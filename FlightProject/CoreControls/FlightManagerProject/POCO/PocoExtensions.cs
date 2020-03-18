using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlightManagerProject
{
    public static class PocoExtensions
    {
        private static FlightsMsSqlDao _flightDao = new FlightsMsSqlDao();
        private static AirLineMsSqlDao _airlineDao = new AirLineMsSqlDao();
        private static CountryMsSqlDao _countryDao = new CountryMsSqlDao();

        public static string ToJsonPresentable(this Flight f)
        {
            JArray jArray = new JArray();
            JValue id = new JValue(f.Id);
            JValue airlineName = new JValue(_airlineDao.Get(f.AirLine_Id).AirLine_Name);
            JValue origin = new JValue(_countryDao.Get(f.Origin_Country_Code).Country_Name);
            JValue destination = new JValue(_countryDao.Get(f.Destination_Country_Code).Country_Name);
            JValue vacancy = new JValue(f.Remaining_Tickets);
            JValue departureTime = new JValue(f.Departure_Time);
            JValue arrivalTime = new JValue(f.Landing_Time);
            jArray.Add(id);
            jArray.Add(airlineName);
            jArray.Add(origin);
            jArray.Add(destination);
            jArray.Add(departureTime);
            jArray.Add(arrivalTime);
            jArray.Add(vacancy);
            string jObject = jArray.ToString();
            return jObject;
        }


        public static string ToJsonPresentable(this AirLine a)
        {
            JArray jArray = new JArray();
            //JValue id = new JValue(a.Id);
            JValue name = new JValue(a.AirLine_Name);
            JValue countryName = new JValue(_countryDao.Get(a.CountryCode).Country_Name);
            string jObject = jArray.ToString();
            return jObject;
        }
    }
}
