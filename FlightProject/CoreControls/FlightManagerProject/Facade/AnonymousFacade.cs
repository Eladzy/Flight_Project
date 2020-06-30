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
    public class AnonymousFacade : FacadeBase, IAnonymousFacade
    {

        public AnonymousFacade()
        {
            _flightDAO = new FlightsMsSqlDao();
            _airlineDAO = new AirLineMsSqlDao();
            _countryDAO = new CountryMsSqlDao();
            _customerDAO = new CustomerMsSqlDao();
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
            catch(Exception e)
            {
                ErrorLogger.Logger(e);
            }
            return false;
        }

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
            //if (!IsCustomerUsernameAvailable(username))
            //{
            //    return false;
            //}
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

        public bool RegisterAirline(string username, string password,string name, string email, string countrycode)
        {
            //user name exists-false
            Regex reg;
            string pwdPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,16}$";
            string usernamePattern = @"^([a-zA-z])([a-zA-z0-9_]){2,9}$";
            string namePattern = @"^([a-zA-Z]){2,16}$";
            string countryPattern = @"^\d{1,3}$";
            var mail = new EmailAddressAttribute();
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
    }
}
