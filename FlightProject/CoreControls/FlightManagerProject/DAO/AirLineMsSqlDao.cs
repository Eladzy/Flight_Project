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
    public class AirLineMsSqlDao : IAirLineDao
    {
        static string connectionString = ConfigurationUtils.connectionString;



        public AirLineMsSqlDao()
        {

        }

        /// <summary>
        /// adds airline to the DB
        /// </summary>
        /// <param name="t"></param>

        public void Add(AirLine t)
        {
            if (t == null)
                throw new ArgumentNullException();

            string query = StProceduresConsts.ADD_AIRLINE;
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", t.AirLine_Name.ToString());
                        cmd.Parameters.AddWithValue("@userName", t.User_Name.ToString());
                        cmd.Parameters.AddWithValue("@password", t.Password.ToString());
                        cmd.Parameters.AddWithValue("@countryCode", t.CountryCode.ToString());
                        connection.Open();
                        var scalar = cmd.ExecuteScalar();
                        if (scalar != null)
                            t.Id = (long)scalar;

                    }
                }
            }
            catch (Exception e)
            {
                ErrorLogger.Logger(e);
                throw e;
            }
        }


        /// <summary>
        /// retrieves airline by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AirLine Get(long id)
        {
            AirLine airLine = new AirLine();
            string query = StProceduresConsts.GET_AIRLINE;
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
                            airLine = new AirLine
                            {
                                Id = id,
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
            return airLine;
        }


        /// <summary>
        /// retrives airline by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public AirLine GetAirLineByUserName(string username)
        {
            if (username == null)
            {
                throw new ArgumentNullException();
            }
            string query = StProceduresConsts.GET_AIRLINE_BYUSER;
            AirLine airLine = new AirLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@user", username);
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            airLine = new AirLine
                            {
                                Id = (long)reader["AL_ID"],
                                AirLine_Name = (string)reader["AL_NAME"],
                                User_Name = username,
                                Password = (string)reader["AL_PASSWORD"],
                                CountryCode = (long)reader["AL_COUNTRYCODE"]
                            };

                        }
                        else
                        {
                            throw new ExceptionUserNotFound("Airline Not Found");
                        }
                    }
                }
            }
            return airLine;
        }


        /// <summary>
        /// returns a list of all airlines
        /// </summary>
        /// <returns></returns>
        public IList<AirLine> GetAll()
        {
            List<AirLine> airLines = new List<AirLine>();
            string query = StProceduresConsts.GET_ALL_AIRLINES;
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
                            AirLine airLine = new AirLine
                            {
                                Id = (long)reader["AL_ID"],
                                AirLine_Name = (string)reader["AL_NAME"],
                                User_Name = (string)reader["AL_USERNAME"],
                                Password = (string)reader["AL_PASSWORD"],
                                CountryCode = (long)reader["AL_COUNTRYCODE"]
                            };
                            airLines.Add(airLine);
                        }
                    }
                }
            }
            return airLines;
        }


        /// <summary>
        /// returns a list of airlines by country code
        /// </summary>
        /// <param name="countryId"></param>
        /// <returns></returns>
        public IList<AirLine> GetAllAirLinesByCountry(long countryId)
        {
            string query = StProceduresConsts.GET_AIRLINES_BY_COUNTRY;
            List<AirLine> airLines = new List<AirLine>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@countryCode", countryId.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            AirLine airLine = new AirLine
                            {
                                Id = (long)reader["AL_ID"],
                                AirLine_Name = (string)reader["AL_NAME"],
                                User_Name = (string)reader["AL_USERNAME"],
                                Password = (string)reader["AL_PASSWORD"],
                                CountryCode = (long)reader["AL_COUNTRYCODE"]
                            };
                            airLines.Add(airLine);
                        }
                    }
                }
            }
            return airLines;
        }


        /// <summary>
        /// removes airlines from DB
        /// </summary>
        /// <param name="t"></param>
        public void Remove(AirLine t)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }
            string query = StProceduresConsts.REMOVE_AIRLINE;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id);
                    cmd.ExecuteNonQuery();
                }

            }
        }


        /// <summary>
        /// udpates airline details in the db
        /// </summary>
        /// <param name="t"></param>
        public void Update(AirLine t)
        {
            if (t == null)
            {
                throw new ArgumentNullException();
            }
            string query = StProceduresConsts.UPDATE_AIRLINE;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id);
                    cmd.Parameters.AddWithValue("@name", t.AirLine_Name.ToString());
                    cmd.Parameters.AddWithValue("@userName", t.User_Name.ToString());
                    cmd.Parameters.AddWithValue("@password", t.Password.ToString());
                    cmd.Parameters.AddWithValue("@countryCode", t.CountryCode.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        public IList<JObject> GetAirlinesJson()
        {
            List<JObject> airLines = new List<JObject>();
            string query = StProceduresConsts.GET_PRESENTABLE_AIRLINES;
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
                            JObject airline = new JObject(
                              new JProperty("id", reader["AL_ID"]),
                              new JProperty("name", reader["AL_NAME"]),
                              new JProperty("countryName", reader["COU_COUNTRY_NAME"])
                              );
                            airLines.Add(airline);
                        }
                    }
                }
            }
            return airLines;
        }
    }
}

