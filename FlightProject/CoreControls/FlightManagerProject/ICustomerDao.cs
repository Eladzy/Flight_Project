﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagerProject
{
    public interface ICustomerDao : IBasic<Customer>
    {
        Customer GetCustomerByUserName(string userName);
    }
}
