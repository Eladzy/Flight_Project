using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagerProject;
using System.Collections.Generic;

namespace FlightProjectTeting.UnitTest
{
    [TestClass]
    public class AirlineFacadeUnitTest
    {
        FlightsMsSqlDao flightsDao = new FlightsMsSqlDao();

        AirLineMsSqlDao airlineDao = new AirLineMsSqlDao();

        LoginToken<AirLine> Token { get; set; }

        static LoggedInAirLineFacade AirlineFacade { get; set; }


        Flight f1 = new Flight
        {
            Id = 21,
            AirLine_Id = 20,
            Departure_Time = new DateTime(2019, 11, 20, 16, 30, 0),
            Landing_Time = new DateTime(2019, 11, 20, 20, 0, 0),
            Origin_Country_Code = 6,
            Destination_Country_Code = 5,
            Remaining_Tickets = 20
        };



        Flight f2 = new Flight
        {
            Id = 22,
            AirLine_Id = 20,
            Departure_Time = new DateTime(2019, 11, 22, 17, 30, 0),
            Landing_Time = new DateTime(2019, 11, 22, 21, 0, 0),
            Origin_Country_Code = 5,
            Destination_Country_Code = 6,
            Remaining_Tickets = 20
        };



        AirLine airline = new AirLine
        {
            AirLine_Name = "UnitTestAirLine",
            User_Name = "AirlineTest",
            CountryCode = 1,
            Password = "A12345678",
            Id = 20,
        };


       
        public AirlineFacadeUnitTest()
        {
            Token = new LoginToken<AirLine>
            {
                User = airline
            };
            AirlineFacade = (LoggedInAirLineFacade)FlightCenter.GetInstance().GetFacade(Token);
        }
        
      [TestMethod]
      public void CreateFlights()
      {
            airlineDao.Add(Token.User);
            AirlineFacade.CreateFlight(Token,f1);
            AirlineFacade.CreateFlight(Token,f2);
            Assert.IsTrue(AirlineFacade.GetAllFlights(Token).Contains(f2));
      }



       [TestMethod]
       public void CancelFlight()
       {
           AirlineFacade.CancelFlight(Token, f2);
           Assert.IsFalse(AirlineFacade.GetAllFlights(Token).Contains(f2));
       }



       [TestMethod]
        public void UpdateFlight()
        {
            f1.Remaining_Tickets = 10;
            AirlineFacade.UpdateFlight(Token,f1);
            Flight flight = flightsDao.Get(f1.Id);
            Assert.IsTrue(flight.Remaining_Tickets==f1.Remaining_Tickets);
        }



        [TestMethod]

       public void Modify()
       {
            airline.Password = "123";
            AirlineFacade.MofidyAirlineDetails(Token,airline);
            Assert.IsTrue(airline.Password == airlineDao.Get(20).Password);
            Token.User = airline;
       }
        [TestMethod]

        public void ChangePassword()
        {
            
            AirlineFacade.ChangeMyPassword(Token,airline.Password,"newpassword");
            airline.Password = "newpassword";
            Assert.IsTrue(airline.Password == airlineDao.Get(airline.Id).Password);
        }
       
        [TestMethod]
        public void GetAllTicketstest()
        {
            List<Ticket> tickets = (List<Ticket>)AirlineFacade.GetAllTickets(Token);
            Assert.IsTrue(tickets != null);
        }
      
    }
}
