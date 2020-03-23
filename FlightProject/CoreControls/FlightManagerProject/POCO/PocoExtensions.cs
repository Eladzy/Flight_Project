using System;
using Newtonsoft.Json.Linq;

namespace FlightManagerProject
{
    public static class PocoExtensions
    {
        private static FlightsMsSqlDao _flightDao = new FlightsMsSqlDao();
        private static AirLineMsSqlDao _airlineDao = new AirLineMsSqlDao();
        private static CountryMsSqlDao _countryDao = new CountryMsSqlDao();

        public static JObject ToJsonPresentable(this Flight f)
        {
            
            JObject flight =
                new JObject(
                    new JProperty("id",f.Id.ToString()),
                    new JProperty("airlineName", _airlineDao.Get(f.AirLine_Id).AirLine_Name),
                    new JProperty("origin", _countryDao.Get(f.Origin_Country_Code).Country_Name),
                    new JProperty("destination", _countryDao.Get(f.Destination_Country_Code).Country_Name),
                    new JProperty("departureTime", f.Departure_Time),
                    new JProperty("arrivalTime", f.Landing_Time),
                    new JProperty("vacancy", f.Remaining_Tickets)
                );
            return flight;
        }


        public static JObject ToJsonPresentable(this AirLine a)
        {
            JObject airline = new JObject(
                new JProperty("name",a.AirLine_Name),
                new JProperty("countryName", _countryDao.Get(a.CountryCode).Country_Name)
                );
            return airline;
        }
    }
}