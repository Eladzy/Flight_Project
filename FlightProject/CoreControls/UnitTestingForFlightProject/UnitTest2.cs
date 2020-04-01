using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using FlightManagerProject;
using System.Collections.Generic;

namespace UnitTestingForFlightProject
{
    [TestClass]
    public class UnitTest2
    {
        AnonymousFacade anonymousFacade = (AnonymousFacade)FlightCenter.GetInstance().GetFacade(null);
        [TestMethod]
        public void TestMethod1()
        {
            List<JObject> flights = (List<JObject>)anonymousFacade.SearchFlights(82009212, null, null, null, null, null);
            Assert.IsTrue(flights.Count > 0);
        }
    }
}
