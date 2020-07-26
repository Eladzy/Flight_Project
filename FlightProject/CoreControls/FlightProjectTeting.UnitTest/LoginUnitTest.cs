using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagerProject;
using System.Diagnostics;

namespace FlightProjectTesting.UnitTest
{
    [TestClass]
    public class LoginTest
    {
       
        LoginService service = new LoginService();
        DataAccessTestingTools testingTools = new DataAccessTestingTools();
       
    
        [TestMethod]
        public void TestMethod1()
        {
            Assert.IsTrue(service.TryAdminLogin("ADMIN", "9999", out LoginToken<Administrator> token));

        }
        [TestMethod]
        public void TestMethod2()
        {
            Customer testCustomer = testingTools.GetCustomer();
            service.TryCustomerLogin(testCustomer.User_Name, testCustomer.Password, out LoginToken<Customer> token);
            Assert.AreEqual(token.User.User_Name, testCustomer.User_Name);
        }
        [TestMethod]
        public void TestMethod3()
        {
            AirLine testAirline = testingTools.GetAirLine();
            LoginToken<AirLine> token=new LoginToken<AirLine>();
          service.TryAirLineLogin(testAirline.User_Name, testAirline.Password, out token);    
            Assert.AreEqual(token.User.User_Name, testAirline.User_Name);
        }
    }
}
