using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using FlightManagerProject;

namespace AuthServer.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        [HttpPost("token")]
        public ActionResult GetToken()
        {
            //security key
            string securityKey = "Super_Long_Security_KEY_For_Token_Validation_And_Auth_Project_2019_11_11$smesk.in";

            //symmetrical key
            var symmetricalSecureKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

            //signing credentials
            var signingCredentials = new SigningCredentials(symmetricalSecureKey, SecurityAlgorithms.HmacSha256Signature);
            
            //create token
            var token = new JwtSecurityToken(
                issuer: "smesk.in",
                audience: "readers",
                expires: DateTime.Now.AddHours(1),
                signingCredentials :signingCredentials

                );
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }

    }
}