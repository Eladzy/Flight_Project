using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    interface ILoggedInAirLineFacade
    {
        IList<Ticket> GetAllTickets(LoginToken<AirLine> token);
        IList<Flight> GetAllFlights(LoginToken<AirLine> token);
        void CancelFlight(LoginToken<AirLine> token, Flight flight);
        void CreateFlight(LoginToken<AirLine> token, Flight flight);
        void UpdateFlight(LoginToken<AirLine> token, Flight flight);
        void ChangeMyPassword(LoginToken<AirLine> token, string oldPassword, string newPassword);
        void MofidyAirlineDetails(LoginToken<AirLine> token, AirLine airline);

    }
}
