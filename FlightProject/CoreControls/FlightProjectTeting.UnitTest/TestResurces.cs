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
            First_Name = "cust1Test",
            User_Name = "aaa1",
            Last_Name = "aaa",
            Password = "123aaaaaa",
            Address = "123a",
            Phone_Number = "1234567890",
            Credit_Card_Number = "1234567890"
        };
        public static Customer c2 = new Customer
        {
            First_Name = "custTest",
            User_Name = "aaa2",
            Last_Name = "bba",
            Password = "123aaaaaaa",
            Address = "123a",
            Phone_Number = "1234567890",
            Credit_Card_Number = "1234567890123456"
        };
        public static Customer c3 = new Customer
        {
            First_Name = "cus3tTest",
            User_Name = "aaa3",
            Last_Name = "bba",
            Password = "123",
            Address = "123a",
            Phone_Number = "1234567890",
            Credit_Card_Number = "1234567890"
        };
        public static AirLine a1 = new AirLine
        {
            User_Name = "airtest",
            AirLine_Name = "fff",
            Password = "12aaaaaaa3",
            CountryCode = 50
        };
        public static AirLine a2 = new AirLine
        {
            User_Name = "air2test",
            AirLine_Name = "fff",
            Password = "123",
            CountryCode = 55
        };
        
    }
}
