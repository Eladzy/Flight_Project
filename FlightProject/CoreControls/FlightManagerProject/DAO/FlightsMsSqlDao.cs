using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;

namespace FlightManagerProject
{
    /// <summary>
    /// 
    /// </summary>
    public class FlightsMsSqlDao : IFlightsDao
    {
        

        /// <summary>
        /// adds a new flight to the DB
        /// </summary>
        /// <param name="t"></param>
        public void Add(Flight t)
        {
            string query = StProceduresConsts.ADD_FLIGHT_STORED_PROCEDURE_NAME;

            using (SqlConnection connection=new SqlConnection(ConfigurationUtils.connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id);
                    cmd.Parameters.AddWithValue("@airline", t.AirLine_Id.ToString());
                    cmd.Parameters.AddWithValue("@origin", t.Origin_Country_Code.ToString());
                    cmd.Parameters.AddWithValue("@destination", t.Destination_Country_Code.ToString());
                    cmd.Parameters.AddWithValue("@departure", t.Departure_Time.ToString());
                    cmd.Parameters.AddWithValue("@landing", t.Landing_Time.ToString());
                    cmd.Parameters.AddWithValue("@remaining", t.Remaining_Tickets.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// gets a flight by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Flight Get(long id)
        {
           
            string query = "GET_FLIGHT";
            Flight flight = new Flight();
            using (SqlConnection connection = new SqlConnection(ConfigurationUtils.connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            flight = new Flight
                            {
                                Id = id,
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code=(long)reader["F_DESTINATION_COUNTRYCODE"],
                                Departure_Time=(DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time=(DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets=(int)reader["F_REMAINING_TICKETS"]
                            };
                            return flight;
                        }
                        else
                        {
                            throw new ExceptionFlightNotFound();
                        }
                    }
                }
            }
            return flight;
        }
        /// <summary>
        /// returns a list of all available flights
        /// </summary>
        /// <returns></returns>
        public IList<Flight> GetAll()
        {
            string query = "GET_ALL_FLIGHTS"; // .........
            List<Flight> flights = new List<Flight>();
            using(SqlConnection connection=new SqlConnection(ConfigurationUtils.connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Flight flight = new Flight
                            {
                                Id = (long)reader["F_ID"],
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code = (long)reader["F_DESTINATION_COUNTRYCODE"],
                                Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                            };
                            flights.Add(flight);
                        }
                    }

                }
            }
            return flights;
        }
        /// <summary>
        /// get all flights and their available tickets
        /// </summary>
        /// <returns></returns>
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            Dictionary<Flight, int> flightVacancy = new Dictionary<Flight, int>();
            List<Flight> flights = this.GetAll().ToList();
            flights.ForEach(f => flightVacancy.Add(f, f.Remaining_Tickets));
            return flightVacancy;
        }
        /// <summary>
        /// get list of flights by customer
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByCustomer(Customer customer)
        {
            string query = "GET_FLIGHTS_BY_CUSTOMER";
            List<Flight> flights = new List<Flight>();
            using(SqlConnection connection=new SqlConnection(ConfigurationUtils.connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", customer.Id.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Flight flight = new Flight
                            {
                                Id = (long)reader["F_ID"],
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code = (long)reader["F_DESTINATION_COUNTRYCODE"],
                                Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                            };
                            flights.Add(flight);
                        }
                        else
                        {
                            throw new ExceptionFlightNotFound();
                        }
                    }
                }
            }
            return flights;
        }
        /// <summary>
        /// returns a list of flights by a specific date of departure
        /// </summary>
        /// <param name="departureDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDepatrureDate(DateTime departureDate)
        {
            string query = "GET_FLIGHTS_BYDEPARTURE";
            List<Flight> flights = new List<Flight>();
            using (SqlConnection connection = new SqlConnection(ConfigurationUtils.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@departure", departureDate.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Flight flight = new Flight
                            {
                                Id = (long)reader["F_ID"],
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code = (long)reader["F_DESTINATION_COUNTRYCODE"],
                                Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                            };
                            flights.Add(flight);
                        }
                    }

                }
            }
            return flights;
        }
        /// <summary>
        /// returns a list of flights by a specific destination country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            string query = "GET_FLIGHTS_BY_DESTINATION_COUNTRY";
            List<Flight> flights = new List<Flight>();
            using (SqlConnection connection = new SqlConnection(ConfigurationUtils.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@country", countryCode.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Flight flight = new Flight
                            {
                                Id = (long)reader["F_ID"],
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code = (long)reader["F_DESTINATION_COUNTRYCODE"],
                                Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                            };
                            flights.Add(flight);
                        }
                    }

                }
            }
            return flights;
        }
        /// <summary>
        /// returns a list of flights by a specific landing date
        /// </summary>
        /// <param name="landingDate"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            string query = "GET_FLIGHTS_BYLAND";
            List<Flight> flights = new List<Flight>();
            using (SqlConnection connection = new SqlConnection(ConfigurationUtils.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@land", landingDate.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Flight flight = new Flight
                            {
                                Id = (long)reader["F_ID"],
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code = (long)reader["F_DESTINATION_COUNTRYCODE"],
                                Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                            };
                            flights.Add(flight);
                        }
                    }

                }
            }
            return flights;
        }
        /// <summary>
        /// returns a list of flights by origin country
        /// </summary>
        /// <param name="countryCode"></param>
        /// <returns></returns>
        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            string query = "GET_FLIGHTS_BY_ORIGIN_COUNTRY";
            List<Flight> flights = new List<Flight>();
            using (SqlConnection connection = new SqlConnection(ConfigurationUtils.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@country", countryCode.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Flight flight = new Flight
                            {
                                Id = (long)reader["F_ID"],
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (long)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code = (long)reader["F_DESTINATION_COUNTRYCODE"],
                                Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                            };
                            flights.Add(flight);
                        }
                    }

                }
            }
            return flights;
        }
        /// <summary>
        /// removes the designated flight
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Flight t)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }
            string query = "REMOVE_FLIGHT";
            using (SqlConnection connection = new SqlConnection(ConfigurationUtils.connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// updates flight details
        /// </summary>
        /// <param name="t"></param>
        public void Update(Flight t)
        {
            string query = "UPDATE_FLIGHT";
            using(SqlConnection connection=new SqlConnection(ConfigurationUtils.connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    cmd.Parameters.AddWithValue("@airline", t.AirLine_Id.ToString());
                    cmd.Parameters.AddWithValue("@origin",t.Origin_Country_Code.ToString());
                    cmd.Parameters.AddWithValue("@destination",t.Destination_Country_Code.ToString());
                    cmd.Parameters.AddWithValue("@departure", t.Departure_Time.ToString());
                    cmd.Parameters.AddWithValue("@landing", t.Landing_Time.ToString());
                    cmd.Parameters.AddWithValue("@tickets", t.Remaining_Tickets.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public IList<JObject> SearchFlight(long? id=null,long? airlineId=null,int? originCountryId=null,int? destinationCountryId=null,DateTime? departureTime=null,DateTime? landingTime=null)
        {
            string query = StProceduresConsts.SEARCH_FLIGHT;
            List<JObject> flights = new List<JObject>();
            using(SqlConnection connection=new SqlConnection(ConfigurationUtils.connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query,connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@airlineId", airlineId);
                    cmd.Parameters.AddWithValue("@origin",originCountryId );
                    cmd.Parameters.AddWithValue("@destination",destinationCountryId);
                    cmd.Parameters.AddWithValue("@departureTime",departureTime);
                    cmd.Parameters.AddWithValue("@landingTime",landingTime);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            JObject flight = new JObject(
                            new JProperty("id", reader["F_ID"]),
                            new JProperty("airlineName", reader["AL_NAME"]),
                            new JProperty("origin", reader["ORIGIN_NAME"]),
                            new JProperty("destination", reader["DESTINATION_NAME"]),
                            new JProperty("departureTime", reader["F_DEPARTURE_TIME"]),
                            new JProperty("arrivalTime", reader["F_LANDING_TIME"]),
                            new JProperty("vacancy", reader["F_REMAINING_TICKETS"])
                          );
                            flights.Add(flight);
                        }
                    }

                }
            }
            return flights;
        }


        public IList<Flight> FlightsByTimeSpan( long? id = null, long? airlineId = null, int? originCountryId = null, int? destinationCountryId = null, DateTime? departureTime1 = null, DateTime? departureTime2 = null, DateTime? landingTime1 = null, DateTime? landingTime2 = null)
        {
            string query = "GET_FLIGHTS_BY_TIMESPAN";
            List<Flight> flights = new List<Flight>();
            using (SqlConnection connection = new SqlConnection(ConfigurationUtils.connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flightId", id);
                    cmd.Parameters.AddWithValue("@airlineId", airlineId);
                    cmd.Parameters.AddWithValue("@origin", originCountryId);
                    cmd.Parameters.AddWithValue("@destination", destinationCountryId);
                    cmd.Parameters.AddWithValue("@departure1", departureTime1);
                    cmd.Parameters.AddWithValue("@departure2", departureTime2);
                    cmd.Parameters.AddWithValue("@landing1", landingTime1);
                    cmd.Parameters.AddWithValue("@landing2", landingTime2);

                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            Flight flight = new Flight
                            {
                                Id = (long)reader["F_ID"],
                                AirLine_Id = (long)reader["F_AIRLINE_ID"],
                                Origin_Country_Code = (int)reader["F_ORIGIN_COUNTRYCODE"],
                                Destination_Country_Code = (int)reader["F_DESTINATION_COUNTRYDOE"],
                                Departure_Time = (DateTime)reader["F_DEPARTURE_TIME"],
                                Landing_Time = (DateTime)reader["F_LANDING_TIME"],
                                Remaining_Tickets = (int)reader["F_REMAINING_TICKETS"]
                            };
                            flights.Add(flight);
                        }
                    }

                }
            }
            return flights;
        }


        public IList<JObject> GetAvailableFlightsJson()
        {
            string query = StProceduresConsts.GET_AVAILABLE_FLIGHTS;
            IList<JObject> flightsJson = new List<JObject>();
            using(SqlConnection connection=new SqlConnection(ConfigurationUtils.connectionString))
            {
                using (SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        JObject flight = new JObject(
                              new JProperty("id", reader["F_ID"]),
                              new JProperty("airlineName", reader["AL_NAME"]),
                              new JProperty("origin", reader["ORIGIN"]),
                              new JProperty("destination", reader["DESTINATION"]),
                              new JProperty("departureTime", reader["F_DEPARTURE_TIME"]),
                              new JProperty("arrivalTime", reader["F_LANDING_TIME"]),
                              new JProperty("vacancy", reader["F_REMAINING_TICKETS"])
                            );
                        flightsJson.Add(flight);
                        
                    }
                }
            }
            return flightsJson;
        }
    }
}
