using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FlightManagerProject;

namespace FlightProjectWebServices
{
    [BasicAuthAdminLogin]
   // [Authorize(Roles ="Administrator")]
    public class AdminController : ApiController
    {

        private LoginToken<Administrator> Token;
        private LoggedInAdminFacade Facade
        {
            get
            {
                return GetTokenFacade();
            }
        }

        private LoggedInAdminFacade GetTokenFacade()
        {
            LoggedInAdminFacade facade;
            ActionContext.Request.Properties.TryGetValue("tokenResult", out object tokenResult);
            this.Token = (LoginToken<Administrator>)tokenResult;
            facade = (LoggedInAdminFacade)FlightCenter.GetInstance().GetFacade(Token);
            return facade;

        }
       
   
        [HttpPost]
        [Route("api/admin/newcompany/{airline}")]
        IHttpActionResult CreateNewAirline([FromBody]AirLine airline)
        {
            if (airline == null || airline.Id == 0)
            {
                return BadRequest();
            }
            try
            {
                this.Facade.CreateNewAirline(Token, airline);
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
            if (airLine == null || airLine.Id == 0)
                return BadRequest();
            try
            {
                this.Facade.UpdateAirlineDetails(Token, airLine);
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
                this.Facade.RemoveAirline(Token, airline);
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
        public IHttpActionResult CreateNewCustomer([FromBody]Customer customer)
        {
            if (customer == null )
            {
                return BadRequest();
            }
            try
            {
                this.Facade.CreateNewCustomer(Token, customer);
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                return NotFound();

            }
            return Ok();
        }
        public IHttpActionResult UpdateCustomerDetails([FromBody]Customer customer)
        {
            if (customer == null || customer.Id == 0)
            {
                return BadRequest();
            }
            try
            {
                this.Facade.UpdateCustomerDetails(Token, customer);
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
                this.Facade.RemoveCustomer(Token, customer);
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
