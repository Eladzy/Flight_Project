using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    /// <summary>
    /// facade class wrapping DAO methods by user privileges
    /// annonymous facade holds universal method allowed for every type of user including non registered users
    /// </summary>
    public class AnonymousFacade : FacadeBase, IAnonymousFacade
    {

        public AnonymousFacade()
        {
            _flightDAO = new FlightsMsSqlDao();
            _airlineDAO = new AirLineMsSqlDao();
            _countryDAO = new CountryMsSqlDao();
            _customerDAO = new CustomerMsSqlDao();
        }


        /// <summary>
        /// view list of countries
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetCountries()
        {
            return _countryDAO.GetAll();
        }

        /// <summary>
        /// view all flights
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();

        }

        /// <summary>
        /// view all available flights
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightVancy = _flightDAO.GetAllFlightsVacancy();
            return flightVancy;
        }

        /// <summary>
        /// get a flight by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flight GetFlightById(long id)
        {
            if (id != 0)
            {
                Flight flight = _flightDAO.Get(id);

                return flight;
            }
            return null;
        }

        /// <summary>
        /// get a list of flights by a departure date
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepatrureDate(departureDate);
        }

        /// <summary>
        /// get a list of flights by destination
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            List<Flight> flights = _flightDAO.GetFlightsByDestinationCountry(countryCode).ToList();
            return flights;
        }

        /// <summary>
        /// get a list of flights by arrival time
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            List<Flight> flights = _flightDAO.GetFlightsByLandingDate(landingDate).ToList();
            return flights;
        }

        /// <summary>
        /// get a list of flights by origin country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            List<Flight> flights = _flightDAO.GetFlightsByOriginCountry(countryCode).ToList();
            return flights;
        }

        /// <summary>
        /// allows search flights by parameters
        /// </summary>
        /// <param name="id"></param>
        /// <param name="airlineId"></param>
        /// <param name="originCountryId"></param>
        /// <param name="destinationCountryId"></param>
        /// <param name="departureTime"></param>
        /// <param name="landingTime"></param>
        /// <returns></returns>
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

        /// <summary>
        /// retrieves a user friendly list of flights
        /// </summary>
        /// <returns></returns>
        public IList<Newtonsoft.Json.Linq.JObject> GetAvailableFlightsJson()
        {
            return _flightDAO.GetAvailableFlightsJson();
        }

        /// <summary>
        /// retrieves  a user friendly list of airlines
        /// </summary>
        /// <returns></returns>
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
            catch(Exception e)
            {
                ErrorLogger.Logger(e);
                
            }
            return false;
        }
      
        /// <summary>
        /// verfies input and registering user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="address"></param>
        /// <param name="phoneNum"></param>
        /// <param name="creditCard"></param>
        /// <param name="mail"></param>
        /// <returns></returns>
        public bool RegisterCustomer(string username,string password,string fname, string lname,//need improving
            string address,string phoneNum,string creditCard,string mail)
        {
            string[] args = new string[] { username, password, fname, lname,  address, phoneNum, creditCard, mail };
            
            foreach (string arg in args)
            {
                if (string.IsNullOrWhiteSpace(arg))
                {
                    return false;
                }
               
            }
            var email = new EmailAddressAttribute();
            if (!email.IsValid(mail))
            {
                return false;
            }
            if (!IsUsernameAvailable(username))
            {
                return false;
            }
            if (fname.All(Char.IsDigit)|| lname.All(Char.IsDigit))
            {
                return false;
            }
            if(creditCard.All(char.IsLetter)||phoneNum.All(char.IsLetter)||phoneNum.Length!=10||creditCard.Length!=16)
            {
                return false;
            }

            Customer c = new Customer
            {
                First_Name=fname,
                Last_Name=lname,
                User_Name=username,
                Password=password,
                Address=address,
                Phone_Number=phoneNum,
                Credit_Card_Number=creditCard,
            };
            _customerDAO.Add(c);

            //sendgrid send mail to email

            return true;
        }
  
        /// <summary>
        /// checks if airline username is available
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public bool IsAirlineusernameAvailable(string username)
        {
            try
            {
                AirLine a = _airlineDAO.GetAirLineByUserName(username);
            }
            catch (ExceptionUserNotFound)
            {

                return true;
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
            }
            return false;
        }
        /// <summary>
        /// verifies input and registering airline company
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <param name="email"></param>
        /// <param name="countrycode"></param>
        /// <returns></returns>
        public bool RegisterAirline(string username, string password,string name, string email, string countrycode)
        {
            //user name exists-false
            Regex reg;
            string pwdPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$";
            string usernamePattern = @"^([a-zA-z])([a-zA-z0-9_]){2,9}$";
            string namePattern = @"^([a-zA-Z]){2,16}$";
            string countryPattern = @"^\d{1,3}$";
            var mail = new EmailAddressAttribute();
            if (!IsUsernameAvailable(username))
            {
                return false;
            }
            if (!mail.IsValid(email))
            {
                return false;
            }
            reg = new Regex(pwdPattern);
            if (!reg.IsMatch(password))
            {
                return false;
            }
            reg = new Regex(usernamePattern);
            if (!reg.IsMatch(username))
            {
                return false;
            }
            reg = new Regex(namePattern);
            if (!reg.IsMatch(name))
            {
                return false;
            }
            reg = new Regex(countryPattern);
            if (!reg.IsMatch(countrycode))
            {
                return false;
            }
            AirLine a = new AirLine
            {
                AirLine_Name = name,
                User_Name = username,
                Password = password,
                CountryCode = int.Parse(countrycode)
            };
            try
            {
                _airlineDAO.Add(a);

            }
            catch (Exception e)//improve
            {

                ErrorLogger.Logger(e);
                return false;
            }
            return true;
        }
       
        /// <summary>
        /// combining the user name checks into both tables
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        /// 
        public bool IsUsernameAvailable(string username)
        {
            return (IsAirlineusernameAvailable(username) && IsCustomerUsernameAvailable(username));
        }
    }
}
