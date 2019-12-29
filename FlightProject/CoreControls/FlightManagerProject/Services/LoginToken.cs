using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class LoginToken<T> : ILoginTokenBase where T : IUser
    {
        public T User { get; set; }
        public IUser GetUser()
        {
            return User;
        }
    }
}
