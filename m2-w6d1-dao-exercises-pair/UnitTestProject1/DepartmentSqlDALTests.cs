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

// using System.Data.SqlClient;

namespace ProjectDBTests
{
    [TestClass()]
    public class DepartmentSqlDALTests
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=projects;Integrated Security=True";
        private int id;
        private int entries;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                conn.Open();

                cmd = new SqlCommand("SELECT COUNT(*) From department", conn);
                entries = (int)cmd.ExecuteScalar();
                cmd = new SqlCommand("INSERT INTO department VALUES ('Manufacturing'); SELECT CAST(SCOPE_IDENTITY() AS int)", conn);
                id = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetDepartmentsTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);

            List<Department> departments = departmentDAL.GetDepartments();

            entries++;

            Assert.IsNotNull(departments);
            Assert.AreEqual(entries, departments.Count);
        }

        [TestMethod]
        public void CreateDepartmentTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);

            Department departmentTestObj = new Department();
            departmentTestObj.Name = "AnyName";
            bool createdDepartment = departmentDAL.CreateDepartment(departmentTestObj);

            Assert.AreEqual(true, createdDepartment);
        }

        [TestMethod]
        public void UpdateDepartmentTest()
        {
            DepartmentSqlDAL departmentDAL = new DepartmentSqlDAL(connectionString);
            Department departmentTestObj = new Department
            {
                Id = id,
                Name = "Shoe",
            };

            bool didWork = departmentDAL.UpdateDepartment(departmentTestObj);

            Assert.AreEqual(true, didWork);
        }
    }
}
