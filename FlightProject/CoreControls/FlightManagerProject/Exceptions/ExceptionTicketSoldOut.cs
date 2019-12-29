using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    [Serializable]
    public class ExceptionTicketSoldOut:Exception
    {
        public ExceptionTicketSoldOut()
        {

        }
        public ExceptionTicketSoldOut(string message) : base(message)
        {

        }
        public ExceptionTicketSoldOut(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
