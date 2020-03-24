using Capstone.Web.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Capstone.Web.DAL
{
    public class WeatherSqlDAO : IWeatherDAO
    {
        private string connectionString;

        public WeatherSqlDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public IList<WeatherModel> GetWeather(string parkCode)
        {
            IList<WeatherModel> weather = new List<WeatherModel>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM weather WHERE parkCode = @parkCode", conn);
                cmd.Parameters.AddWithValue("@parkCode", parkCode);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    weather.Add(MapRowToProduct(reader));
                }
            }
            return weather;


        }
        private WeatherModel MapRowToProduct(SqlDataReader reader)
        {
            WeatherModel wm = new WeatherModel();
            wm.ParkCode = Convert.ToString(reader["parkCode"]);
            wm.ForecastDay = Convert.ToInt32(reader["fiveDayForecastValue"]);
            wm.Low = Convert.ToInt32(reader["low"]);
            wm.High = Convert.ToInt32(reader["high"]);
            wm.Forecast = Convert.ToString(reader["forecast"]);
            return wm;
            //return new WeatherModel()
            //{
            //    ParkCode = Convert.ToString(reader["parkCode"]),               
            //    ForecastDay = Convert.ToInt32(reader["fiveDayForecastValue"]),

            //    Low = Convert.ToInt32(reader["low"]),
            //    High = Convert.ToInt32(reader["high"]),                
            //    Forecast = Convert.ToString(reader["forecast"]),
            //};
        }
    }
}
