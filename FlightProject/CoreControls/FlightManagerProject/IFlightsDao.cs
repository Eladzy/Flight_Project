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
        IList<Newtonsoft.Json.Linq.JObject> SearchFlight(long? id = null, long? airlineId = null, int? originCountryId = null, int? destinationCountryId = null, DateTime? departureTime = null, DateTime? landingTime = null);
        IList<Flight> FlightsByTimeSpan(long? id = null, long? airlineId = null, int? originCountryId = null, int? destinationCountryId = null, DateTime? departureTime1 = null, DateTime? departureTime2 = null, DateTime? landingTime1 = null, DateTime? landingTime2 = null);
        IList<Newtonsoft.Json.Linq.JObject> GetAvailableFlightsJson();
    }
}
