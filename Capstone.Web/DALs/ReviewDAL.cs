using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;
using System.Data.SqlClient;

namespace Capstone.Web.DALs
{
    public class ReviewDAL : IReviewDAL
    {
        private string _connectionString;

        public ReviewDAL(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<ReviewModel> GetReviewsForLandmark(int landmarkID)
        {
            List<ReviewModel> reviews = new List<ReviewModel>();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM review WHERE review.landmark_id = @landmarkID", conn);
                    cmd.Parameters.AddWithValue("@lalndmarkID", landmarkID);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        reviews.Add(new ReviewModel()
                        {
                            ID = Convert.ToInt32(reader["id"]),
                            UserID = Convert.ToInt32(reader["user_id"]),
                            Date = Convert.ToDateTime(reader["review_DATE"]),
                            Text = Convert.ToString(reader["text"]),
                            ThumbsUp = Convert.ToBoolean(reader["thumbs_up"]),
                            ThumbsDown = Convert.ToBoolean(reader["thumbs_down"])
                        });
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return reviews;
        }
    }
}