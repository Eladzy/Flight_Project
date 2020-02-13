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
        public void CancelTicket(LoginToken<Customer> token, Ticket ticket)
        {
            if(ticket.Customer_Id!=token.User.Id||ticket==null)
            {
                Exception e=new ExceptionTicketNotFound("Ticket is either not belong to user or not found");
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
                return ticket;
            }
           ExceptionTicketSoldOut e= new ExceptionTicketSoldOut();
            ErrorLogger.Logger(e);
            throw e;
            
        }
    }
}
