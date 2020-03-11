using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace FlightManagerProject
{
    public class TicketsMsSqlDao : ITicketDao
    {
        static string connectionString = ConfigurationUtils.connectionString;
        /// <summary>
        /// Adds new ticket to the db
        /// </summary>
        /// <param name="t"></param>
        public void Add(Ticket t)
        {
            string query = "ADD_TICKET";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@flight", t.Flight_Id.ToString());
                    cmd.Parameters.AddWithValue("@customer", t.Customer_Id.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// retrieves a ticket by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Ticket Get(long id)
        {
            string query = "GET_TICKET";
            Ticket ticket = new Ticket();
            using (SqlConnection connection = new SqlConnection(connectionString))
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
                            ticket = new Ticket
                            {
                                Id = (long)reader["T_ID"],
                                Flight_Id=(long)reader["T_FLIGHT_ID"],
                                Customer_Id=(long)reader["T_CUSTOMER_ID"]

                            };
                            return ticket;
                        }
                        else
                        {
                            throw new TicketNotFoundException ();
                        }
                    }

                }
            }
            return ticket;
        }
        /// <summary>
        /// returns a list of all tickets
        /// </summary>
        /// <returns></returns>
        public IList<Ticket> GetAll()
        {
            string query = "GET_ALL_TICKETS";
            Ticket ticket;
            List<Ticket> tickets = new List<Ticket>();
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
                             ticket = new Ticket
                            {
                                Id = (long)reader["T_ID"],
                                Flight_Id = (long)reader["T_FLIGHT_ID"],
                                Customer_Id = (long)reader["T_CUSTOMER_ID"]

                            };
                            tickets.Add(ticket);
                        }
                    }

                }
            }
            return tickets;
        }
        /// <summary>
        /// removes tickets from the db
        /// </summary>
        /// <param name="t"></param>
        public void Remove(Ticket t)
        {
            string query="REMOVE_TICKET";
            using(SqlConnection connection=new SqlConnection(connectionString))
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
        /// updates ticket information on the db
        /// </summary>
        /// <param name="t"></param>
        public void Update(Ticket t)
        {
            string query = "UPDATE_TICKET";
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", t.Id.ToString());
                    cmd.Parameters.AddWithValue("@flight", t.Flight_Id.ToString());
                    cmd.Parameters.AddWithValue("@customer", t.Customer_Id.ToString());
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
