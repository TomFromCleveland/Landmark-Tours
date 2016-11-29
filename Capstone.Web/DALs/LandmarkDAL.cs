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
                    SqlCommand cmd = new SqlCommand("SELECT * FROM landmark WHERE admin_approved = 1", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        allLandMarks.Add(new LandmarkModel()
                        {
                            ID = Convert.ToInt32(reader["id"]),
                            ImageName = Convert.ToString(reader["image_name"]),
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

        public bool SubmitNewLandmark(LandmarkModel landmark)
        {
            int submissionSuccessful = 0;
                        
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO landmark ( admin_approved, image_name, landmark_description, name, longitude, latitude) VALUES (0, @imageName, @landmarkDescription, @name, @longitude, @latitude )", conn);
                    cmd.Parameters.AddWithValue("@imageName", landmark.ImageName);
                    cmd.Parameters.AddWithValue("@landmarkDescription", landmark.Description);
                    cmd.Parameters.AddWithValue("@name", landmark.Name);
                    cmd.Parameters.AddWithValue("@longitude", landmark.Longitude);
                    cmd.Parameters.AddWithValue("@latitude", landmark.Latitude);

                    submissionSuccessful = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return (submissionSuccessful != 0);
        }
    }
}