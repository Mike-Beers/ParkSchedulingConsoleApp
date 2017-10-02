using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectDB.DAL
{
    public class ProjectSqlDAL
    {
        private string getAllProjectsSQL = @"SELECT * FROM project";
        private string assignEmployeeToProjectSQL = @"INSERT INTO project_employee VALUES(@project_id, @employee_id)";
        private string removeEmployeeSQL = @"DELETE FROM project_employee WHERE employee_id = @employee_id AND project_id = @project_id";
        private string createProjectSQL = @"INSERT INTO project VALUES (@name, @from_date, @to_date)";

        private string connectionString;

        // Single Parameter Constructor
        public ProjectSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Project> GetAllProjects()
        {
            List<Project> projects = new List<Project>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(getAllProjectsSQL, conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        projects.Add(GetProjectsFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return projects;
        }

        public bool AssignEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(assignEmployeeToProjectSQL, conn);
                    command.Parameters.AddWithValue("@employee_id", employeeId);
                    command.Parameters.AddWithValue("@project_id", projectId);

                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(removeEmployeeSQL, conn);
                    command.Parameters.AddWithValue("@employee_id", employeeId);
                    command.Parameters.AddWithValue("@project_id", projectId);

                    int rowsAffected = command.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }

        public bool CreateProject(Project newProject)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(createProjectSQL, conn);
                    command.Parameters.AddWithValue("@name", newProject.Name);
                    command.Parameters.AddWithValue("@from_date", newProject.StartDate);
                    command.Parameters.AddWithValue("@to_date", newProject.EndDate);

                    int rowsAffected = command.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (SqlException)
            {
                throw;
            }
        }
        private Project GetProjectsFromRow(SqlDataReader results)
        {
            Project newProject = new Project();

            newProject.ProjectId = Convert.ToInt32(results["project_id"]);
            newProject.Name = Convert.ToString(results["name"]);
            newProject.StartDate = Convert.ToDateTime(results["from_date"]);
            newProject.EndDate = Convert.ToDateTime(results["to_date"]);

            return newProject;
        }
    }
}

