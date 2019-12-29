using System;
using System.Collections.Generic;

namespace FlightManagerProject
{
    public interface IAnonymousFacade
    {
        IList<Flight> GetAllFlights();
        IList<AirLine> GetAllAirlineCompanies();
        Dictionary<Flight, int> GetAllFlightsVacancy();
        Flight GetFlightById(long id);
        IList<Flight> GetFlightsByOriginCountry(int countryCode);
        IList<Flight> GetFlightsByDestinationCountry(int countryCode);
        IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
    }
}
