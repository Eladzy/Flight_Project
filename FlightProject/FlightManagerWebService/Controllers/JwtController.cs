using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http;
using FlightManagerProject;

namespace FlightProjectWebServices
{
    [RoutePrefix("authJwt")]
    public class JwtController : ApiController
    {
        private readonly static object key = new object();
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
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return NotAuthenticated();
            LoginService loginService = new LoginService();
            lock (key)
            {
                try
                {
                    var token = loginService.TryLogin(username, password);
                    if (token != null)
                    {
                        var jwtToken = CreateJwtToken(username);
                        return Ok(jwtToken);
                    }
                }
                catch (ExceptionWrongPassword e)
                {
                    ErrorLogger.Logger(e);
                    return NotAuthenticated();
                }
               
              
            }
            return NotAuthenticated();
        }

        private string CreateJwtToken(string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            throw new NotImplementedException();
        }
    }
}
