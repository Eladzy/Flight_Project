using FlightManagerProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FlightsWebApp.Controllers.AuthAttributes
{
    public class BasicAuthAirlineAttribute : AuthorizationFilterAttribute,IBasicAuthIUserAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //null check
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
                return;
            }


            string authenticationToken = actionContext.Request.Headers.Authorization.Parameter;

            string decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));

            string[] credentialsArray = decodedAuthenticationToken.Split(':');

            string username = credentialsArray[0];
            string password = credentialsArray[1];
            

            LoginToken<AirLine> tokenResult;
            LoginService loginService = new LoginService();
            if (loginService.TryAirLineLogin(username, password, out tokenResult))
            {
                if (tokenResult.User.User_Name == username && tokenResult.User.Password == password)
                {
                    actionContext.Request.Properties["tokenResult"] = tokenResult;
                    return;
                }
            }          

            // stops the request
            actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Unauthorized, "Unauthorized");
            return;
        }
    }
}