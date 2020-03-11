using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public class Administrator : IUser,IPoco
    {
        // remove
        string userName = ConfigurationUtils.adminUserName;
        string password = ConfigurationUtils.adminPassword;

        // replace with
        public string User_Name { get; set; }
        public string Password { get; set; }
    }
}
