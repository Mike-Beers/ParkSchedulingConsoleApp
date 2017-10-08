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
        Reservation reservation = new Reservation();

        public void RunCLI()
        {
            string input = "";
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parksList = dal.GetAllParks();
            Park selectedPark = null;

            while (true)
            {

                GetParks();                             //prints out the list of all possible parks to choose from
                input = Console.ReadLine();
                Console.Clear();
                if (input.ToUpper() == "Q")              //if User selects to quit, program ends
                {
                    return;
                }
                selectedPark = DisplayPark(input);

                if (selectedPark == null) { }     //displays all information about a single park IF THE SELECTED PARK IS NULL WE NEED TO START THE LOOP OVER AGAIN
                else if (selectedPark != null)
                {

                    ParkInfoMenu();                     // 3 options: view campgrounds, search reservation, or previous screen
                    input = Console.ReadLine();
                    Console.Clear();
                }

                if (input == "1")
                {
                    ViewCampgrounds(selectedPark);  //all campgrounds of a selected park
                    CamprgroundMenu();              // 2 options: search for reservation, previous screen
                    string input2 = Console.ReadLine();
                    Console.Clear();

                    if (input2 == "1")
                    {
                        SearchForReservationMenu(selectedPark);
                        MakeReservationMenu();
                    }
                    else if (input2 == "2")
                    {
                        //back to main menu
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid menu option.");
                    }
                }
                else if (input == "2")
                {
                    SearchForReservationMenu(selectedPark);
                    MakeReservationMenu();
                }
                else if (input == "3") { }
                else
                {
                    Console.WriteLine("please select a valid menu option.");
                    Console.WriteLine();
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
        private void SearchForReservationMenu(Park selectedPark)
        {

            Console.Clear(); //new
            ViewCampgrounds(selectedPark);
            SiteSqlDAL siteSqlDAL = new SiteSqlDAL(connectionString);


            string[] reservationInputs = ReservationInput();


            int campground_id = Convert.ToInt32(reservationInputs[0]);


            DateTime from_date = Convert.ToDateTime(reservationInputs[1]);
            reservation.Reservation_from_date = from_date;

            DateTime to_date = Convert.ToDateTime(reservationInputs[2]);
            reservation.Reservation_to_date = to_date;

            //if (campground_id == 0)
            //{
            //    ViewCampgrounds(selectedPark);
            //}


            List<Site> availableSites = siteSqlDAL.GetAvailableSites(campground_id, from_date, to_date);
            foreach (Site site in availableSites)
            {
                Console.WriteLine(site.ToString() + siteSqlDAL.GetCost(campground_id, from_date, to_date));

            }

        }        //NEED TO MAKE 0 START OUR RUNNING LOOP OVER
        private void MakeReservationMenu()
        {

            Console.WriteLine("What site should be reserved? (enter 0 to cancel)");
            reservation.Site_id = Convert.ToInt32(Console.ReadLine());
            if (reservation.Site_id == 0)
            {
                return;
            }
            else
            {
                Console.WriteLine("What name should the reservation be made under?");
                reservation.Reservation_name = Console.ReadLine();
                Console.WriteLine($"The reservation has been made and the confirmation id is: {BookReservation(reservation)}");
            }

        }
        private string[] ReservationInput()
        {

            string[] result = new string[3];
            Console.Write("Which campground? (enter 0 to cancel)");
            result[0] = Console.ReadLine();
            if (result[0] == "0")
            {
                return result;
            }
            else
            {

                Console.Write("What is the arrival date? (MM/DD/YYYY)");
                result[1] = Console.ReadLine();

                Console.Write("What is the departure date? (MM/DD/YYYY)");
                result[2] = Console.ReadLine();
            }

            return result;
        }
        private void GetParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parks = dal.GetAllParks();
            int menuCounter = 1;
            Console.WriteLine("Select a Park for Further Details: ");
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
            bool parsed = int.TryParse(input, out int selection);
            if (parsed)
            {
                if (selection <= parkInfo.Count())
                {
                    selectedPark = parkInfo[selection - 1];
                    Console.WriteLine(selectedPark.ToString());
                    return selectedPark;
                }
                else
                {
                    Console.Write("The park number you have selected is invalid, ");
                }
            }
            else
            {
                Console.Write("Please enter a park number or ");
            }
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
        private int BookReservation(Reservation reservation)
        {
            ReservationSqlDAL reservationDal = new ReservationSqlDAL(connectionString);
            return reservationDal.CreateReservation(reservation);
        }
    }
}
