using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public interface IFlightsDao : IBasic<Flight>
    {
        Dictionary<Flight, int> GetAllFlightsVacancy();
        IList<Flight> GetFlightsByOriginCountry(int countryCode);
        IList<Flight> GetFlightsByDestinationCountry(int countryCode);
        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Flight> GetFlightsByCustomer(Customer customer);
        IList<Newtonsoft.Json.Linq.JObject> SearchFlight(long? id , long? airlineId, int? originCountryId, int? destinationCountryId, DateTime? departureTime, DateTime? landingTime );
        IList<Flight> FlightsByTimeSpan(long? id, long? airlineId , int? originCountryId, int? destinationCountryId, DateTime? departureTime1, DateTime? departureTime2, DateTime? landingTime1, DateTime? landingTime2);
        IList<Newtonsoft.Json.Linq.JObject> GetAvailableFlightsJson();
        IList<Newtonsoft.Json.Linq.JObject> GetJsonFlightsByCustomer(long id);
    }
}
