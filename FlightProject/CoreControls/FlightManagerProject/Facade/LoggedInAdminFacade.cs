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
        }
        public void CreateNewAirline(LoginToken<Administrator> token, AirLine airline)
        {
            if (token.User is Administrator)
            {
                _airlineDAO.Add(airline);
            }
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
    }
}
