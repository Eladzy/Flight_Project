using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FlightManagerProject;


namespace FlightProjectWebServices.Controllers
{
    public class AnnonymousController : ApiController
    {
        LoginToken<IUser> token = new LoginToken<IUser>
        {
            User = null
        };
        FlightCenter instance = FlightCenter.GetInstance();

        [ResponseType(typeof(IEnumerable<Flight>))]
        [HttpGet]
        [Route("api/flights")]
        public IHttpActionResult GetAllFlights()
        {
            List<Flight> flights = instance.GetFacade(token).GetAllFlights().ToList();
            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }

        [ResponseType(typeof(IEnumerable<AirLine>))]
        [HttpGet]
        [Route("api/airlines")]
        public IHttpActionResult GetAllAirlineCompanies()
        {
            List<AirLine> airLines = instance.GetFacade(token).GetAllAirlineCompanies().ToList();
            if (airLines.Count == 0)
                return NotFound();
            return Ok(airLines);

        }


        [HttpGet]
        [ResponseType(typeof(Dictionary<Flight, int>))]
        [Route("api/flightsbyvacancy")]
        IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights = instance.GetFacade(token).GetAllFlightsVacancy();
            if (flights.Count == 0)
            {
                return NotFound();
            }
            return Ok(flights);
        }
        [HttpGet]
        [ResponseType(typeof(Flight))]
        [Route("api/getflightbyid/{id}")]
        IHttpActionResult GetFlightById([FromBody]long id)
        {
            Flight flight;
            try
            {
                flight = instance.GetFacade(token).GetFlightById(id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            if (flight == null)
                return NotFound();
            return Ok(flight);

        }


        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/getflightsbyorigin/{countryCode}")]
        IHttpActionResult GetFlightsByOriginCountry([FromBody]int countryCode)
        {
            List<Flight> flights;
            try
            {
                flights = instance.GetFacade(token).GetFlightsByOriginCountry(countryCode).ToList();
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            }
            if (flights == null || flights.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            return Ok(flights);

        }


        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/getflightsbydestination/{countryCode}")]
        IHttpActionResult GetFlightsByDestinationCountry([FromBody]int countryCode)
        {
            List<Flight> flights;
            try
            {
                flights = instance.GetFacade(token).GetFlightsByDestinationCountry(countryCode).ToList();
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            }
            if (flights == null || flights.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            return Ok(flights);
        }
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/getflightsbydeparture/{departureDate}")]
        IHttpActionResult GetFlightsByDepatrureDate([FromBody]DateTime departureDate)
        {
            List<Flight> flights;
            try
            {
                flights = instance.GetFacade(token).GetFlightsByDepatrureDate(departureDate).ToList();
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            }
            if (flights == null || flights.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            return Ok(flights);
        }


        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/getflightsbydeparture/{ladingDate}")]
        IHttpActionResult GetFlightsByLandingDate([FromBody]DateTime landingDate)
        {
            List<Flight> flights;
            try
            {
                flights = instance.GetFacade(token).GetFlightsByLandingDate(landingDate).ToList();
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            }
            if (flights == null || flights.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            return Ok(flights);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/search")]
        IHttpActionResult GetBySearch([FromBody] long flightId = 0, [FromBody]string airlineName = "", [FromBody]string originCountry = "", [FromBody]string destCountry = "", [FromBody]string depTime = "", [FromBody]string landTime = "")
        {
            return Ok();
        }
    }
}
