using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DALs
{
    public class LandmarkDAL : ILandmarkDAL
    {
        private string _connectionString;

        public LandmarkDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<LandmarkModel> GetAllApprovedLandmarks()
        {
            List<LandmarkModel> allLandMarks = new List<LandmarkModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM landmark WHERE admin_approved = 1",conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allLandMarks.Add(new LandmarkModel()
                        {


                            ID = Convert.ToInt32(reader["id"]),
                            Name = Convert.ToString(reader["name"]),
                            Description = Convert.ToString(reader["landmark_description"]),
                            Longitude = Convert.ToDouble(reader["longitude"]),
                            Latitude = Convert.ToDouble(reader["latitude"])

                        });
                    }


                }

            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return allLandMarks;
        }

        public LandmarkModel GetLandmark()
        {
            throw new NotImplementedException();
        }
    }

}