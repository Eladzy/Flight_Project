using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightManagerProject;
using System.Text;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Net;

namespace FlightProjectWebServices
{
    /// <summary>
    /// basic auth attribute not corrently on use due to jwt token
    /// </summary>
    public class BasicAuthAdminLoginAttribute:AuthorizationFilterAttribute,IBasicAuthIUserAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unauthorized");
                return;
            }

            string AuthenticationToken = actionContext.Request.Headers.Authorization.Parameter;

            string DecodedAuthenticationToekn = Encoding.UTF8.GetString(Convert.FromBase64String(AuthenticationToken));

            string[] credentialArray = DecodedAuthenticationToekn.Split(':');

            string username = credentialArray[0];
            string password = credentialArray[1];
            LoginService loginService = new LoginService();
            LoginToken<Administrator> tokenResult;

            if(loginService.TryAdminLogin(username,password,out tokenResult))
            {
                actionContext.Request.Properties["tokenResult"] = tokenResult;
                return;
            }

            actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            return;
        }


    }
}