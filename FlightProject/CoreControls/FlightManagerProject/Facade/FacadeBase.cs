using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
  public abstract class FacadeBase
    {
       public IAirLineDao _airlineDAO;
       public ICountryDao _countryDAO;
       public  ICustomerDao _customerDAO;
       public IFlightsDao _flightDAO;
       public  ITicketDao _ticketDAO;
    }
}
