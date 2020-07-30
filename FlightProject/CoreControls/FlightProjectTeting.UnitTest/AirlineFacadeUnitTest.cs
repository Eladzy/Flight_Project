using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagerProject;
using System.Collections.Generic;
using System.Reflection;
namespace FlightProjectTesting.UnitTest
{
    [TestClass]
    public class AirlineFacadeUnitTest
    {
        FlightsMsSqlDao _flightsDao = new FlightsMsSqlDao();

        AirLineMsSqlDao _airlineDao = new AirLineMsSqlDao();

        DataAccessTestingTools _testingTools = new DataAccessTestingTools();

        static LoggedInAirLineFacade _facade = new LoggedInAirLineFacade();


        [TestMethod]
        public void CreateFlights()
        {
            bool isIncluded = false;
            AirLine a = GetAirLine(TestResurces.a2);
           
            LoginToken<AirLine> loginToken = new LoginToken<AirLine>
            {
                User = a,
            };
            Flight f = new Flight()
            {
                AirLine_Id = a.Id,
                Departure_Time = DateTime.Now.AddDays(1),
                Landing_Time = DateTime.Now.AddDays(1).AddHours(1),
                Destination_Country_Code = 34,
                Origin_Country_Code = 32,
                Remaining_Tickets = 250
            };
           _facade.CreateFlight(loginToken,f);
            List<Flight> flights = (List<Flight>)_facade.GetAllComapnyFlights(loginToken);
            foreach (Flight flight in flights)
            {
                Type type = f.GetType();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo property in properties)
                {

                    if (Equals(property.GetValue(flight).ToString(), property.GetValue(f).ToString()))
                    {
                        isIncluded = true;
                    }
                    else
                    {
                        if (property.Name != "Id")
                        {
                            isIncluded = false;
                            break;
                        }
                    }
                }
                _flightsDao.Remove(flight);
                if (isIncluded)
                {
                    _airlineDao.Remove(loginToken.User);
                    Assert.IsTrue(isIncluded);
                }
            }
            Clear(loginToken);
            Assert.IsTrue(isIncluded);
        }



        [TestMethod]
        public void CancelFlight()
        {          
            AirLine a = GetAirLine(TestResurces.a2);        
            LoginToken<AirLine> loginToken = new LoginToken<AirLine>
            {
                User = a,
            };
            Flight f = new Flight()
            {
                AirLine_Id = a.Id,
                Departure_Time = DateTime.Now.AddDays(1),
                Landing_Time = DateTime.Now.AddDays(1).AddHours(1),
                Destination_Country_Code = 34,
                Origin_Country_Code = 32,
                Remaining_Tickets = 250
            };
            _facade.CreateFlight(loginToken, f);
            List<Flight> flights = (List<Flight>)_facade.GetAllComapnyFlights(loginToken);
            if (flights.Count == 0)
            {
                throw new AssertFailedException("no flights were pulled");
            }
            flights.ForEach(flight => _facade.CancelFlight(loginToken, flight));
            flights= (List<Flight>)_facade.GetAllComapnyFlights(loginToken);
            _airlineDao.Remove(loginToken.User);
            Assert.IsTrue(flights.Count == 0);
        }



        [TestMethod]
        public void UpdateFlight()
        {
            AirLine a = GetAirLine(TestResurces.a2);
           
            LoginToken<AirLine> loginToken = new LoginToken<AirLine>
            {
                User = a,
            };
            Flight f = new Flight()
            {
                AirLine_Id = a.Id,
                Departure_Time = DateTime.Now.AddDays(1),
                Landing_Time = DateTime.Now.AddDays(1).AddHours(1),
                Destination_Country_Code = 34,
                Origin_Country_Code = 32,
                Remaining_Tickets = 250
            };
            _facade.CreateFlight(loginToken, f);
            List<Flight> flights = (List<Flight>)_facade.GetAllComapnyFlights(loginToken);
            if (flights.Count == 0)
            {
                throw new AssertFailedException("no flights were pulled");
            }
            Flight flight1 = flights[0];
            flight1.Remaining_Tickets = 55;
            _facade.UpdateFlight(loginToken, flight1);
            flight1 = _flightsDao.Get(flight1.Id);
            //flights.ForEach(flight => AirlineFacade.CancelFlight(loginToken, flight));
            //_airlineDao.Remove(loginToken.User);
            Clear(loginToken);
            Assert.IsTrue(flight1.Remaining_Tickets == 55);
        }



        [TestMethod]

        public void Modify()
        {
            AirLine a = GetAirLine(TestResurces.a2);
            LoginToken<AirLine> loginToken = new LoginToken<AirLine>
            {
                User = a,
            };
            a.User_Name = "modifyTest";
            _facade.MofidyAirlineDetails(loginToken,a);
           AirLine airLine= _airlineDao.Get(a.Id);
            Clear(loginToken);
            Assert.IsTrue(airLine.User_Name == "modifyTest");
        }
        [TestMethod]

        public void ChangePassword()
        {
            AirLine a = GetAirLine(TestResurces.a2);
            LoginToken<AirLine> loginToken = new LoginToken<AirLine>
            {
                User = a,
            };

            _facade.ChangeMyPassword(loginToken, a.Password, "newpassword");
            a.Password = "newpassword";
            Assert.IsTrue(a.Password == _airlineDao.Get(a.Id).Password);
            Clear(loginToken);
        }

        [TestMethod]
        public void GetAllTicketstest()
        {
            AirLine a = _testingTools.GetAirLine();
            LoginToken<AirLine> loginToken = new LoginToken<AirLine>
            {
                User = a,
            };
            List<Ticket> tickets = (List<Ticket>)_facade.GetAllTickets(loginToken);
            Assert.IsTrue(tickets.Count>0);
        }

        AirLine GetAirLine(AirLine a)
        {
            try
            {
                _airlineDao.Add(a);
                a = _airlineDao.GetAirLineByUserName(a.User_Name);
            }
            catch (Exception)
            {

                a = _airlineDao.GetAirLineByUserName(a.User_Name);
            }
            return a;
        }

        void Clear(LoginToken<AirLine>loginToken)
        {
            List<Flight> flights = (List<Flight>)_facade.GetAllComapnyFlights(loginToken);
            flights.ForEach(flight => _facade.CancelFlight(loginToken, flight));
            _airlineDao.Remove(loginToken.User);
        }
    }
}
