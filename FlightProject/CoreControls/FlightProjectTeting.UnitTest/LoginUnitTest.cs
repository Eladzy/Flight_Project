using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagerProject;
using System.Diagnostics;

namespace FlightProjectTeting.UnitTest
{
    [TestClass]
    public class LoginTest
    {
       
        LoginService service = new LoginService();
        CustomerMsSqlDao customerDao = new CustomerMsSqlDao();
        AirLineMsSqlDao airLineDao = new AirLineMsSqlDao();
        Customer testCustomer = new Customer
        {
            First_Name = "LogTest",
            Last_Name = "1",
            User_Name="LogC",
            Password="12345678",
            Phone_Number="1234567890",
            Address="12s",
            Credit_Card_Number="1234567890123456"
        };
       
        AirLine testAirline = new AirLine
        {
            AirLine_Name = "testAirLine",
            Id = 100,
            User_Name="LogAirline",
            CountryCode=1,
            Password="12345678",
            
        };
    
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(service.TryAdminLogin("ADMIN", "9999", out LoginToken<Administrator> token));

        }
        [TestMethod]
        public void TestMethod2()
        {
            customerDao.Add(testCustomer);
            service.TryCustomerLogin(testCustomer.User_Name, testCustomer.Password, out LoginToken<Customer> token);
            Assert.AreEqual(token.User.User_Name, testCustomer.User_Name);
        }
        [TestMethod]
        public void TestMethod3()
        {
            airLineDao.Add(testAirline);
            LoginToken<AirLine> token=new LoginToken<AirLine>();
          service.TryAirLineLogin(testAirline.User_Name, testAirline.Password, out token);    
            Assert.AreEqual(token.User.User_Name, testAirline.User_Name);
        }
    }
}
