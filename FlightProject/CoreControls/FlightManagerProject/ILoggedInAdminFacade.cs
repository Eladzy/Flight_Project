using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    interface ILoggedInAdminFacade
    {
        void CreateNewAirline(LoginToken<Administrator> token, AirLine airline);
        void UpdateAirlineDetails(LoginToken<Administrator> token, AirLine airLine);
        void RemoveAirline(LoginToken<Administrator> token, AirLine airline);
        void CreateNewCustomer(LoginToken<Administrator> token, Customer customer);
        void UpdateCustomerDetails(LoginToken<Administrator> token, Customer customer);
        void RemoveCustomer(LoginToken<Administrator> token, Customer customer);
    }
}
