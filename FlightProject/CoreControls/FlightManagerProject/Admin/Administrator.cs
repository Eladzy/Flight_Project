using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class Administrator:IUser,IPoco
    {
        string userName = ConfigurationUtils.adminUserName;
        string password = ConfigurationUtils.adminPassword;
    }
}
