using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class ExceptionUserNotFound:Exception
    {
        public ExceptionUserNotFound()
        {

        }
        public ExceptionUserNotFound(string message):base(message)
        {

        }
        public ExceptionUserNotFound(string message, Exception innerException) : base(message, innerException)
        {

        }

    }
}
