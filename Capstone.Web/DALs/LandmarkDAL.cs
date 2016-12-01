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
                            Latitude = Convert.ToDouble(reader["latitude"]),
                            GooglePlacesID = Convert.ToString(reader["google_api_placeID"])
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

        public LandmarkModel GetLandmark(int landmarkID)
        {
            LandmarkModel landmark = new LandmarkModel();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"SELECT * 
                                                      FROM landmark
                                                      WHERE landmark.id = @landmarkID", conn);

                    cmd.Parameters.AddWithValue("@landmarkID", landmarkID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        landmark.Description = Convert.ToString(reader["landmark_description"]);
                        landmark.GooglePlacesID = Convert.ToString(reader["google_api_placeID"]);
                        landmark.ImageName = Convert.ToString(reader["image_name"]);
                        landmark.IsApproved = Convert.ToBoolean(reader["admin_approved"]);
                        landmark.Latitude = Convert.ToDouble(reader["latitude"]);
                        landmark.Longitude = Convert.ToDouble(reader["longitude"]);
                        landmark.Name = Convert.ToString(reader["name"]);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return landmark;
        }

        public bool SubmitNewLandmark(LandmarkModel landmark)
        {
            int submissionSuccessful = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO landmark ( admin_approved, image_name, landmark_description, name, longitude, latitude, google_api_placeID) 
                                                      VALUES (0, @imageName, @landmarkDescription, @name, @longitude, @latitude, @placeID )", conn);

                    cmd.Parameters.AddWithValue("@imageName", landmark.ImageName);
                    cmd.Parameters.AddWithValue("@landmarkDescription", landmark.Description);
                    cmd.Parameters.AddWithValue("@name", landmark.Name);
                    cmd.Parameters.AddWithValue("@longitude", landmark.Longitude);
                    cmd.Parameters.AddWithValue("@latitude", landmark.Latitude);
                    cmd.Parameters.AddWithValue("@placeID", landmark.GooglePlacesID);
                    submissionSuccessful = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return (submissionSuccessful != 0);
        }
        public List<LandmarkModel> GetAllUnapprovedLandmarks()
        {
            List<LandmarkModel> unapprovedLandMarks = new List<LandmarkModel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM landmark WHERE admin_approved = 0", conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        unapprovedLandMarks.Add(new LandmarkModel()
                        {
                            ID = Convert.ToInt32(reader["id"]),
                            ImageName = Convert.ToString(reader["image_name"]),
                            Name = Convert.ToString(reader["name"]),
                            Description = Convert.ToString(reader["landmark_description"]),
                            Longitude = Convert.ToDouble(reader["longitude"]),
                            Latitude = Convert.ToDouble(reader["latitude"]),
                            GooglePlacesID = Convert.ToString(reader["google_api_placeID"])
                        });
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return unapprovedLandMarks;
        }

        public bool ApproveLandmarks(List<LandmarkModel> landmarks)
        {
            int approvalSucessful = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    foreach (var landmark in landmarks)
                    {
                        SqlCommand cmd = new SqlCommand(@"UPDATE landmark 
                                                          SET admin_approved = @landmarkIsApproved 
                                                          WHERE landmark.id = @landmarkID", conn);

                        cmd.Parameters.AddWithValue("@landmarkID", landmark.ID);
                        cmd.Parameters.AddWithValue("landmarkIsApproved", landmark.IsApproved);

                        if (cmd.ExecuteNonQuery() != 0)
                        {
                            approvalSucessful++;
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            //Ask Josh: is it a problem that we don't confirm a partial success?
            return (approvalSucessful == landmarks.Count);
        }
    }
}