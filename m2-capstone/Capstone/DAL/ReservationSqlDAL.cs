using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAL
{
    class ReservationSqlDAL
    {
        private string connectionString;

        public ReservationSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        private List<Reservation> GetAllReservations()
        {
            List<Reservation> allReservations = new List<Reservation>();
            try
            {
                using(SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM reservation;", conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        allReservations.Add(CreateFromRow(results));
                    }
                }
            }
            catch(SqlException)
            {
                throw;
            }
            return allReservations;
        }
        public List<Reservation> GetAvailableReservations()
        {
            List<Reservation> availableReservations = new List<Reservation>();
            List<Reservation> bookedReservations = new List<Reservation>();
            bookedReservations = GetAllReservations();
            

            return availableReservations;
        }
        private Reservation CreateFromRow(SqlDataReader results)
        {
            Reservation reservation = new Reservation();
            reservation.Reservation_id = Convert.ToInt32(results["reservation_id"]);
            reservation.Site_id = Convert.ToInt32(results["site_id"]);
            reservation.Reservation_name = Convert.ToString(results["name"]);
            reservation.Reservation_from_date = Convert.ToDateTime(results["from_date"]);
            reservation.Reservation_to_date = Convert.ToDateTime(results["to_date"]);
            reservation.Reservation_create_date = Convert.ToDateTime(results["create_date"]);
            return reservation;
        }

    }
}
