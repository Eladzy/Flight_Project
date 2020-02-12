using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagerProject;
using System.Diagnostics;
using System.Collections.Generic;

namespace FlightProjectTeting.UnitTest
{
    [TestClass]
    public class CustomerFacadeTest
    {
        CustomerMsSqlDao customerDao = new CustomerMsSqlDao();
        FlightCenter flightCenter = FlightCenter.GetInstance();
        Customer customer = new Customer
        {
            Id=232323,
            First_Name = "facadeTest",
            Last_Name = "1tr",
            User_Name="customerFacade",
            Password="12345678",
            Address="1dsf",
            Phone_Number="1234567890",
            Credit_Card_Number="1234567890"
            
        };
        
        public LoginToken<Customer>token ;
        public static LoggedInCustomerFacade customerFacade;
        public CustomerFacadeTest()
        {
            token = new LoginToken<Customer>
            {
                User = customer
            };
            customerFacade = (LoggedInCustomerFacade)flightCenter.GetFacade(token);
        }
        [TestMethod]
        public void StartMethod()
        {
            customerDao.Add(customer);
             var customer2= customerDao.GetCustomerByUserName(customer.User_Name);
            Assert.IsTrue(customer2.Id != 0);
        }
        [TestMethod]
        public void GetFlightByID()
        {
            Debug.Print("2nd");
            var customerFacade = flightCenter.GetFacade(token);
            Flight flight =customerFacade.GetFlightById(1);
            Assert.IsNotNull(flight);
        }
        [TestMethod]
        public void GetFlightList()
        {
            List<Flight> flights = (List<Flight>)customerFacade.GetAllFlights();
            Assert.IsTrue(flights.Count > 1);
        }
        [TestMethod]
        public void GetAirlines()
        {
            List<AirLine> airLines = (List<AirLine>)customerFacade.GetAllAirlineCompanies();
            Assert.IsTrue(airLines.Count > 1);
        }
        [TestMethod]
        public void GetFlightVacancy()
        {
            Dictionary<Flight, int> vacancy = customerFacade.GetAllFlightsVacancy();
            Assert.IsTrue(vacancy.Count > 0);
        }
        [TestMethod]
        public void GetByDeparture()
        {
            var customerFacade = flightCenter.GetFacade(token);
            Flight flight = customerFacade.GetFlightById(1);
            List<Flight> flights = (List<Flight>)customerFacade.GetFlightsByDepatrureDate(flight.Departure_Time);
            Assert.IsTrue(flights.Contains(flight));
        }
        [TestMethod]
        public void GetByLanding()
        {
            var customerFacade = flightCenter.GetFacade(token);
            Flight flight = customerFacade.GetFlightById(2);
            List<Flight> flights = (List<Flight>)customerFacade.GetFlightsByLandingDate(flight.Landing_Time);
            Assert.IsTrue(flights.Contains(flight));
        }
        [TestMethod]
        public void GetMyFlights()
        {
            LoginToken<Customer> loginToken = new LoginToken<Customer>();
            Customer c = customerDao.GetCustomerByUserName("CustomerUserName");
            loginToken.User = c;
            List<Flight> flights = (List<Flight>)customerFacade.GetAllMyFlights(loginToken);
            Assert.IsTrue(flights.Count > 0);
            
        }
        [TestMethod]
        public void PurchaseTicket()
        {
            var ticket= customerFacade.PurchaseTicket(token, customerFacade.GetFlightById(2));
            Assert.IsTrue(ticket is Ticket && ticket != null);
             
        }
        [TestMethod]
        public void CancelTicket()
        {
            Customer c = new Customer
            {
                Id=354545,
                First_Name = "canceltest",
                Last_Name = "13asd",
                User_Name = "canceltest",
                Address = "adssad",
               Password="324rfdsfsd",
               Credit_Card_Number="1234567891234567",
               Phone_Number="1234567890",
            
            };
            customerDao.Add(c);
            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            var ticket = customerFacade.PurchaseTicket(loginToken, customerFacade.GetFlightById(2));
            customerFacade.CancelTicket(loginToken, ticket);
            Assert.IsTrue(customerFacade.GetAllMyFlights(loginToken).Count == 0);
        }
    }
}
