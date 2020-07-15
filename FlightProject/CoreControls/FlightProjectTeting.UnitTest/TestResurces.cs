using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;

namespace FlightProjectTesting.UnitTest
{
    public static class TestResurces
    {
       public static Customer c1 = new Customer
        {
            First_Name = "aaa",
            User_Name = "aaa1",
            Last_Name = "aaa",
            Password = "123",
            Address = "123a",
            Phone_Number = "1234567890",
            Credit_Card_Number = "1234567890"
        };
        public static Customer c2 = new Customer
        {
            First_Name = "bba",
            User_Name = "aaa2",
            Last_Name = "bba",
            Password = "123",
            Address = "123a",
            Phone_Number = "1234567890",
            Credit_Card_Number = "1234567890"
        };
        public static AirLine a1 = new AirLine
        {
            User_Name = "air1",
            AirLine_Name = "fff",
            Password = "123",
            CountryCode = 50
        };
        public static AirLine a2 = new AirLine
        {
            User_Name = "air2",
            AirLine_Name = "fff",
            Password = "123",
            CountryCode = 55
        };
        
    }
}
