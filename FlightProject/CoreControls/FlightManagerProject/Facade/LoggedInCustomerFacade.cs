using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class LoggedInCustomerFacade : AnonymousFacade,ILoggedInCustomerFacade
    {

        TicketsMsSqlDao ticketsDao = new TicketsMsSqlDao();
        FlightsMsSqlDao flightsDao = new FlightsMsSqlDao();
        /// <summary>
        /// validate that the ticket actually belongs to the customer
        /// and if the flight still available the ticket will return to the ticket pool
        /// </summary>
        /// <param name="token"></param>
        /// <param name="ticket"></param>
        public void CancelTicket(LoginToken<Customer> token, long flightId)
        {
            Ticket ticket = _ticketDAO.GetTicketByInfo(token.User.Id, flightId);
            if(ticket.Customer_Id!=token.User.Id||ticket==null)
            {
                Exception e=new TicketNotFoundException ("Ticket is either not belong to user or not found");
                ErrorLogger.Logger(e);
                throw e;
            }
            ticketsDao.Remove(ticket);
            Flight flight = flightsDao.Get(ticket.Flight_Id);
            if (flight != null)
            {
                flight.Remaining_Tickets += 1;
                flightsDao.Update(flight);
            }
        }
        /// <summary>
        /// gets every flight associated to a certain customer
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllMyFlights(LoginToken<Customer> token)
        {

            List<Flight> flights = flightsDao.GetFlightsByCustomer(token.User).ToList();
            return flights;
        }
        /// <summary>
        /// validates that there are enough tickets in a flight and associate the customer to the flighte
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        /// <returns></returns>
        public Ticket PurchaseTicket(LoginToken<Customer> token, Flight flight)
        {
            if (flight.Id == 0||flight == null)
            {
                Exception ex= new ExceptionFlightNotFound("No Valid Flight was chosen");
                ErrorLogger.Logger(ex);
                throw ex;
            }
            if (flight.Remaining_Tickets>0)
            {
                Ticket ticket = new Ticket
                {
                    Flight_Id = flight.Id,
                    Customer_Id = token.User.Id
                };
                ticketsDao.Add(ticket);
                flight.Remaining_Tickets--;
                flightsDao.Update(flight);
                return ticket;
            }
           ExceptionTicketSoldOut e= new ExceptionTicketSoldOut();
            ErrorLogger.Logger(e);
            throw e;
            
        }

        public JObject GetUserDetails(LoginToken<Customer> token, long id)
        {
            if (token.User.Id==id)
            {
               
                Customer c = _customerDAO.Get(id);
                return c.ToJsonPresentable();
            }
            else
            {
                var e = new ExceptionUserNotFound();
                ErrorLogger.Logger(e);
                throw e;

            }

        }


        public IList<JObject> GetCustomerFlightsJson(LoginToken<Customer> token, long id)
        {
            if (token.User.Id == id)
            {
                return _flightDAO.GetJsonFlightsByCustomer(id);
            }
            else
            {
                 ExceptionUserNotFound e = new ExceptionUserNotFound();
                ErrorLogger.Logger(e);
                throw e;
            }
        }


        public JObject UpdateCustomerDetails(LoginToken<Customer> token, long id,string fname,string lname,string phone,string address)
        {
            Customer c = _customerDAO.Get(id);
            c.First_Name = string.IsNullOrWhiteSpace(fname) ? c.First_Name : fname;
            c.Last_Name = string.IsNullOrWhiteSpace(lname) ? c.Last_Name : lname;
            c.Phone_Number = !string.IsNullOrWhiteSpace(phone) && phone.All(char.IsDigit)&&phone.Length == 10 ? phone : c.Phone_Number;
            c.Address = string.IsNullOrWhiteSpace(address) ? c.Address : address;
            _customerDAO.Update(c);
            return _customerDAO.Get(id).ToJsonPresentable();
        }//revisit id parameter might be unnecessery
        public bool ChangePassword(LoginToken<Customer> token,string oldPassword,string newPassword)
        {
            if (oldPassword == token.User.Password)
            {
                Customer c = token.User;
                c.Password = newPassword;
                _customerDAO.Update(c);
                return true;
            }
            return false;
        }
    }
}
