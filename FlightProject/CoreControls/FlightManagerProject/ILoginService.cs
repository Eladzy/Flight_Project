using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    interface ILoginService
    {
        bool TryAdminLogin(string username, string password,out LoginToken<Administrator>token);
        bool TryAirLineLogin(string username, string password,out LoginToken<AirLine>token);
        bool TryCustomerLogin(string username, string password,out LoginToken<Customer>token);
    }
}
