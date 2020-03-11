using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    [Serializable]
    public class TicketNotFoundException :Exception
    {
        public TicketNotFoundException ()
        {

        }
        public TicketNotFoundException (string message):base(message)
        {

        }
        public TicketNotFoundException (string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
