using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightManagerProject;

namespace FlightProjectWebServices.Controllers
{
    // [Authorize(Roles = "Administrator")]
    public class AdminController : ApiController
    {
        static Administrator administrator = new Administrator();
        
        private LoggedInAdminFacade Facade { get; set; }
        static LoginToken<Administrator> token = new LoginToken<Administrator>
        {
            User = administrator
        };
        public AdminController()
        {
            this.Facade = (LoggedInAdminFacade)FlightCenter.GetInstance().GetFacade(token);
        }
        [HttpPost]
        [Route ("api/admin/newcompany/{airline}")]
        IHttpActionResult CreateNewAirline([FromBody]AirLine airline)
        {
            if (airline == null||airline.Id==0)
            {
                return BadRequest();
            }
            try
            {
                this.Facade.CreateNewAirline(token,airline);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return NotFound();
            }
            return Ok();
        }


        [HttpPut]
        [Route("api/admin/updateairline/{airLine}")]
        IHttpActionResult UpdateAirlineDetails([FromBody]AirLine airLine)
        {
            if (airLine == null||airLine.Id==0)
                return BadRequest();
            try
            {
                this.Facade.UpdateAirlineDetails(token, airLine);
            }
            catch (Exception e)
            {

                ErrorLogger.Logger(e);
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete]
        [Route("api/admin/removeairline/{airline}")]
        IHttpActionResult RemoveAirline([FromBody]AirLine airline)
        {
            if (airline == null || airline.Id == 0)
                return BadRequest();
            try
            {
                this.Facade.RemoveAirline(token, airline);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return NotFound();

            }
            return Ok();
        }
        [HttpPost]
        [Route("api/admin/createnewcustomer/{customer}")]
        IHttpActionResult CreateNewCustomer([FromBody]Customer customer)
        {
            if (customer == null || customer.Id == 0)
            {
                return BadRequest();
            }
            try
            {
                this.Facade.CreateNewCustomer(token, customer);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return NotFound();

            }
            return Ok();
        }
        IHttpActionResult UpdateCustomerDetails([FromBody]Customer customer)
        {
            if (customer == null || customer.Id == 0)
            {
                return BadRequest();
            }
            try
            {
                this.Facade.UpdateCustomerDetails(token, customer);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return NotFound();

            }
            return Ok();
        }
        IHttpActionResult RemoveCustomer([FromBody]Customer customer)
        {
            if (customer == null || customer.Id == 0)
            {
                return BadRequest();
            }
            try
            {
                this.Facade.RemoveCustomer(token, customer);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return NotFound();

            }
            return Ok();
        }
    }
}
