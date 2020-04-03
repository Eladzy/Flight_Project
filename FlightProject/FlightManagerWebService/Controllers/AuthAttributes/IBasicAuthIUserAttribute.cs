using System.Web.Http.Controllers;

namespace FlightProjectWebServices
{
    public interface IBasicAuthIUserAttribute
    {
        void OnAuthorization(HttpActionContext actionContext);
    }
}