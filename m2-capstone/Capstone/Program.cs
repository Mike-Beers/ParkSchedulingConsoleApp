using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample Code to get a connection string from the
            // App.Config file
            // Use this so that you don't need to copy your connection string all over your code!
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

            CampgroundSqlDAL campground = new CampgroundSqlDAL(connectionString);
            List<Campground> newList = campground.GetAllCampgrounds();
            foreach (Campground value in newList)
            {
                Console.WriteLine(value.ToString());
            }


            SiteSqlDAL site = new SiteSqlDAL(connectionString);
            List<Site> newSite = site.GetAllSites();
            foreach (Site value in newSite)
            {
                Console.WriteLine(value.ToString());
            }

            SiteSqlDAL thisSite = new SiteSqlDAL(connectionString);
            List<Site> top5 = thisSite.GetTop5Sites();
            foreach (Site value in top5)
            {
                Console.WriteLine(value.ToString());
            }
        }
    }
}
