using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using FlightManagerProject;

namespace FlightProjectWebServices.Controllers
{
    public class AirLineController : ApiController//TODO: rethink if the id null check is neccessery or even logical
    {
        private AirLine AirlineCompany = new AirLine//temporary token
        {
            Id = 1,
            AirLine_Name = "usaAirline",
            User_Name = "asd",
            Password = "1234",
            CountryCode = 1
        };


        private LoggedInAirLineFacade Facade { get; set; }
        private LoginToken<AirLine> token = new LoginToken<AirLine>();
        public AirLineController()
        {
            token.User = AirlineCompany;
            this.Facade = (LoggedInAirLineFacade)FlightCenter.GetInstance().GetFacade(token);
        }



        [HttpGet]
        [ResponseType(typeof(List<Ticket>))]
        [Route("api/airline/getalltickets")]
        IHttpActionResult GetAllTickets()
        {
            List<Ticket> tickets;
            try
            {
                tickets = this.Facade.GetAllTickets(token).ToList();
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


        [HttpGet]
        [ResponseType(typeof(List<Flight>))]
        [Route("api/airline/getflights")]
        IHttpActionResult GetAllFlights()
        {
            List<Flight> flights;
            try
            {
                flights = this.Facade.GetAllFlights(token).ToList();
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
        IHttpActionResult CancelFlight([FromBody]Flight flight)
        {
            if (flight.Id == 0 || flight == null)
                return NotFound();
            try
            {
                this.Facade.CancelFlight(token, flight);
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
        [Route("api/airline/createflight/{flight}")]
        IHttpActionResult CreateFlight([FromBody]Flight flight)
        {

            try
            {
                this.Facade.CreateFlight(token, flight);
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
        [Route("api/airline/updateflight/{flight}")]
        IHttpActionResult UpdateFlight([FromBody]Flight flight)
        {
            if (flight.AirLine_Id != token.User.Id)
            {
                return StatusCode(HttpStatusCode.BadRequest);
            }
            try
            {
                this.Facade.UpdateFlight(token, flight);
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
        [Route("api/airline/changemypassword/{oldPassword}/{newPassword}")]
        IHttpActionResult ChangeMyPassword([FromBody]string oldPassword, [FromBody] string newPassword)
        {
            throw new NotImplementedException();//TODO check regex
        }
        [HttpPut]
        [Route("api/airline/updateself/{airline}")]
        IHttpActionResult MofidyAirlineDetails([FromBody]AirLine airline)
        {
            if (airline != token.User)
            {
                return StatusCode(HttpStatusCode.Forbidden);
            }
            try
            {
                this.Facade.MofidyAirlineDetails(token, airline);
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

    }
}
