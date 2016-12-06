using Capstone.Web.Models;
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

        public UserModel GetUser(string username)
        {
            UserModel user = null;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM app_user WHERE app_user.username = @username", conn);
                    cmd.Parameters.AddWithValue("@username", username);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        user = new UserModel
                        {
                            ID = Convert.ToInt32(reader["id"]),
                            Password = Convert.ToString(reader["user_password"]),
                            Username = Convert.ToString(reader["username"]),
                            Salt = Convert.ToString(reader["salt"]),
                            IsAdmin = Convert.ToBoolean(reader["is_admin"])
                        };
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return user;
        }

        public bool CreateNewUser(UserModel user)
        {
            int creationSuccessful = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("INSERT INTO app_user VALUES (@username, @password, @salt, @isAdmin)", conn);

                    cmd.Parameters.AddWithValue("@username", user.Username);
                    cmd.Parameters.AddWithValue("@password", user.Password);
                    cmd.Parameters.AddWithValue("@salt", user.Salt);
                    cmd.Parameters.AddWithValue("@isAdmin", user.IsAdmin);

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