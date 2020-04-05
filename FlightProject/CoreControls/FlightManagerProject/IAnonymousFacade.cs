using System;
using System.Collections.Generic;

namespace FlightManagerProject
{
    public interface IAnonymousFacade
    {
        IList<Flight> GetAllFlights();
        //IList<AirLine> GetAllAirlineCompanies();
        IList<Country> GetCountries();
        Dictionary<Flight, int> GetAllFlightsVacancy();
        Flight GetFlightById(long id);
        IList<Flight> GetFlightsByOriginCountry(int countryCode);
        IList<Flight> GetFlightsByDestinationCountry(int countryCode);
        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Newtonsoft.Json.Linq.JObject> SearchFlights(long? id, long? airlineId, int? originCountryId, int? destinationCountryId, DateTime? departureTime, DateTime? landingTime);
        IList<Flight> SearchFlightsByTimeSpan( long? id, long? airlineId, int? originCountryId, int? destinationCountryId , DateTime? departureTime1, DateTime? departureTime2, DateTime? landingTime1, DateTime? landingTime2);
        IList<Newtonsoft.Json.Linq.JObject> GetAvailableFlightsJson();
        IList<Newtonsoft.Json.Linq.JObject> GetAirlinesJson();
    }
}
