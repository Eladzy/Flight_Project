﻿using System;
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

        [ResponseType(typeof(IEnumerable<AirLine>))]//add filter that censores username&password
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
        public IHttpActionResult GetAllFlightsVacancy()
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
        public IHttpActionResult GetFlightById([FromBody]long id)
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
        public IHttpActionResult GetFlightsByOriginCountry([FromBody]int countryCode)
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
         public IHttpActionResult GetFlightsByDestinationCountry([FromBody]int countryCode)
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
        public IHttpActionResult GetFlightsByDepatrureDate([FromBody]DateTime departureDate)
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
        public IHttpActionResult GetFlightsByLandingDate([FromBody]DateTime landingDate)
        {
            List<Flight> flights = new List<Flight>() ;
            try
            {
                flights = instance.GetFacade(token).GetFlightsByLandingDate(landingDate).ToList();
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
            }
               
            if (flights == null || flights.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            return Ok(flights);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]//todo
        [Route("api/searchFlight")]
        public IHttpActionResult GetBySearch([FromBody] long? flightId = null, [FromBody]long? airlineId = null, [FromBody]int? originCountryId = null, [FromBody]int? destinationCountryId = null, [FromBody]string depTime = null, [FromBody]string landTime = null)
        {
            DateTime? departureTime = DateTime.Parse(depTime);
            DateTime? landingTime = DateTime.Parse(landTime);
            try
            {
                List<Flight> flights = instance.GetFacade(token).SearchFlights( flightId, airlineId, originCountryId, destinationCountryId, departureTime, landingTime).ToList();
                if (flights.Count == 0)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(flights);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
            }
        }


        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]//todo
        [Route("api/searchFlightRange")]
        public IHttpActionResult GetByTimeRange([FromBody] long? flightId = null, [FromBody]long? airlineId = null, [FromBody]int? originCountryId = null, [FromBody]int? destinationCountryId = null, [FromBody]string depTime1 = null, [FromBody]string depTime2 = null, [FromBody]string landTime1 = null, [FromBody]string landTime2 = null)
        {
            DateTime? departureTime1 = DateTime.Parse(depTime1);
            DateTime? departureTime2 = DateTime.Parse(depTime2);
            DateTime? landingTime1 = DateTime.Parse(landTime1);
            DateTime? landingTime2 = DateTime.Parse(landTime2);
            try
            {
                List<Flight> flights = instance.GetFacade(token).SearchFlightsByTimeSpan(flightId, airlineId, originCountryId, destinationCountryId, departureTime1, departureTime2, landingTime1, landingTime2).ToList();
                if (flights.Count == 0)
                    return StatusCode(HttpStatusCode.NoContent);
                return Ok(flights);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
            }
        }
    }
}
