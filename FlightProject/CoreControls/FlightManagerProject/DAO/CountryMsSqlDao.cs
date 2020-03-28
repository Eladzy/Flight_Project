using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace FlightManagerProject
{
    public class CountryMsSqlDao : ICountryDao
    {
        static string connectionString = ConfigurationUtils.connectionString;

        /// <summary>
        /// adds a country to the db
        /// </summary>
        /// <param name="t"></param>
        public void Add(Country t)
        {
            string query = "ADD_COUNTRY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    cmd.Parameters.AddWithValue("@name", t.Country_Name);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// returns a country from the db
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Country Get(long id)
        {
            string query = "GET_COUNTRY";
            Country country = new Country();
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        if (reader.HasRows) // is this neccesray ?
                        {
                            country = new Country
                            {
                                Id = (long)reader["COU_ID"],
                                Country_Name = (string)reader["COU_COUNTRY_NAME"]
                            };
                            return country;
                        }
                        else
                        {
                            throw new ExceptionCountryNotFound($"Country not found in Get (country) by id. id = {id}");
                        }
                    }
                }
            }
            return country;
        }


        /// <summary>
        /// returns a list of all countries
        /// </summary>
        /// <returns></returns>
        public IList<Country> GetAll()
        {
            string query = "GET_ALL_COUNTRIES";
            List<Country> countries = new List<Country>();
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                            Country country = new Country
                            {
                                Id = (long)reader["COU_ID"],
                                Country_Name=(string)reader["COU_COUNTRY_NAME"],
                            };
                            countries.Add(country);
                        }
                    }
                }
            }
            return countries;
        }


        /// <summary>
        /// removes country from the db
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Country t)
        {
            string query = "REMOVE_COUNTRY";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        /// <summary>
        /// update country at the db
        /// </summary>
        /// <param name="t"></param>
        public void Update(Country t)
        { 
            string query = "UPDATE_COUNTRY";
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    cmd.Parameters.AddWithValue("@name", t.Country_Name.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}