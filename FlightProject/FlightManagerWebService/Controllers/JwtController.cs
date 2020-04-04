﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Web.Http;
using FlightManagerProject;
using Microsoft.IdentityModel.Tokens;

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
                        var jwtToken = CreateJwtToken(token,username);
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



        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        private string CreateJwtToken(ILoginTokenBase token,string username)
        {
            DateTime issuedAt = DateTime.UtcNow;
            DateTime expired = DateTime.UtcNow.AddDays(1);


            var tokenHandler =new JwtSecurityTokenHandler();
            Type t = token.GetUser().GetType();
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim("username",username),
                new Claim(ClaimTypes.Role,t.Name)
            });


            const string secretKey = "99C4A955E7274BE9B4D78B0025E04E88D55C6783EF3446C88ACAF9EBC22D9758";
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
              var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var jwtToken =(JwtSecurityToken)tokenHandler.CreateJwtSecurityToken(
                   issuer: "https://localhost:44375/",
                   audience: "https://localhost:44375/",
                   subject: claimsIdentity,
                   notBefore: issuedAt,
                   expires: expired,
                   signingCredentials: signingCredentials);

            var tokenString = tokenHandler.WriteToken(jwtToken);

            return tokenString;
        }
    }
}
