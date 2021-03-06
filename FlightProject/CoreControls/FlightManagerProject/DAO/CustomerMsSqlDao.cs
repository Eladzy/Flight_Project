﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data;
using Newtonsoft.Json.Linq;

namespace FlightManagerProject
{
    public class CustomerMsSqlDao : ICustomerDao
    {
        static string connectionString = ConfigurationUtils.connectionString;

        /// <summary>
        /// adds customer to the db
        /// </summary>
        /// <param name="customer"></param>
        public void Add(Customer customer)
        {
            string query = StProceduresConsts.ADD_CUSTOMER;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@firstName", customer.First_Name.ToString());
                    cmd.Parameters.AddWithValue("@lastName", customer.Last_Name.ToString());
                    cmd.Parameters.AddWithValue("@userName", customer.User_Name.ToString());
                    cmd.Parameters.AddWithValue("@password", customer.Password.ToString());
                    cmd.Parameters.AddWithValue("@address", customer.Address.ToString());
                    cmd.Parameters.AddWithValue("@phoneNumber", customer.Phone_Number.ToString());
                    cmd.Parameters.AddWithValue("@creditCard", customer.Credit_Card_Number.ToString());
                    connection.Open();
                    var scalar = cmd.ExecuteScalar();
                    if (scalar != null)
                        customer.Id = (long)scalar;

                }
            }
        }


        /// <summary>
        /// retrieves customer by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Customer Get(long id)
        {
            Customer customer = new Customer();
            string query = StProceduresConsts.GET_CUSTOMER_BY_ID;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            customer = new Customer
                            {
                                Id = id,
                                First_Name = (string)reader["C_FIRST_NAME"],
                                Last_Name = (string)reader["C_LAST_NAME"],
                                User_Name = (string)reader["C_USER_NAME"],
                                Password = (string)reader["C_PASSWORD"],
                                Address = (string)reader["C_ADDRESS"],
                                Phone_Number = (string)reader["C_PHONE_NUMBER"],
                                Credit_Card_Number = (string)reader["CREDIT_CARD_NUMBER"]
                            };
                        }
                        else
                        {
                            throw new ExceptionUserNotFound("Customer Not Found");
                        }
                    }

                }
                if (customer.Id == 0 || customer.First_Name == null)
                {
                    ExceptionUserNotFound e = new ExceptionUserNotFound("Sent null or white space to getCustomerByUserName");
                    ErrorLogger.Logger(e);
                    throw e;
                }
                return customer;
            }
        }


        /// <summary>
        /// returns a list of all customers
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetAll()
        {
            string query = StProceduresConsts.GET_ALL_CUSTOMERS;
            Customer customer = new Customer();
            List<Customer> customers = new List<Customer>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            customer = new Customer
                            {
                                Id = (long)reader["C_ID"],
                                First_Name = (string)reader["C_FIRST_NAME"],
                                Last_Name = (string)reader["C_LAST_NAME"],
                                User_Name = (string)reader["C_USER_NAME"],
                                Password = (string)reader["C_PASSWORD"],
                                Address = (string)reader["C_ADDRESS"],
                                Phone_Number = (string)reader["C_PHONE_NUMBER"],
                                Credit_Card_Number = (string)reader["CREDIT_CARD_NUMBER"]
                            };
                            customers.Add(customer);
                        }
                    }
                }
            }
            return customers;
        }


        /// <summary>
        /// gets customer by its username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public Customer GetCustomerByUserName(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                ExceptionUserNotFound e = new ExceptionUserNotFound("Sent null or white space to getCustomerByUserName");
                ErrorLogger.Logger(e);
                throw e;
            }
            Customer customer = new Customer();
            string query = StProceduresConsts.GET_CUSTOMER_BY_USERNAME;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user", username.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {


                        customer = new Customer
                        {
                            Id = (long)reader["C_ID"],
                            First_Name = (string)reader["C_FIRST_NAME"],
                            Last_Name = (string)reader["C_LAST_NAME"],
                            User_Name = username,
                            Password = (string)reader["C_PASSWORD"],
                            Address = (string)reader["C_ADDRESS"],
                            Phone_Number = (string)reader["C_PHONE_NUMBER"],
                            Credit_Card_Number = (string)reader["CREDIT_CARD_NUMBER"]
                        };

                    }

                }
            }
            if (customer.First_Name == null) //unnecessary?
            {
                ExceptionUserNotFound e = new ExceptionUserNotFound("username was not found");               
                
              throw e;
            }
            return customer;
        }


        /// <summary>
        /// removes customer from the db
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Customer t)
        {
            string query = StProceduresConsts.REMOVE_CUSTOMER;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// update details in the db
        /// </summary>
        /// <param name="t"></param>
        public void Update(Customer t)
        {
            string query = StProceduresConsts.UPDATE_CUSTOMER;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    cmd.Parameters.AddWithValue("@firstName", t.First_Name.ToString());
                    cmd.Parameters.AddWithValue("@lastName", t.Last_Name.ToString());
                    cmd.Parameters.AddWithValue("@userName", t.User_Name.ToString());
                    cmd.Parameters.AddWithValue("@password", t.Password.ToString());
                    cmd.Parameters.AddWithValue("@address", t.Address.ToString());
                    cmd.Parameters.AddWithValue("@phoneNumber", t.Phone_Number.ToString());
                    cmd.Parameters.AddWithValue("@creditCard", t.Credit_Card_Number.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}

