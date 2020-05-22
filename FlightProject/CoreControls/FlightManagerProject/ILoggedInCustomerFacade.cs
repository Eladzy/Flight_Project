using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
   public interface ILoggedInCustomerFacade
    {
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
        Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight);
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        Newtonsoft.Json.Linq.JObject UpdateCustomerDetails(LoginToken<Customer> token, long id, string fname, string lname, string phone, string address);
        bool ChangePassword(LoginToken<Customer> token, string oldPassword, string newPassword);
    }
}
