using System;
using System.Collections.Generic;

namespace FlightManagerProject
{
    public interface IAnonymousFacade
    {
        IList<Flight> GetAllFlights();
        IList<AirLine> GetAllAirlineCompanies();
        IList<Country> GetCountries();
        Dictionary<Flight, int> GetAllFlightsVacancy();
        Flight GetFlightById(long id);
        IList<Flight> GetFlightsByOriginCountry(int countryCode);
        IList<Flight> GetFlightsByDestinationCountry(int countryCode);
        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Newtonsoft.Json.Linq.JObject> SearchFlights(long? id = null, long? airlineId = null, int? originCountryId = null, int? destinationCountryId = null, DateTime? departureTime = null, DateTime? landingTime = null);
        IList<Flight> SearchFlightsByTimeSpan( long? id = null, long? airlineId = null, int? originCountryId = null, int? destinationCountryId = null, DateTime? departureTime1 = null, DateTime? departureTime2 = null, DateTime? landingTime1 = null, DateTime? landingTime2 = null);
        IList<Newtonsoft.Json.Linq.JObject> GetAvailableFlightsJson();
        IList<Newtonsoft.Json.Linq.JObject> GetAirlinesJson();
    }
}
