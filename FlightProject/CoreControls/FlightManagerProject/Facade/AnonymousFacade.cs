using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class AnonymousFacade : FacadeBase, IAnonymousFacade
    {

        public AnonymousFacade()
        {
            _flightDAO = new FlightsMsSqlDao();
            _airlineDAO = new AirLineMsSqlDao();
            _countryDAO = new CountryMsSqlDao();
        }



        //public IList<AirLine> GetAllAirlineCompanies()
        //{
        //    List<AirLine> airLines = _airlineDAO.GetAll().ToList();
        //    return airLines;
        //}

        public IList<Country> GetCountries()
        {
            return _countryDAO.GetAll();
        }


        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();

        }


        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightVancy = _flightDAO.GetAllFlightsVacancy();
            return flightVancy;
        }


        public Flight GetFlightById(long id)
        {
            if (id != 0)
            {
                Flight flight = _flightDAO.Get(id);

                return flight;
            }
            return null;
        }


        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepatrureDate(departureDate);
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


        public IList<JObject> SearchFlights(long? id, long? airlineId, int? originCountryId, int? destinationCountryId, DateTime? departureTime, DateTime? landingTime)
        {
            List<JObject> flights =
                _flightDAO.SearchFlight(id, airlineId, originCountryId, destinationCountryId, departureTime, landingTime).ToList();
            return flights;
        }


        public IList<Flight> SearchFlightsByTimeSpan(long? id, long? airlineId, int? originCountryId, int? destinationCountryId, DateTime? departureTime1,
            DateTime? departureTime2, DateTime? landingTime1, DateTime? landingTime2)//todo fix or remove
        {
            List<Flight> flights =
                _flightDAO.FlightsByTimeSpan(id, airlineId, originCountryId, destinationCountryId, departureTime1, departureTime2, landingTime1, landingTime2).ToList();
            return flights;
        }


        public IList<Newtonsoft.Json.Linq.JObject> GetAvailableFlightsJson()
        {
            return _flightDAO.GetAvailableFlightsJson();
        }


        public IList<Newtonsoft.Json.Linq.JObject> GetAirlinesJson()
        {
            return _airlineDAO.GetAirlinesJson();
        }

        /// <summary>
        /// check for customer username availablity return true for avialable username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsCustomerUsernameAvailable(string username)
        {
            try
            {
                Customer c = _customerDAO.GetCustomerByUserName(username);
            }
            catch (ExceptionUserNotFound)
            {

                return true;
            }
            return false;
        }

        public bool RegisterCustomer(string fname,string lname,string username, string password,
            string address,string phoneNum,string creditCard,string mail)
        {
            string[] args = new string[] { fname, lname, username, password, address, phoneNum, creditCard, mail };
            
            foreach (string arg in args)
            {
                if (string.IsNullOrWhiteSpace(arg))
                {
                    return false;
                }
               
            }
           
            if (fname.All(Char.IsDigit))
            {
                return false;
            }
            if(lname.All(Char.IsDigit))
            {
                return false;
            }

            //if ((!long.TryParse(args["phoneNum"], out _))&&args["phoneNum"].Length!=10)
            //{
            //    return false;
            //}
            //if ((!long.TryParse(args["creditCard"], out _))&& args["creditCard"].Length!=16)
            //{
            //    return false;
            //}
            return false;
        }
    }
}
