﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


namespace FlightManagerProject
{
   public  class LoginService : ILoginService//to change and renovate the whole class
    {
       private  AirLineMsSqlDao AirlineDao =new AirLineMsSqlDao();
       private  CustomerMsSqlDao CustomerDao = new CustomerMsSqlDao();

        /// <summary>
        /// determins wether the username and password belongs to any user in the database and to which archytype
        /// and yields back ILoginTokenBase result
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
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
        /// <summary>
        /// check if credntials match to an administrator
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TryAdminLogin(string username, string password,out LoginToken<Administrator>token)//todo fix temp login
        {
            Administrator administrator = new Administrator();
            if (string.IsNullOrWhiteSpace(username))
            {
                token = null;
                return false;
            }
            try
            {
                administrator = AdminMsSqlDao.GetByUsername(username);
            }
            catch (ExceptionUserNotFound)
            {

                token = null;
                return false;
            }    
            if (username == administrator.User_Name )
            {
                if (password == administrator.Password)
                {
                    token = new LoginToken<Administrator>
                    {
                        User = administrator
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
        /// <summary>
        /// check if credntials match to an airline
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TryAirLineLogin(string username, string password,out LoginToken<AirLine>token)
        {
            AirLine airline=new AirLine();
            if (string.IsNullOrWhiteSpace(username))
            {
                token = null;
                return false;
            }
            try
            {
                airline= AirlineDao.GetAirLineByUserName(username);

            }
            catch (ExceptionUserNotFound)
            {
                token = null;
                return false;
            }
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
        /// <summary>
        /// check if credntials match to a customer
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public bool TryCustomerLogin(string username, string password,out LoginToken<Customer>token)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                token = null;
                return false;
            }

            Customer customer = new Customer();
            try
            {
                  customer= CustomerDao.GetCustomerByUserName(username);

            }
            catch (ExceptionUserNotFound)
            {

                token = null;
                return false;
            }
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
