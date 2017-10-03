using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectDB.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using ProjectDB.Models;

namespace UnitTestProject1
{
    [TestClass()]
    public class EmployeeSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=projects;Integrated Security=True";
        private int employees;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command;

                command = new SqlCommand("SELECT COUNT(*) FROM employee", conn);
                employees = (int)command.ExecuteScalar();
                command = new SqlCommand("INSERT INTO employee VALUES (1, 'Mike', 'Beers', 'Developer', '2017-10-03', 'M', '2017-10-03')", conn);
                command.ExecuteNonQuery();
            }
        }
        [TestCleanup]
        public void Cleanup()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllEmployeesTest()
        {
            EmployeeSqlDAL newEmployee = new EmployeeSqlDAL(connectionString);
            List<Employee> employeesList = newEmployee.GetAllEmployees();

            employees++;

            Assert.IsNotNull(employeesList);
            Assert.AreEqual(employees, employeesList.Count);
        }

        [TestMethod]
        public void SearchTest()
        {
            EmployeeSqlDAL newEmployee = new EmployeeSqlDAL(connectionString);
            List<Employee> employeesList = newEmployee.Search("Mike", "Beers");

            Assert.AreEqual(employeesList[0].FirstName, "Mike");
            Assert.AreEqual(employeesList[0].LastName, "Beers");
        }
        [TestMethod]
        public void GetEmployeesWithoutProjectsTest()
        {
            EmployeeSqlDAL newEmployee = new EmployeeSqlDAL(connectionString);
            List<Employee> employeesList = newEmployee.GetEmployeesWithoutProjects();

            Assert.IsNotNull(employeesList);
        }

    }
}
