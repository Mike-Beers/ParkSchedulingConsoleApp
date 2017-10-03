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
    public class ProjectSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=projects;Integrated Security=True";
        private int projects;
        private int projectId;
        private int employeeId;
        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command;

                command = new SqlCommand("SELECT COUNT(*) FROM project", conn);
                projects = (int)command.ExecuteScalar();
                command = new SqlCommand("INSERT INTO project VALUES ('Newer Project', '2017-10-12', '2017-10-13'); SELECT CAST (SCOPE_IDENTITY() AS int)", conn);
                projectId = (int)command.ExecuteScalar();
                command = new SqlCommand("INSERT INTO employee VALUES (1, 'Mike', 'Beers', 'Developer', '2017-10-03', 'M', '2017-10-03'); SELECT CAST (SCOPE_IDENTITY() AS int)", conn);
                employeeId = (int)command.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }
        [TestMethod]
        public void GetAllProjectsTest()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);
            List<Project> projectList = newProject.GetAllProjects();

            projects++;

            Assert.IsNotNull(projectList);
            Assert.AreEqual(projects, projectList.Count);
        }
        [TestMethod]
        public void AssignEmployeeToProjectTest()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);
            bool worked = newProject.AssignEmployeeToProject(projectId, employeeId);
        }
        [TestMethod]
        public void RemoveEmployeeFromProjectTest()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);
            bool worked = newProject.RemoveEmployeeFromProject(projectId, employeeId);
        }
        [TestMethod]
        public void CreateProjectTest()
        {
            ProjectSqlDAL newProject = new ProjectSqlDAL(connectionString);
            Project projectTestObj = new Project
            {
                Name = "newProjectName",
                StartDate = new DateTime(1955,01,01,12,00,00 ),
                EndDate = new DateTime(2010,07,07,12,00,00)
            };

            bool worked = newProject.CreateProject(projectTestObj);
        }
    }
}
