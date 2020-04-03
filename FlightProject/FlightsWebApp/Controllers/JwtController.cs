using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http;
using FlightManagerProject;

namespace FlightsWebApp.Controllers
{
    [RoutePrefix("authJwt")]
    public class JwtController : ApiController
    {
        [HttpGet]
        [Authorize]
        [Route("authSucceed")]
        public IHttpActionResult Authenticated()
        {
            return Ok("authenticated");
        }
        [HttpGet]
        [Route("authFailed")]
        public IHttpActionResult NotAuthenticated()
        {
           return Unauthorized();
        }

        [HttpPost]
        [Route("authenticate")]
        public IHttpActionResult Authenticate([FromBody]string username,string password)
        {
            LoginService loginService = new LoginService();

            throw new NotImplementedException();
        }
    }
}
