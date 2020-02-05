using System.Web.Http.Controllers;

namespace FlightsWebApp.Controllers
{
    public interface IBasicAuthIUserAttribute
    {
        void OnAuthorization(HttpActionContext actionContext);
    }
}