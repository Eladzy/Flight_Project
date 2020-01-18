using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    static class TicketFactory
    {
        public static Ticket GenerateTicket(Customer customer, Flight flight)
        {
            LoginToken<Customer> token = new LoginToken<Customer>
            {
                User = customer
            };
            var customerFacade=(LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(token);
            Ticket t = customerFacade.PurchaseTicket(token, flight);
            return t;
        }
    }
}
