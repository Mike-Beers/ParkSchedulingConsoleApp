using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.DAL
{
    public class DepartmentSqlDAL
    {
        private const string getDepartmentsSql = "select * from department";
        private const string createDepartmentSQL = @"INSERT INTO department (name) VALUES (@name)";
        private const string updateDepartmentSQL = @"UPDATE department SET name = @name WHERE department_id = @id";
        private string connectionString;

        // Single Parameter Constructor
        public DepartmentSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Department> GetDepartments()
        {
            List<Department> departments = new List<Department>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(getDepartmentsSql, conn);
                    SqlDataReader results = command.ExecuteReader();

                    while (results.Read()) // while we have a result, do something with it
                    {
                        departments.Add(CreateDepartmentFromRow(results));
                    }
                }
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw;
            }

            return departments;
        }

        public bool CreateDepartment(Department newDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(createDepartmentSQL, conn);
                    //command.Parameters.AddWithValue("@department_id", newDepartment.Id);
                    command.Parameters.AddWithValue("@name", newDepartment.Name);

                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        public bool UpdateDepartment(Department updatedDepartment)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand(updateDepartmentSQL, conn);
                    command.Parameters.AddWithValue("@name", updatedDepartment.Name);
                    command.Parameters.AddWithValue("@id", updatedDepartment.Id);

                    int rowsAffected = command.ExecuteNonQuery();

                    return (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                throw;
            }
        }

        private Department CreateDepartmentFromRow(SqlDataReader results)
        {
            Department dept = new Department();
            dept.Id = Convert.ToInt32(results["department_id"]);
            dept.Name = Convert.ToString(results["name"]);
            return dept;
        }
    }
}
