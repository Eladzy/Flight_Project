using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using  FlightManagerProject;
using System.Collections.Generic;
using System.Diagnostics;

namespace FlightProjectTeting.UnitTest
{
    [TestClass]
    public class AnnonymousFacadeTest
    {
        
        
        [TestMethod]
        public void AnonymouseFacade_GetFlightById_FlightFound()
        {
            Flight flight = FlightCenter.GetInstance().GetFacade(null).GetFlightById(1);
        }
        [TestMethod]
       public void GetFlightLIst()
       {
           
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetAllFlights();
            Assert.IsTrue(flights.Count > 1);
       }
        [TestMethod]
        public void GetAirlines()
        {
            
            List<AirLine> airLines = (List<AirLine>)FlightCenter.GetInstance().GetFacade(null).GetAllAirlineCompanies();
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
            Flight flight = FlightCenter.GetInstance().GetFacade(null).GetFlightById(1);
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByDepatrureDate(flight.Departure_Time);
            Assert.IsTrue(flights.Contains(flight));
        }
        [TestMethod]
        public void GetByLanding()
        {
            Flight flight = FlightCenter.GetInstance().GetFacade(null).GetFlightById(2);
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByLandingDate(flight.Landing_Time);
            Assert.IsTrue( flights.Contains(flight));
        }
        [TestMethod]
        public void GetByOrigin()
        {
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByOriginCountry(1);
            Assert.IsTrue(flights.Count > 0);
        }
        [TestMethod]
        public void GetByDestination()
        {
            List<Flight> flights = (List<Flight>)FlightCenter.GetInstance().GetFacade(null).GetFlightsByDestinationCountry(2);
            Assert.IsTrue(flights.Count > 0);
        }
    }
}
 
