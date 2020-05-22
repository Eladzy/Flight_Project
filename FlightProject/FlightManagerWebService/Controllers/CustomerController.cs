using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using FlightManagerProject;
using Newtonsoft.Json.Linq;

namespace FlightProjectWebServices//test pending
{
   // [BasicAuthCustomerAttribute]
    [Authorize(Roles ="Customer")]
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
            //_token = new LoginToken<Customer>();---->Basic auth
            //Request.Properties.TryGetValue("tokenResult", out object tokenResult);---->Basic auth
            //_token = (LoginToken<Customer>)tokenResult;---->Basic auth
            LoggedInCustomerFacade facade =null;
            LoginService loginService = new LoginService();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                _token = (LoginToken<Customer>)loginService.TryLogin(identity.FindFirst("username").Value.ToString(), identity.FindFirst("password").Value.ToString());
                facade = (LoggedInCustomerFacade)FlightCenter.GetInstance().GetFacade(_token);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
            }
            return facade;
        }

        [HttpPost]
        [ResponseType(typeof(JObject))]
        [Route("api/customer/getCustomerDetails")]
        public IHttpActionResult GetCustomerDetails([FromBody]string customerId)
        {
            JObject c = null;
            try
            {
                long id;
                
                long.TryParse(customerId, out id);
                c = _facade.GetUserDetails(_token, id);
                return Ok(c);
            }
            catch (ExceptionUserNotFound e)
            {

                ErrorLogger.Logger(e);
                return BadRequest();
            }
            catch(Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
            }
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
        [Route("api/customer/purchase")]
        public IHttpActionResult PurchaseTicket([FromBody]string flightId)
        {          
            Flight flight = null;
            try
            {
                long id;
                if (long.TryParse(flightId, out id))
                {
                    flight = _facade.GetFlightById(id);
                }
                else
                {
                    return BadRequest("invalid id input format");
                }
            }
            catch (ExceptionFlightNotFound e)
            {
              
                ErrorLogger.Logger(e);
                return BadRequest("flight Id not found");
            }
            catch(Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
            }
            Ticket ticket = new Ticket(); 
            try
            {
                ticket = _facade.PurchaseTicket(_token, flight);
                if ( ticket == null)
                {
                    return StatusCode(HttpStatusCode.NotFound);
                }
                return Ok(ticket);
            }
            catch (ExceptionTicketSoldOut e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.BadRequest);
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

        [HttpPost]
        [ResponseType(typeof(IEnumerable<JObject>))]
        [Route("api/customer/getmyflightsjson/")]
        public IHttpActionResult GetMyFlightsJson([FromBody]string id)
        {
            try
            {
                long userId;
                long.TryParse(id, out userId);
                IList<JObject> flights = _facade.GetCustomerFlightsJson(_token, userId);
                return Ok(flights);
            }
            catch (ExceptionUserNotFound e)
            {
                ErrorLogger.Logger(e);
                return BadRequest();
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
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

        [HttpPut]
        [ResponseType(typeof(JObject))]
        [Route("api/customer/updateDetails")]
        public IHttpActionResult UpdateDetails([FromBody]string[] userData)
        {
            long id;
            if(long.TryParse(userData[0],out id))
            {
                try
                {
                     return Ok( _facade.UpdateCustomerDetails(_token, id, userData[1], userData[2], userData[3], userData[4]));

                }
                catch (Exception e)
                {
                    ErrorLogger.Logger(e);
                    return InternalServerError();
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [ResponseType(typeof(bool))]
        [Route("api/customer/changePassword")]
        public IHttpActionResult ChangePassword([FromBody] string[] passwords)
        {
            try
            {
            if (_facade.ChangePassword(_token,passwords[0], passwords[1]))
            {
                return Ok(true);
            }
            else
            {
                    return Ok(false);
                }

            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }
    }
}
