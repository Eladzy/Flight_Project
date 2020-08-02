using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using FlightManagerProject;
using Newtonsoft.Json.Linq;

namespace FlightProjectWebServices
{
    [AllowAnonymous]
    public class AnnonymousController : ApiController
    {
        LoginToken<IUser> token = new LoginToken<IUser>
        {
            User = null
        };
        FlightCenter instance = FlightCenter.GetInstance();

        /// <summary>
        ///  provides a list of all availalble countries in a web ready JSON format
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<JObject>))]
        [HttpGet]
        [Route("api/countries")]
        public IHttpActionResult GetCountries()
        {
            List<Country> countries = instance.GetFacade(token).GetCountries().ToList();
            if (countries.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);
            return Ok(countries);
        }


        /// <summary>
        /// provides a list of all availalble flights in a web ready JSON format
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(IEnumerable<JObject>))]
        [HttpGet]
        [Route("api/flights")]
        public IHttpActionResult GetAllFlights()
        {
            List<Flight> flights = instance.GetFacade(token).GetAllFlights().ToList();
            if (flights.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);
            return Ok(flights);
        }

        //[ResponseType(typeof(IEnumerable<AirLine>))]//move to admin controller
        //[HttpGet]
        //[Route("api/airlines")]
        //private IHttpActionResult GetAllAirlineCompanies()
        //{
        //    List<AirLine> airLines = instance.GetFacade(token).GetAllAirlineCompanies().ToList();
        //    if (airLines.Count == 0)
        //        return StatusCode(HttpStatusCode.NoContent);          
        //    return Ok(airLines);

        //}

            /// <summary>
            /// returns a list of flights with available tickets
            /// </summary>
            /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(Dictionary<Flight, int>))]
        [Route("api/flightsbyvacancy")]
        public IHttpActionResult GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flights = instance.GetFacade(token).GetAllFlightsVacancy();
            if (flights.Count == 0)          
                return NotFound();
            
            return Ok(flights);
        }
       
        /// <summary>
        /// provides flight by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(string))]
        [Route("api/getflightbyid")]
        public IHttpActionResult GetFlightById([FromUri]long id)
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
            return Ok(flight.ToJsonPresentable());

        }

        /// <summary>
        /// return a list of flights by a corresponding origin country id 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<Flight>))]
        [Route("api/getflightsbyorigin/{countryCode}")]
        public IHttpActionResult GetFlightsByOriginCountry([FromBody]int countryCode)
        {
            List<Flight> flights = null;
            try
            {
                flights = instance.GetFacade(token).GetFlightsByOriginCountry(countryCode).ToList();
            }
            catch(TicketNotFoundException e)
            {
                return Content(HttpStatusCode.BadRequest, e.Message);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                //return StatusCode(HttpStatusCode.BadRequest);//is it ok?
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            if (flights == null || flights.Count == 0)
                return StatusCode(HttpStatusCode.NoContent);//is it ok?
            return Ok(flights);

        }

        /// <summary>
        /// return a list of flights by a corresponding destination country id 
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
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

        /// <summary>
        /// return a list of flights by a arrival  date
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
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
        [ResponseType(typeof(IEnumerable<JObject>))]
        [Route("api/searchFlight/{flightId?}/{airlineId?}/{originCountryId?}/{destinationCountryId?}/{depTime?}/{landTime?}")]
        public IHttpActionResult GetBySearch([FromUri] long? flightId = null, long? airlineId = null, int? originCountryId = null,int? destinationCountryId = null, string depTime = null, string landTime = null)
        {
            DateTime? departureTime = null;
            DateTime? landingTime = null;
            if (!string.IsNullOrEmpty(depTime))
            {
                 departureTime = DateTime.Parse(depTime);
            }
            if(!string.IsNullOrEmpty(landTime))
            {
                landingTime= DateTime.Parse(landTime);
            }
            try
            {
                List<JObject> flights = instance.GetFacade(token).SearchFlights( flightId, airlineId , originCountryId , destinationCountryId , departureTime, landingTime).ToList();
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
        [ResponseType(typeof(IEnumerable<Flight>))]//todo fix parameters and  url string or remove
        [Route("api/searchFlightRange")]
        public IHttpActionResult GetByTimeRange(long? flightId, long? airlineId, int? originCountryId, int? destinationCountryId, string depTime1, string depTime2, string landTime1, string landTime2)
        {
            DateTime? departureTime1= null;
            DateTime? departureTime2 = null;
            DateTime? landingTime1 = null;
            DateTime? landingTime2 = null;
            if (depTime1 != null)
              departureTime1 =  Convert.ToDateTime(depTime1);
            if (depTime2 != null)
                departureTime2 = Convert.ToDateTime(depTime2);
            if (landTime1 != null)
                landingTime1 = Convert.ToDateTime(landTime1);
            if (landTime2 != null)
                landingTime2 = Convert.ToDateTime(landTime2);
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
        [HttpGet]
        [ResponseType(typeof(IEnumerable<JObject>))]
        [Route("api/GetAvailableFlights")]
        public  IHttpActionResult GetAvailableFlightsJson()
        {
            List<JObject> flights = new List<JObject>();
            try
            {
                 flights = instance.GetFacade(token).GetAvailableFlightsJson().ToList();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            catch(Exception e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.InternalServerError);
            }     
            return Ok(flights);
        }

        [HttpGet]
        [ResponseType(typeof(IEnumerable<JObject>))]
        [Route("api/GetAirlinesJson")]
        public IHttpActionResult GetAirlinesJson()
        {
            List<JObject> airlinesJson = new List<JObject>();
            try
            {
                airlinesJson = instance.GetFacade(token).GetAirlinesJson().ToList();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            return Ok(airlinesJson);
        }

        [HttpGet]
        [Route("api/checkCustomerUsername/")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult IsCustomerNameAvailable([FromUri]string username)
        {
            bool isAvailable;
            try
            {
                isAvailable = instance.GetFacade(token).IsCustomerUsernameAvailable(username);
                return Ok(isAvailable);
                
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("api/registerCustomer")]
       // [ResponseType(typeof(bool))]
        public IHttpActionResult RegisterCustomer(string[]userData)
        {
           if(instance.GetFacade(token).RegisterCustomer(userData[0], userData[1], userData[2], userData[3], userData[4], userData[5], userData[6], userData[7]))
           {

                //sendgrid mail
                //string[] credentials = new string[] { userData[0], userData[1] };
                //JwtController jwtController = new JwtController();
                //jwtController.Authenticate(credentials);
                return Ok(true);

           }
            return BadRequest();
        }
        [HttpGet]
        [Route("api/checkAirlineUsername/")]
        [ResponseType(typeof(bool))]
        public IHttpActionResult IsAirlineNameAvailable([FromUri]string username)
        {
            bool isAvailable;
            try
            {
                isAvailable = instance.GetFacade(token).IsAirlineusernameAvailable(username);
                return Ok(isAvailable);

            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return InternalServerError();
            }
        }
        [HttpPost]
        [Route("api/registerAirline")]
        public IHttpActionResult RegisterAirline(string[] userData)
        {
            if (instance.GetFacade(token).RegisterAirline(userData[0], userData[1], userData[2], userData[3], userData[4]))
            {
                //sendgrid mail
                //string[] credentials = new string[] { userData[0], userData[1] };
                //JwtController jwtController = new JwtController();
                //jwtController.Authenticate(credentials);
                return Ok(true);
            }
            return BadRequest();
        }
        
    }

}
