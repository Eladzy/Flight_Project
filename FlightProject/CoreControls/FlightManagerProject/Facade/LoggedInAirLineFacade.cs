using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class LoggedInAirLineFacade :AnonymousFacade, ILoggedInAirLineFacade
    {
      
        public LoggedInAirLineFacade()
        {
            _airlineDAO = new AirLineMsSqlDao();
            _flightDAO= new FlightsMsSqlDao();
            _ticketDAO= new TicketsMsSqlDao();
        }

      /// <summary>
      /// cancel the designated flight after clearing all the tickets
      /// </summary>
      /// <param name="token"></param>
      /// <param name="flight"></param>
        public void CancelFlight(LoginToken<AirLine> token, Flight flight)
        {

            if (token.User.Id == flight.AirLine_Id)
            {
                foreach (Ticket ticket in _ticketDAO.GetAll().Where(t=>t.Flight_Id==flight.Id))
                {
                    if (flight.Id == ticket.Flight_Id)
                    {
                        _ticketDAO.Remove(ticket);
                    }
                }
                _flightDAO.Remove(flight);
            }
        }
        /// <summary>
        /// replaces the old password via update
        /// </summary>
        /// <param name="token"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        public void ChangeMyPassword(LoginToken<AirLine> token, string oldPassword, string newPassword)
        {
            if (oldPassword == token.User.Password)
            {
                AirLine airLine = new AirLine
                {
                    Id = token.User.Id,
                    AirLine_Name = token.User.AirLine_Name,
                    User_Name = token.User.User_Name,
                    CountryCode = token.User.CountryCode,
                    Password = newPassword
                    

                };
                _airlineDAO.Update(airLine);
            }
            else
            {
                throw new ExceptionWrongPassword();
            }
        }
        /// <summary>
        /// creates new flight for this airline
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void CreateFlight(LoginToken<AirLine> token, Flight flight)
        {
            if(flight.AirLine_Id==token.User.Id)
            _flightDAO.Add(flight);
        }
        /// <summary>
        /// returns a list of the same airline
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Flight> GetAllFlights(LoginToken<AirLine> token)
        {

            List<Flight> flights = _flightDAO.GetFlightByAirline(token.User).ToList();
            return flights;
        }
        /// <summary>
        /// returns a list of all tickets belong to the same airline
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public IList<Ticket> GetAllTickets(LoginToken<AirLine> token)
        {
            List<Flight> flights = GetAllFlights(token).ToList();
            List<Ticket> tickets = new List<Ticket>();
            
            foreach (Flight flight in flights)
            {
                tickets.AddRange(_ticketDAO.GetAll().Where(t => t.Flight_Id == flight.Id));
            }
            return tickets;
        }
        /// <summary>
        /// updates airline details
        /// </summary>
        /// <param name="token"></param>
        /// <param name="airline"></param>
        public void MofidyAirlineDetails(LoginToken<AirLine> token, AirLine airline)
        {
            if (token.User.Id == airline.Id)
            {
                _airlineDAO.Update(airline);
            }
            throw new ExceptionUserNotFound();
            
        }
        /// <summary>
        /// update flight details of the same airline
        /// </summary>
        /// <param name="token"></param>
        /// <param name="flight"></param>
        public void UpdateFlight(LoginToken<AirLine> token, Flight flight)
        {
            if (flight.AirLine_Id == token.User.Id)
            {
               _flightDAO.Update(flight);
            }
            else
                throw new ExceptionFlightNotFound();
        }
        public JObject GetUserDetails(LoginToken<AirLine> token, long id)
        {
            if (token.User.Id == id)
            {

                AirLine a = _airlineDAO.Get(id);
                return a.ToJsonPresentable();
            }
            else
            {
                var e = new ExceptionUserNotFound();
                ErrorLogger.Logger(e);
                throw e;

            }

        }
    }
}
