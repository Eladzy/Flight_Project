using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
  public abstract class FacadeBase
    {
       /*protected*/public IAirLineDao _airlineDAO;
       /*protected*/ public ICountryDao _countryDAO;
       /*protected*/public  ICustomerDao _customerDAO;
        /*protected*/public IFlightsDao _flightDAO;
        /*protected*/public  ITicketDao _ticketDAO;
    }
}
