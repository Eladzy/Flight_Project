using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using  FlightManagerProject;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace FlightProjectTesting.UnitTest
{
    [TestClass]
    public class AnnonymousFacadeTest
    {
        DataAccessTestingTools _testingTools = new DataAccessTestingTools();
        AirLineMsSqlDao _airlineDao = new AirLineMsSqlDao();
        CustomerMsSqlDao _customerDao = new CustomerMsSqlDao();


        [TestMethod]
        public void AnonymouseFacade_GetFlightById_FlightFound()
        {
            Flight f = _testingTools.GetFlight();
            Flight flight = FlightCenter.GetInstance().GetFacade(null).GetFlightById(f.Id);
        }
        [TestMethod]
       public void GetFlightList()
       {
           
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetAllFlights();
            Assert.IsTrue(flights.Count > 1);
       }
        [TestMethod]
        public void GetAvailableFlightLst()
        {

            List<JObject> flights = (List<JObject>)FlightCenter.GetInstance().GetFacade(null).GetAvailableFlightsJson();
            Assert.IsTrue(flights.Count > 1);
        }
        [TestMethod]
        public void GetAirlines()
        {

            List<JObject> airLines = (List<JObject>)FlightCenter.GetInstance().GetFacade(null).GetAirlinesJson();
            Assert.IsTrue(airLines.Count > 1);
        }
        [TestMethod]
        public void GetFlightVacancy()
        {
          
            Dictionary<Flight, int> vacancy = FlightCenter.GetInstance().GetFacade(null).GetAllFlightsVacancy();
            Assert.IsTrue(vacancy.Count > 0);
        }
        [TestMethod]
        public void GetByDeparture()
        {
            Flight flight = _testingTools.GetFlight();
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByDepatrureDate(flight.Departure_Time);
            Assert.IsTrue(flights.Contains(flight));
        }
        [TestMethod]
        public void GetByLanding()
        {
            Flight flight = _testingTools.GetFlight();
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByLandingDate(flight.Landing_Time);
            Assert.IsTrue( flights.Contains(flight));
        }
        [TestMethod]
        public void GetByOrigin()
        {
            Flight flight = _testingTools.GetFlight();
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByOriginCountry((int)flight.Origin_Country_Code);
            Assert.IsTrue(flights.Count > 0);
        }
        [TestMethod]
        public void GetByDestination()
        {
            Flight flight = _testingTools.GetFlight();
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByDestinationCountry((int)flight.Destination_Country_Code);
            Assert.IsTrue(flights.Count > 0);
        }

        [TestMethod]
        public void CustomerUsernameCheck()
        {
            Customer c = _testingTools.GetCustomer();
            Assert.IsTrue(FlightCenter.GetInstance().GetFacade(null).IsCustomerUsernameAvailable(c.User_Name) == false && FlightCenter.GetInstance().GetFacade(null).IsCustomerUsernameAvailable("4324234") == true);
        }
        [TestMethod]
        public void AirlineUsernameCheck()
        {
            AirLine a = _testingTools.GetAirLine();
            Assert.IsTrue(FlightCenter.GetInstance().GetFacade(null).IsAirlineusernameAvailable(a.User_Name) == false && FlightCenter.GetInstance().GetFacade(null).IsCustomerUsernameAvailable("4324234") == true);
        }

        [TestMethod]
        public void RegisterAirline()
        {
            AirLine a = TestResurces.a1;
            FlightCenter.GetInstance().GetFacade(null).RegisterAirline(a.User_Name, a.Password, a.AirLine_Name,"as2as@aad.com",a.CountryCode.ToString());
            AirLine a1 = _airlineDao.GetAirLineByUserName(a.User_Name);
            Assert.IsTrue(a1.Id != 0);
            _airlineDao.Remove(a1);
        }

        [TestMethod]
        public void RegisterCustomer()
        {
            Customer c = TestResurces.c2;
            FlightCenter.GetInstance().GetFacade(null).RegisterCustomer(c.User_Name, c.Password, c.First_Name, c.Last_Name, c.Address, c.Phone_Number, c.Credit_Card_Number, "dadad@elasdsd.com");
            Customer c1 = (_customerDao.GetCustomerByUserName(c.User_Name));
            Assert.IsTrue(c1.Id!=0);
            _customerDao.Remove(c1);
        }

       
    }
}
 
