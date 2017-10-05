using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL
    {
        private string connectionString;

        public CampgroundSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Campground> GetAllCampgrounds()
        {
            List<Campground> campgrounds = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM campground", conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        campgrounds.Add(GetCampgroundFromRow(results));
                    }
                }

            }
            catch (SqlException)
            {
                throw;
            }

            return campgrounds;
        }

        private Campground GetCampgroundFromRow(SqlDataReader results)
        {
            Campground newCampground = new Campground();

            newCampground.Campground_id = Convert.ToInt32(results["campground_id"]);
            newCampground.Park_id = Convert.ToInt32(results["park_id"]);
            newCampground.Campground_name = Convert.ToString(results["name"]);
            newCampground.Open_from_month = Convert.ToInt32(results["open_from_mm"]);
            newCampground.Open_to_month = Convert.ToInt32(results["open_to_mm"]);
            newCampground.Daily_fee = Convert.ToInt32(results["daily_fee"]);

            return newCampground;
        }
    }
}
