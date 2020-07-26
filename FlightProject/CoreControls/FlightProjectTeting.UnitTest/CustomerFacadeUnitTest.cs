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
        CustomerMsSqlDao _customerDao = new CustomerMsSqlDao();
        DataAccessTestingTools testingTools = new DataAccessTestingTools();
        TicketsMsSqlDao _ticketsDao = new TicketsMsSqlDao();
        static Customer _customer = TestResurces.c1;
        public static LoggedInCustomerFacade customerFacade=new LoggedInCustomerFacade();
        
      

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
                c= _customerDao.GetCustomerByUserName(_customer.User_Name);
            }
            catch (Exception)
            {

                _customerDao.Add(_customer);
                c = _customerDao.GetCustomerByUserName(_customer.User_Name);
            }
           
            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            var ticket= customerFacade.PurchaseTicket(loginToken,testingTools.GetFlight());
             Assert.IsTrue(ticket ==_ticketsDao.Get(ticket.Id));

        }

        [TestMethod]
        public void PurchaseFlight()
        {
            Customer c;
            try
            {
                c = _customerDao.GetCustomerByUserName(_customer.User_Name);
            }
            catch (Exception)
            {

                _customerDao.Add(_customer);
                c = _customerDao.GetCustomerByUserName(_customer.User_Name);
            }

            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            var ticket = customerFacade.PurchaseTicket(loginToken, testingTools.GetFlight());
            Assert.IsTrue(ticket == _ticketsDao.GetTicketByInfo(loginToken.User.Id, ticket.Flight_Id));
        }

        [TestMethod]
        [ExpectedException(typeof(TicketNotFoundException))]
        public void CancelTicket()
        {
            Customer c;
            try
            {
                c = _customerDao.GetCustomerByUserName(_customer.User_Name);
            }
            catch (Exception)
            {

                _customerDao.Add(_customer);
                c = _customerDao.GetCustomerByUserName(_customer.User_Name);
            }

            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            var ticket = customerFacade.PurchaseTicket(loginToken, testingTools.GetFlight());
            customerFacade.CancelTicket(loginToken, ticket.Flight_Id);
            _ticketsDao.GetTicketByInfo(loginToken.User.Id,ticket.Flight_Id);
  
        }

        [TestMethod]
        public void UpdateCustomer()
        {
            Customer c = TestResurces.c3;
            _customerDao.Add(c);
            c = _customerDao.GetCustomerByUserName(c.User_Name);
            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            var facade=(LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(loginToken);
            facade.UpdateCustomerDetails(loginToken, c.Id, "testName", "testLName", "0123456789", "testAddress");
            c = _customerDao.Get(c.Id);
            Assert.IsTrue(c.First_Name == "testName" && c.Last_Name == "testLName"&& c.Address == "testAddress");
        }

        [TestMethod]
        public void ChangePassword()
        {
            Customer c;
            try
            {
                c = _customerDao.GetCustomerByUserName(_customer.User_Name);
            }
            catch (Exception)
            {

                _customerDao.Add(_customer);
                c = _customerDao.GetCustomerByUserName(_customer.User_Name);
            }
            LoginToken<Customer> loginToken = new LoginToken<Customer>
            {
                User = c
            };
            string newPassword = "TestNewPassword1";
            var facade = (LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(loginToken);
            facade.ChangePassword(loginToken, c.Password, newPassword);
            c= _customerDao.GetCustomerByUserName(_customer.User_Name);
            Assert.IsTrue(c.Password == newPassword);
        }
    }
}
