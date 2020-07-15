using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightManagerProject;
using System.Data.SqlClient;

namespace FlightProjectTesting.UnitTest
{
    class DataAccessTestingTools
    {
        private const string connectionString =
               "Data Source=.;Initial Catalog = FlightCom; Integrated Security = True";
        public Flight GetFlight()
        {
            Flight flight;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(" SELECT TOP 1 * FROM FLIGHTS WHERE F_REMAINING_TICKETS >1;"))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        flight = new Flight()
                        {
                            Id = (long)reader["F_ID"],
                            AirLine_Id = (long)reader["F_AIRLINE_ID"],
                            Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                            Destination_Country_Code = (long)reader["F_DESTINATION_COUNTRYCODE"],
                            Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                            Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                            Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                        };
                        return flight;
                    }
                    throw new ExceptionFlightNotFound();
                }
            }
        }
        public Customer GetCustomer()
        {
            Customer customer;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT TOP 1* FROM CUSTOMERS ORDER BY NEWID() "))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    connection.Open();
                    while (reader.Read())
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
                        return customer;
                    }
                   
                }
            }
            throw new ExceptionUserNotFound();
        }

        public AirLine GetAirLine()
        {
            AirLine airLine;
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(" SELECT TOP 1* FROM AIRLINES ORDER BY NEWID() ;"))
                {
                    cmd.CommandType = System.Data.CommandType.Text;
                    SqlDataReader reader = cmd.ExecuteReader();
                    connection.Open();
                    if (reader.HasRows)
                    {
                        airLine = new AirLine
                        {
                            Id = (long)reader["AL_ID"],
                            AirLine_Name = (string)reader["AL_NAME"],
                            User_Name = (string)reader["AL_USERNAME"],
                            Password = (string)reader["AL_PASSWORD"],
                            CountryCode = (long)reader["AL_COUNTRYCODE"]
                        };
                        return airLine;
                    }
                    else
                    {
                        throw new ExceptionUserNotFound("Airline Not Found");
                    }
                }
            }
        }
    }
}
