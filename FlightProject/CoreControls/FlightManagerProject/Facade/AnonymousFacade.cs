using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
     public class AnonymousFacade :FacadeBase, IAnonymousFacade
     {
        
        public AnonymousFacade()
        {
            _flightDAO = new FlightsMsSqlDao();
            _airlineDAO = new AirLineMsSqlDao();
        }



        public IList<AirLine> GetAllAirlineCompanies()
        {
            List<AirLine> airLines = _airlineDAO.GetAll().ToList();
            return airLines;
        }



        public IList<Flight> GetAllFlights()
        {
            List<Flight> flights = _flightDAO.GetAll().ToList();
            return flights;
        }



        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int>  flightVancy = _flightDAO.GetAllFlightsVacancy();
            return flightVancy;
        }



        public Flight GetFlightById(long id)
        {
            if (id != 0 )
            {
                Flight flight = _flightDAO.Get(id);
                return flight;
            }
            return null;
        }



        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            List<Flight> flights = _flightDAO.GetFlightsByDepatrureDate(departureDate).ToList();
            return flights;
        }



        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flight> flights = _flightDAO.GetFlightsByDestinationCountry(countryCode).ToList();
            return flights;
        }



        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flight> flights = _flightDAO.GetFlightsByLandingDate(landingDate).ToList();
            return flights;
        }



        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            List<Flight> flights = _flightDAO.GetFlightsByOriginCountry(countryCode).ToList();
            return flights;
        }

        public IList<Flight> SearchFlights(long? id = null, long? airlineId = null, int? originCountryId = null, int? destinationCountryId = null, DateTime? departureTime = null, DateTime? landingTime = null)
        {
            List<Flight> flights =
                _flightDAO.SearchFlight(id, airlineId, originCountryId, destinationCountryId, departureTime, landingTime).ToList();
            return flights;
        }

        public IList<Flight> SearchFlightsByTimeSpan( long? id = null, long? airlineId = null, int? originCountryId = null, int? destinationCountryId = null, DateTime? departureTime1 = null, DateTime? departureTime2 = null, DateTime? landingTime1 = null, DateTime? landingTime2 = null)
        {
            List<Flight> flights =
                _flightDAO.FlightsByTimeSpan( id, airlineId, originCountryId, destinationCountryId, departureTime1,departureTime2, landingTime1,landingTime2).ToList();
            return flights;
        }
    }
}
