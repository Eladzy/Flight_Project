using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public interface ITicketDao:IBasic<Ticket>
    {
        Ticket GetTicketByInfo(long customerId, long flightId);
    }
}
