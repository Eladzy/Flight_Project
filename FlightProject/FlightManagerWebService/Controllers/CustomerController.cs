using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightManagerProject;

namespace FlightProjectWebServices//test pending
{
    [BasicAuthCustomerAttribute]
    public class CustomerController : ApiController
    {

        private LoginToken<Customer> _token;
        private LoggedInCustomerFacade _facade
        {
            get
            {
                return GetTokenFacade();
            }
        }
        public LoggedInCustomerFacade GetTokenFacade()
        {
            _token = new LoginToken<Customer>();
            LoggedInCustomerFacade facade=null;
            Request.Properties.TryGetValue("tokenResult", out object tokenResult);
            _token = (LoginToken<Customer>)tokenResult;
            try
            {               
                facade = (LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(_token);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
            }
            return facade;
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/customer/allmyflights")]
        public IHttpActionResult GetAllMyFlights()
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
        public IHttpActionResult PurchaseTicket([FromBody]Flight flight)
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
        public IHttpActionResult CancelTicket([FromBody]Ticket ticket)
        {
            if (ticket.Customer_Id != _token.User.Id)
            {
                TicketNotFoundException  ex = new TicketNotFoundException ("Ticket Either Not Found Or Not Belond To The User");
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
            catch (TicketNotFoundException  e)
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
