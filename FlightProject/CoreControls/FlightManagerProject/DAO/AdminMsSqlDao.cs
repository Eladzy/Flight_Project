using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace FlightManagerProject
{
    static class AdminMsSqlDao
    {
        static string connectionString = ConfigurationUtils.connectionString;
       

        public static Administrator Get(long id)
        {
            string query = StProceduresConsts.GET_ADMIN_BY_ID;
            Administrator administrator = new Administrator();
  
            using(SqlConnection connection=new SqlConnection(connectionString))
            {
                using(SqlCommand cmd=new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id.ToString());
                    connection.Open();
                    var reader = cmd.ExecuteReader(default);
                    while (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                             administrator = new Administrator()
                             {
                                Id = (long)reader["ADMIN_ID"],
                                User_Name = (string)reader["ADMIN_USER_NAME"],
                                Password = (string)reader["ADMIN_PASSWORD"]
                            };
                        }
                    }
                    
                }
                if (administrator.Id != 0)
                {
                    return administrator;
                }
                throw new ExceptionUserNotFound();

            }
        }

        public static Administrator GetByUsername(string username)
        {
            string query = StProceduresConsts.GET_ADMIN_BY_USER_NAME;
            Administrator administrator = new Administrator();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@username", username.ToString());
                    connection.Open();
                    var reader = cmd.ExecuteReader(CommandBehavior.Default);
                    while (reader.Read())
                    {
                        administrator = new Administrator()
                        {
                           Id = (long)reader["ADMIN_ID"],
                           User_Name = (string)reader["ADMIN_USER_NAME"],
                           Password = (string)reader["ADMIN_PASSWORD"]
                        };                      
                    }

                }
                if (administrator.Id != 0)
                {
                    return administrator;
                }
                throw new ExceptionUserNotFound();

            }
        }
    }
}
