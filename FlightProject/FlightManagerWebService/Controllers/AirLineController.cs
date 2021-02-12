using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Description;
using FlightManagerProject;
using Newtonsoft.Json.Linq;

namespace FlightProjectWebServices
{
   // [BasicAuthAirline]
    [Authorize(Roles ="AirLine")]
    public class AirLineController : ApiController
    {

        private LoggedInAirLineFacade Facade 
        {
            get
            {
                return GetTokenFacade();
            }
        }

        private LoginToken<AirLine> Token;
       
        private LoggedInAirLineFacade GetTokenFacade()
        {
            LoginService loginService = new LoginService();
            LoggedInAirLineFacade facade;
            try
            {
                //basic auth code is here only for documentation
                //ActionContext.Request.Properties.TryGetValue("tokenResult", out object tokenResult);
                //Token = (LoginToken<AirLine>)tokenResult;
                var identity = User.Identity as ClaimsIdentity;
                Token = (LoginToken<AirLine>)loginService.TryLogin(identity.FindFirst("username").Value.ToString(), identity.FindFirst("password").Value.ToString());
                facade = (LoggedInAirLineFacade)FlightCenter.GetInstance().GetFacade(Token);
            }
            catch (Exception)
            {

                throw;
            }
           
            return facade;
        }



        [HttpGet]
        [ResponseType(typeof(List<Ticket>))]
        [Route("api/airline/getalltickets")]
        public IHttpActionResult GetAllTickets()
        {
            List<Ticket> tickets;
            try
            {
                tickets = this.Facade.GetAllTickets(Token).ToList();
                if (tickets.Count == 0 || tickets == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(tickets);
            }
            catch (SqlException e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return NotFound();
            }

        }


        [HttpPost]
        [ResponseType(typeof(List<Flight>))]
        [Route("api/airline/getflights")]
        public IHttpActionResult GetAllFlights()
        {
            List<Flight> flights;
            try
            {
                flights = this.Facade.GetAllComapnyFlights(Token).ToList();
                if (flights.Count == 0 || flights == null)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(flights);
            }
            catch (SqlException e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return NotFound();
            }

        }


        [HttpDelete]
        [ResponseType(typeof(Flight))]
        [Route("api/airline/remove/{flight}")]
        public IHttpActionResult CancelFlight([FromBody]Flight flight)
        {
            if (flight.Id == 0 || flight == null)
                return NotFound();
            try
            {
                this.Facade.CancelFlight(Token, flight);
                return Ok(flight);
            }
            catch (ExceptionFlightNotFound e)
            {
                ErrorLogger.Logger(e);
                return BadRequest();
            }
            catch (SqlException e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return NotFound();

            }

        }


        [HttpPost]
        [ResponseType(typeof(Flight))]
        [Route("api/airline/createflight")]
        public IHttpActionResult CreateFlight([FromBody]Flight flight)
        {
            
            try
            {
                this.Facade.CreateFlight(Token, flight);
                return Ok(flight);
            }
            catch (SqlException e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return NotFound();
            }

        }


        [HttpPut]
        [ResponseType(typeof(Flight))]
        [Route("api/airline/updateflight")]
        public IHttpActionResult UpdateFlight([FromBody]Flight flight)
        {
            GetTokenFacade();
            if (flight.AirLine_Id != Token.User.Id)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            
            try
            {
               
                this.Facade.UpdateFlight(Token, flight);
                return Ok(flight);
            }
            catch (ExceptionFlightNotFound e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.Forbidden);
            }
            catch (SqlException e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return NotFound();
            }

        }

        [HttpPut]
        [Route("api/airline/changemypassword")]
        public IHttpActionResult ChangeMyPassword([FromBody]string[] passwords)
        {
            try
            {
                Facade.ChangeMyPassword(Token, passwords[0], passwords[1]);
                return Ok();

            }
            catch (ExceptionWrongPassword)
            {
                return StatusCode(HttpStatusCode.Unauthorized);
            }
            catch (ArgumentException)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            catch (Exception)
            {

                return InternalServerError();
            }
        }

        [HttpPut]
        [Route("api/airline/updateself/{airline}")]
        public IHttpActionResult MofidyAirlineDetails([FromBody]AirLine airline)
        {
            if (airline != Token.User)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            try
            {
                this.Facade.MofidyAirlineDetails(Token, airline);
                return Ok(airline);
            }
            catch (ExceptionUserNotFound e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.Forbidden);
            }
            catch (SqlException e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError(e);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return NotFound();
            }

        }


        [HttpPost]
        [ResponseType(typeof(JObject))]
        [Route("api/airline/getAirlineDetails")]
        public IHttpActionResult GetAirlineDetails([FromBody]string airlineId)
        {
            JObject a = null;
            try
            {
                long id;

                long.TryParse(airlineId, out id);
                a = Facade.GetUserDetails(Token, id);
                return Ok(a);
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

        //[HttpPost]
        //[ResponseType(typeof(JObject))]
        //[Route("api/airline/submitFlight")]
        //public IHttpActionResult SubmitFlight([FromBody])

    }
}
