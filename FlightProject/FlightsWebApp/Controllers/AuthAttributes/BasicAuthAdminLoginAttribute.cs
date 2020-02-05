using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FlightManagerProject;
using System.Text;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;

namespace FlightsWebApp.Controllers.AuthAttributes
{
    public class BasicAuthAdminLoginAttribute:AuthorizationFilterAttribute,IBasicAuthIUserAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            
        }


    }
}