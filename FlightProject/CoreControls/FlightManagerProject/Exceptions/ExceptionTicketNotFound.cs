using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    [Serializable]
    public class ExceptionTicketNotFound:Exception
    {
        public ExceptionTicketNotFound()
        {

        }
        public ExceptionTicketNotFound(string message):base(message)
        {

        }
        public ExceptionTicketNotFound(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
