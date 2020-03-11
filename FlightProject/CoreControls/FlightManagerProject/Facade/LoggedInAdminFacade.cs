using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class LoggedInAdminFacade : AnonymousFacade, ILoggedInAdminFacade
    {
        public LoggedInAdminFacade()
        {//check
            _airlineDAO = new AirLineMsSqlDao();
            _customerDAO = new CustomerMsSqlDao();
            _countryDAO = new CountryMsSqlDao();
        }
        public void CreateNewAirline(LoginToken<Administrator> token, AirLine airline)
        {
            // get airline by name
            // if found
            //    throw new CompanyAlreadyExistException(...)

            // else
            
            // this is redundant check, since the type T will always be Administrator
            if (token.User is Administrator)
            {
                _airlineDAO.Add(airline);
            }
           // else
               // throw new WrongTokenException("user is not admin")
        }

        public void CreateNewCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token.User is Administrator)
            {
                _customerDAO.Add(customer);
            }
        }

        public void RemoveAirline(LoginToken<Administrator> token, AirLine airline)
        {
            if (token.User is Administrator)
            {
                _airlineDAO.Remove(airline);
            }
        }

        public void RemoveCustomer(LoginToken<Administrator> token, Customer customer)
        {
            if (token.User is Administrator)
            {
                _customerDAO.Remove(customer);
            }
        }

        public void UpdateAirlineDetails(LoginToken<Administrator> token, AirLine airLine)
        {
            if (token.User is Administrator)
            {
                _airlineDAO.Update(airLine);
            }
        }

        public void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer)
        {
            if (token.User is Administrator)
            {
                _customerDAO.Update(customer);
            }
        }
        public void AddCountry(LoginToken<Administrator> token, Country country)
        {
            if (token.User is Administrator)
            {
                _countryDAO.Add(country);
            }
        }
        public AirLine GetAirlineByUser(LoginToken<Administrator> token, string username)
        {
          
            try
            {
                return _airlineDAO.GetAirLineByUserName(username);
            }
            catch (ExceptionFlightNotFound e)
            {
                ErrorLogger.Logger(e);
                throw e;
            }
          
        }
        public Customer GetCustomerByUser(LoginToken<Administrator> token, string username)
        {
           return _customerDAO.GetCustomerByUserName(username);
        }
    }
}
