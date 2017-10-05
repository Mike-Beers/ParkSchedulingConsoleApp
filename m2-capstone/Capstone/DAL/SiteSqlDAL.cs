using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class SiteSqlDAL
    {
        private string connectionString;

        public SiteSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Site> GetAllSites()
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM site", conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        sites.Add(GetSiteFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return sites;
        }

        public List<Site> GetTop5Sites()
        {
            List<Site> top5Sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT TOP 5 * FROM site ORDER BY max_occupancy DESC;", conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        top5Sites.Add(GetSiteFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return top5Sites;
        }

        private Site GetSiteFromRow(SqlDataReader results)
        {
            Site newSite = new Site();

            newSite.Site_id = Convert.ToInt32(results["site_id"]);
            newSite.Camground_id = Convert.ToInt32(results["campground_id"]);
            newSite.Site_number = Convert.ToInt32(results["site_number"]);
            newSite.Max_occupancy = Convert.ToInt32(results["max_occupancy"]);
            newSite.Accessible = Convert.ToBoolean(results["accessible"]);
            newSite.Max_rv_length = Convert.ToInt32(results["max_rv_length"]);
            newSite.Utilities = Convert.ToBoolean(results["utilities"]);

            return newSite;
        }
    }
}
