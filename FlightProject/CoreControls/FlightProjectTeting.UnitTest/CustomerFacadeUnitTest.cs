using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightManagerProject;
using System.Diagnostics;
using System.Collections.Generic;

namespace FlightProjectTesting.UnitTest
{
    [TestClass]
    public class CustomerFacadeTest
    {
        CustomerMsSqlDao customerDao = new CustomerMsSqlDao();
        DataAccessTestingTools testingTools = new DataAccessTestingTools();
        TicketsMsSqlDao _ticketsDao = new TicketsMsSqlDao();
       
        static Customer _customer = TestResurces.c1;
        public LoginToken<Customer> _token = new LoginToken<Customer>
        {
            User = _customer
        };
        public static LoggedInCustomerFacade customerFacade=new LoggedInCustomerFacade();
        
       
        [TestMethod]
        public void CustomerAdd()
        {
            customerDao.Add(_customer);
             var customer2= customerDao.GetCustomerByUserName(_customer.User_Name);
            Assert.IsTrue(customer2.Id != 0);
            
        }

      

        [TestMethod]
        public void GetMyFlights()
        {
            Customer c = testingTools.GetCustomer();
            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            List<Flight> flights = (List<Flight>)customerFacade.GetAllMyFlights(loginToken);
            Assert.IsTrue(flights.Count > 0);
            
        }
        [TestMethod]
        public void TicketTesting()
        {
            Customer c;
            try
            {
                c= customerDao.GetCustomerByUserName(_customer.User_Name);
            }
            catch (Exception)
            {

                customerDao.Add(_customer);
                c = customerDao.GetCustomerByUserName(_customer.User_Name);
            }
           
            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            var ticket= customerFacade.PurchaseTicket(loginToken,testingTools.GetFlight());
             Assert.IsTrue(ticket ==_ticketsDao.Get(ticket.Id));

        }
        [TestMethod]
        public void CancelTicket()
        {
            //customerDao.Add(c);
            //LoginToken<Customer> loginToken = new LoginToken<Customer>
            //{
            //    User = c
            //};
            //var ticket = customerFacade.PurchaseTicket(loginToken, customerFacade.GetFlightById(2));
            ////customerFacade.CancelTicket(loginToken, ticket);
            //Assert.IsTrue(customerFacade.GetAllMyFlights(loginToken).Count == 0);
        }
        private void TicketAssertScenarioA()
        {

        }
        private void TicketAssertScenarioB()
        {

        }
    }
}

//[TestMethod]
//public void GetFlightByID()
//{

//    var customerFacade = flightCenter.GetFacade(token);
//    Flight flight =customerFacade.GetFlightById(1);
//    Assert.IsNotNull(flight);
//}

//[TestMethod]
//public void GetAirlines()
//{
//    List<AirLine> airLines = (List<AirLine>)customerFacade.GetAllAirlineCompanies();
//    Assert.IsTrue(airLines.Count > 1);
//}
//[TestMethod]
//public void GetFlightVacancy()
//{
//    Dictionary<Flight, int> vacancy = customerFacade.GetAllFlightsVacancy();
//    Assert.IsTrue(vacancy.Count > 0);
//}
//[TestMethod]
//public void GetByDeparture()
//{
//    var customerFacade = flightCenter.GetFacade(token);
//    Flight flight = customerFacade.GetFlightById(1);
//    List<Flight> flights = (List<Flight>)customerFacade.GetFlightsByDepatrureDate(flight.Departure_Time);
//    Assert.IsTrue(flights.Contains(flight));
//}
//[TestMethod]
//public void GetByLanding()
//{
//    var customerFacade = flightCenter.GetFacade(token);
//    Flight flight = customerFacade.GetFlightById(2);
//    List<Flight> flights = (List<Flight>)customerFacade.GetFlightsByLandingDate(flight.Landing_Time);
//    Assert.IsTrue(flights.Contains(flight));
//}