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

        public void RunCLI()
        {
            string input = "";
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parksList = dal.GetAllParks();
            Park selectedPark = null;

            while (input != "Q")
            {
                GetParks();                             //prints out the list of all possible parks to choose from
                input = Console.ReadLine();
                selectedPark = DisplayPark(input); //displays all information about a single park
                ParkInfoMenu();                     // 3 options: view campgrounds, search reservation, or previous screen
                input = Console.ReadLine();
                if (input == "1")
                {
                    ViewCampgrounds(selectedPark);  //all campgrounds of a selected park
                    CamprgroundMenu();              // 2 options: search for reservation, previous screen
                    string input2 = Console.ReadLine();
                    if (input2 == "1")
                    {
                        SearchForReservationMenu(selectedPark);
                    }
                    if (input2 == "2")
                    {
                        ViewCampgrounds(selectedPark);  // back to all campgrounds of a selected park
                    }
                }
                if (input == "2")
                {
                    SearchForReservationMenu(selectedPark);
                }
                if (input == "3")
                {
                    // GetParks();
                }
            }


        }
        private void ParkInfoMenu()
        {
            Console.WriteLine("Select A Command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search For Reservation");
            Console.WriteLine("3) Return to Previous Screen");
        }
        private void CamprgroundMenu()
        {
            Console.WriteLine("Select A Command");
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return To Previous Screen");
        }

        private string[] ReservationInput()
        {
            string[] result = new string[3];
            Console.Write("Which campground? (enter 0 to cancel)");
            result[0] = Console.ReadLine();
            Console.Write("What is the arrival date? (MM/DD/YYYY)");
            result[1] = Console.ReadLine();
            Console.Write("What is the departure date? (MM/DD/YYYY)");
            result[2] = Console.ReadLine();

            return result;
        }

        private void SearchForReservationMenu(Park selectedPark)
        {
            ViewCampgrounds(selectedPark);
            string[] reservationInputs = ReservationInput();
            int campground_id = Convert.ToInt32(reservationInputs[0]);
            DateTime from_date = Convert.ToDateTime(reservationInputs[1]);
            DateTime to_date = Convert.ToDateTime(reservationInputs[2]);
            if (campground_id == 0)
            {
                ViewCampgrounds(selectedPark);
            }
        }

        private void GetParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parks = dal.GetAllParks();
            int menuCounter = 1;
            Console.WriteLine("Select a Park Name for Further Details: ");
            foreach (Park park in parks)
            {
                Console.WriteLine(menuCounter++ + ")" + park.Park_name);
            }
            Console.WriteLine("Q) Quit");

        }
        private Park DisplayPark(string input)
        {
            Park selectedPark = null;
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parkInfo = dal.GetAllParks();
            int selection = Convert.ToInt32(input);

            selectedPark = parkInfo[selection -1];
            Console.WriteLine(selectedPark.ToString());
            //foreach (Park park in parkInfo)
            //{
            //    if //(input == park.Park_name)  ||
            //    (selection == parkInfo[selection -1])
            //    {
            //        selectedPark = park;
            //    }
            //}

            return selectedPark;
        }

        private void ViewCampgrounds(Park selectedPark)
        {
            CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(connectionString);
            List<Campground> campgrounds = campgroundDAL.GetCampgrounds(selectedPark);

            foreach (Campground campground in campgrounds)
            {
                Console.WriteLine(campground.ToString());
            }
        }

    }
}
