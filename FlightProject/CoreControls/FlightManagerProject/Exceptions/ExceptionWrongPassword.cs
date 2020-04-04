using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class ExceptionWrongPassword:Exception
    {
        public ExceptionWrongPassword()
        {

        }
        public ExceptionWrongPassword(string message):base(message)
        {

        }
        public ExceptionWrongPassword(string message, Exception innerException) : base(message, innerException)  
        {

        }
    }
}
