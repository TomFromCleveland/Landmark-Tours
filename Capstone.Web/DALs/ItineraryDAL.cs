using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Capstone.Web.DALs
{
    public class ItineraryDAL : IITineraryDAL
    {
        private string _connectionString;

        public ItineraryDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool CreateNewItinerary(ItineraryModel itinerary)
        {
            int newItineraryAdded = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO itinerary (name, itinerary_date, user_id, startingLatitude, startingLongitude) 
                                                      VALUES (@name, @itineraryDate, @userID, @startingLatitude, @startingLongitude)", conn);

                    cmd.Parameters.AddWithValue("@name", itinerary.Name);
                    cmd.Parameters.AddWithValue("@itineraryDate", itinerary.Date);
                    cmd.Parameters.AddWithValue("@userID", itinerary.UserID);
                    cmd.Parameters.AddWithValue("@startingLatitude", itinerary.StartingLatitude);
                    cmd.Parameters.AddWithValue("@startingLongitude", itinerary.StartingLongitude);

                    newItineraryAdded = cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }
            return (newItineraryAdded != 0);
        }

        public bool AddItineraryLandmarks(ItineraryModel itinerary)
        {
            int additionSucess = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(@"INSERT INTO itinerary_landmark (itinerary_id, landmark_id)
                                                      VALUES (@itineraryID, @landmarkID)", conn);

                    cmd.Parameters.AddWithValue("@itineraryID", itinerary.ID);
                    cmd.Parameters.Add("@landmarkID", SqlDbType.Int);

                    foreach (var landmark in itinerary.LandmarkList)
                    {
                        cmd.Parameters["@landmarkID"].Value = landmark.ID;
                        additionSucess += cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);                
            }

            return (additionSucess == itinerary.LandmarkList.Count);
        }

        public bool DeleteItinerary(ItineraryModel itinerary)
        {
            bool itineraryDeletion = false;
            int linkTableDeletions = 0;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(@"DELETE FROM itinerary_landmark 
                                                      WHERE itinerary_landmark.itinerary_id = @itineraryID
                                                      AND itinerary_landmark.landmark_id = @landmarkID", conn);

                    cmd.Parameters.AddWithValue("@itineraryID", itinerary.ID);
                    cmd.Parameters.Add("@landmarkID", SqlDbType.Int);

                    foreach (var landmark in itinerary.LandmarkList)
                    {
                        cmd.Parameters["@landmarkID"].Value = landmark.ID;

                        linkTableDeletions += cmd.ExecuteNonQuery();
                    }

                    cmd.CommandText = @"DELETE FROM itinerary
                                        WHERE itinerary.id = @itineraryID";

                    itineraryDeletion = (cmd.ExecuteNonQuery() != 0);
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
            }

            return (itineraryDeletion && (linkTableDeletions == itinerary.LandmarkList.Count));
        }



    }
}