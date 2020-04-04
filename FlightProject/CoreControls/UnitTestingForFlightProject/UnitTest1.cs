using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagerProject;

namespace UnitTestingForFlightProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LoginService login = new LoginService();
            var token = login.TryLogin("DanielStoneN00M1M", "N00M1MG2");
            Assert.IsTrue(token.GetUser() is Customer);
        }
    }
}
