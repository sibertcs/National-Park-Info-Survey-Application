using Capstone.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class SurveySqlDAO : ISurveyDAO
    {
        private string connectionString;

        public SurveySqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveNewSubmisssion(SurveyModel submission)
        {
            try
            {
                // Create a new connection object
                using (var conn = new SqlConnection(connectionString))
                {
                    // Open the connection
                    conn.Open();

                    var sql = $"INSERT into survey_result values(@parkCode, @emailAddress, @state, @activityLevel)";
                    var cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@parkCode", submission.ParkCode);
                    cmd.Parameters.AddWithValue("@emailAddress", submission.EmailAddress);
                    cmd.Parameters.AddWithValue("@state", submission.State);
                    cmd.Parameters.AddWithValue("@activityLevel", submission.ActivityLevel);

                    // Execute the command
                    var reader = cmd.ExecuteNonQuery();

                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public IList<SurveyModel> GetSurveys()
        {
            IList<SurveyModel> surveys = new List<SurveyModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT COUNT(survey_result.parkCode) AS 'count', park.parkName, park.parkCode FROM survey_result JOIN park ON park.parkCode = survey_result.parkCode GROUP BY park.parkName, park.parkCode ORDER BY COUNT(survey_result.parkCode) DESC, park.parkName", conn);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    surveys.Add(MapRowToForumPost(reader));
                }
            }
            return surveys;
        }

        private SurveyModel MapRowToForumPost(SqlDataReader reader)
        {
            return new SurveyModel()
            {
                ParkName = Convert.ToString(reader["parkName"]),
                ParkCode = Convert.ToString(reader["parkCode"]),
                Count = Convert.ToInt32(reader["count"])
            };

            
        }
    }
}
