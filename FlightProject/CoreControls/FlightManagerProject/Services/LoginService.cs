using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace FlightManagerProject
{
   public  class LoginService : ILoginService//to change and renovate the whole class
    {//ask if i can make that static
       private  AirLineMsSqlDao AirlineDao =new AirLineMsSqlDao();
       private  CustomerMsSqlDao CustomerDao = new CustomerMsSqlDao();


        public ILoginTokenBase TryLogin(string username, string password) //, out FacadeBase facade) 
        {
           // facade = null;
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return null;   
            LoginToken<Administrator> admin;
            LoginToken<AirLine> airline;
            LoginToken<Customer> customer;
            if (TryAdminLogin(username, password, out admin))
            {
                //facade = new LoggedInAdminFacade();
                return admin;
            }
            if(TryAirLineLogin(username,password,out airline))
            {
                return airline;
            }
            if(TryCustomerLogin(username,password,out customer))
            {
                return customer;
            }
            return null;
        }

        public bool TryAdminLogin(string username, string password,out LoginToken<Administrator>token)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                token = null;
                return false;
            }
                
            if (username.ToUpper() == ConfigurationUtils.adminUserName.ToUpper() )
            {
                if (password.ToUpper() == ConfigurationUtils.adminPassword.ToUpper())
                {
                    token = new LoginToken<Administrator>
                    {
                        User = new Administrator()
                    };
                    return true;
                }
                else
                {
                    token = null;
                    return false;
                    throw new ExceptionWrongPassword($"Wrong Password for user {username}");
                }
            }
            token = null;
            return false;
        }

        public bool TryAirLineLogin(string username, string password,out LoginToken<AirLine>token)
        {
           
            if (string.IsNullOrWhiteSpace(username))
            {
                token = null;
                return false;
            }
              
            AirLine airline= AirlineDao.GetAirLineByUserName(username);
            if (airline == null)
            {

                token = null;
                return false;
            } 
            if (airline.Password == password)
            {
                token = new LoginToken<AirLine>
                {
                    User = airline
                };

                return true;
            }
            else
            {
                token = null;
                return false;
                throw new ExceptionWrongPassword("Wrong Password");
            }
            
         
        }

        public bool TryCustomerLogin(string username, string password,out LoginToken<Customer>token)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                token = null;
                return false;
            }

            Customer customer = CustomerDao.GetCustomerByUserName(username);
            if (customer== null||customer.Id==0)
            {

                token = null;
                return false;
                throw new ExceptionUserNotFound("Does not Exist");
            }
            if (customer.Password == password)
            {
                token = new LoginToken<Customer>
                {
                    User = customer
                };

                return true;
            }
            else
            {
                token = null;
                return false;
                throw new ExceptionWrongPassword("Wrong Password");
            }

        }
    }
}
