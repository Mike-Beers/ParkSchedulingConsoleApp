using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Configuration;

namespace Capstone
{
    public class ParkSystemCLI
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        int menuCounter = 0;
                    CampgroundSqlDAL campgroundsdal = new CampgroundSqlDAL(connectionString);




        public void RunCLI()
        {
            GetParks(); //prints out the list of all possible parks to choose from
            string input = Console.ReadLine();

            while (input != "Q")
            {
                foreach (Park park in parks)
                {
                    if ()
                    {
                       
                    campgroundsdal.GetCampgrounds(park)
                    }
                }

            }


        }
        private void ParkInfo()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parkInfo = dal.GetAllParks();


        }

        private void GetParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parks = dal.GetAllParks();

            Console.WriteLine("Select a Park Name for Further Details: ");
            foreach (Park park in parks)
            {
                Console.WriteLine(menuCounter++ + ")" + park.Park_name);
            }
            Console.WriteLine("Q) Quit");

        }
        private void SelectPark(string input)
        {

        }

        private void OpeningMenu()
        {

        }

    }
}
