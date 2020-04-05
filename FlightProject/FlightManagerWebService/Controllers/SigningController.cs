//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using System.Web.Http.Description;
//using FlightManagerProject;

//namespace FlightManagerWebService
//{
//    public class SigningController : ApiController
//    {

//        LoginToken<IUser> token = new LoginToken<IUser>
//        {
//            User = null
//        };
//        FlightCenter instance = FlightCenter.GetInstance();



//        [HttpGet]
//        [ResponseType(typeof(bool))]
//        public IHttpActionResult IsCustomerNameAvailable(string username)
//        {
//            bool isAvailable;
//            try
//            {
//                isAvailable = instance.GetFacade(token).IsCustomerUsernameAvailable(username);
//                return Ok(isAvailable);

//            }
//            catch (Exception e)
//            {
//                ErrorLogger.Logger(e);
//                return InternalServerError();
//            }
//        }
       
//    }
//}
