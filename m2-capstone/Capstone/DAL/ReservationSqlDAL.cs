using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone.DAL;

namespace Capstone.DAL
{
    public class ReservationSqlDAL
    {
        private string connectionString;

        public ReservationSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        public int CreateReservation(int site_id, string name, DateTime from_date, DateTime to_date)
        {
            int reservationId = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(@"INSERT INTO reservation VALUES (@site_id, @name, @from_date, @to_date, GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS INT);", conn);
                    command.Parameters.AddWithValue("@site_id", site_id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@from_date", from_date.ToString());
                    command.Parameters.AddWithValue("@to_date", to_date.ToString());

                   reservationId = (int)command.ExecuteScalar();

                }
            }
            catch(SqlException)
            {
                throw;
            }
            return reservationId;
        }
        private List<Reservation> GetAllReservations()
        {
            List<Reservation> allReservations = new List<Reservation>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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
            catch (SqlException)
            {
                throw;
            }
            return allReservations;
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
