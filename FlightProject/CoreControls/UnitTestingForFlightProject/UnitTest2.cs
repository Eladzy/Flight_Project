using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using FlightManagerProject;
using System.Collections.Generic;
using System.Reflection;

namespace UnitTestingForFlightProject
{
    [TestClass]
    public class UnitTest2
    {
        public delegate Customer del(string s);
        AnonymousFacade anonymousFacade = (AnonymousFacade)FlightCenter.GetInstance().GetFacade(null);
        [TestMethod]
        public void TestMethod1()
        {
            CustomerMsSqlDao customerMsSql = new CustomerMsSqlDao();
            Func<string, Customer> func = customerMsSql.GetCustomerByUserName;
           Customer c= func("EmmaMilesN00M1M");
            LoginService login = new LoginService();
            var token = login.TryLogin(c.User_Name, c.Password);
            Type t = token.GetUser().GetType();
            PropertyInfo propertyInfo = t.GetProperty("Password");
            var pwd = propertyInfo.GetValue(token.GetUser());
            Assert.IsTrue(c.Password==pwd.ToString());

        }
    }
}
