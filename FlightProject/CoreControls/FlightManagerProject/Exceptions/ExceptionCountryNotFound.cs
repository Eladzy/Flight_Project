using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    [Serializable]
    class ExceptionCountryNotFound:Exception
    {
        public ExceptionCountryNotFound()
        {

        }
        public ExceptionCountryNotFound(string message) : base(message)
        {

        }
        public ExceptionCountryNotFound(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
