using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightDataBaseFiller
{
    class TicketBuy
    {

        private LoginService GetLoginService;
        
        public int TicketsPerCustomer { get; set; }

        public List<Flight> TotalFlights { get; set; }

        public List<Customer> TotalCustomers { get; set; }
        //to figure out what to do in exception scenario

        public TicketBuy()
        {
            GetLoginService = new LoginService();
        }

        public void TicketAquisition()
        {
            while (TicketsPerCustomer > 0)
            {
                foreach (Customer c in TotalCustomers)
                {
                    LoginToken<Customer> token;
                    if(GetLoginService.TryCustomerLogin(c.User_Name,c.Password,out token))
                    {
                        var getCustomerFacade = (LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(token);
                        getCustomerFacade.PurchaseTicket
                    }
                }
            }
        }

    }
}
