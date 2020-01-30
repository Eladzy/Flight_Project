﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightManagerProject;

namespace FlightProjectWebServices.Controllers
{
    public class CustomerController : ApiController
    {
        private Customer c = new Customer
        {
            First_Name = "customerName",
            User_Name = "CustomerUserName",
            Last_Name = "lastName",
            Password = "password",
            Address = "address",
            Phone_Number = "1234567890",
            Credit_Card_Number = "1234567890123456",
        };
        private LoginToken<Customer> _token;
        private LoggedInCustomerFacade _facade;
        public CustomerController()
        {
            _token = new LoginToken<Customer>();
            _token.User = c;
            try
            {
                _facade = (LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(_token);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);

            }
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/customer/allmyflights")]
        IHttpActionResult GetAllMyFlights()
        {
            List<Flight> flights = null;
            try
            {
                flights = _facade.GetAllMyFlights(_token).ToList();
                if (flights.Count == 0)
                    StatusCode(HttpStatusCode.NoContent);
                return Ok(flights);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }



        }

        [HttpPost]
        [ResponseType(typeof(Ticket))]
        [Route("api/customer/purchase/{flight}")]
        IHttpActionResult PurchaseTicket([FromBody]Flight flight)
        {
            Ticket ticket = new Ticket(); ;
            try
            {
                ticket = _facade.PurchaseTicket(_token, flight);
                if (ticket.Id == 0 || ticket == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                return Ok(ticket);
            }
            catch (ExceptionTicketSoldOut e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.NotFound);
            }
            catch (ExceptionFlightNotFound e)
            {
                ErrorLogger.Logger(e);
                return NotFound();
            }
            catch (System.Data.SqlClient.SqlException e)
            {

                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }

        }

        [HttpDelete]
        [ResponseType(typeof(Ticket))]
        [Route("api/customer/cancelticket/{ticket}")]
        IHttpActionResult CancelTicket([FromBody]Ticket ticket)
        {
            if (ticket.Customer_Id != _token.User.Id)
            {
                ExceptionTicketNotFound ex = new ExceptionTicketNotFound("Ticket Either Not Found Or Not Belond To The User");
                ErrorLogger.Logger(ex);
                return StatusCode(HttpStatusCode.Forbidden);
            }
            try
            {
                _facade.CancelTicket(_token, ticket);
                return Ok(ticket);
            }
            catch (System.Data.SqlClient.SqlException e)
            {

                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (ExceptionTicketNotFound e)
            {
                ErrorLogger.Logger(e);
                return NotFound();
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }

        }
    }
}