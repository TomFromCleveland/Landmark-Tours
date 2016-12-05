using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DALs
{
    public class UserDAL : IUserDAL
    {
        private string _connectionString;

        public UserDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool CreateNewUser(string username, string password)
        {
            int creationSuccessful = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("sql goes here", conn);

                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    creationSuccessful = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return (creationSuccessful == 1);
        }



    }
}