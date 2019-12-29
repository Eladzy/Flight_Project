using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FlightManagerProject
{
    [Serializable]
    public class ExceptionFlightNotFound:Exception
    {
        public ExceptionFlightNotFound()
        {

        }
        public ExceptionFlightNotFound(string message):base(message)
        {

        }
        public ExceptionFlightNotFound(string message, Exception innerException) : base(message, innerException)
        {
     
        }
    }
}
