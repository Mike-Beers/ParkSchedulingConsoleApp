using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using Capstone.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=campground;Integrated Security=True";
        private int reservationId;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command;
                conn.Open();

                command = new SqlCommand("INSERT INTO reservation VALUES (1, TestName, '08/28/1990', '09/14/1990', GETDATE(); SELECT CAST (SCOPE_IDENTITY() AS int)", conn);
                reservationId = (int)command.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void CreateReservationTest()
        {

            ReservationSqlDAL reservationDAL = new ReservationSqlDAL(connectionString);

            Reservation reservationTestObj = new Reservation();
            reservationTestObj {

            }
            bool createdDepartment = departmentDAL.CreateDepartment(departmentTestObj);

            Assert.AreEqual(true, createdDepartment);
        }
    }
}
