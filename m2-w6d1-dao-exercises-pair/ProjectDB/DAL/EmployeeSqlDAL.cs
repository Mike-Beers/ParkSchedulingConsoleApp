using ProjectDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ProjectDB.DAL
{
    public class EmployeeSqlDAL
    {
        private string getAllEmployeesSQL = @"SELECT * FROM employee";
        private string searchSQL = @"SELECT * FROM employee WHERE first_name = @first_name AND last_name = @last_name";
        private string getEmployeesWithoutProjectsSQL = @"SELECT * FROM employee JOIN project_employee ON project_employee.employee_id = employee.employee_id WHERE project_employee.project_id IS NULL";
        private string connectionString;

        // Single Parameter Constructor
        public EmployeeSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(getAllEmployeesSQL, conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        employees.Add(GetEmployeeFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return employees;
        }

        public List<Employee> Search(string firstname, string lastname)
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(searchSQL, conn);

                    command.Parameters.AddWithValue("@first_name", firstname);
                    command.Parameters.AddWithValue("@last_name", lastname);

                    SqlDataReader results = command.ExecuteReader();

                    while (results.Read())
                    {
                        employees.Add(GetEmployeeFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return employees;
        }

        public List<Employee> GetEmployeesWithoutProjects()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(getEmployeesWithoutProjectsSQL, conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        employees.Add(GetEmployeeFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return employees;
        }

        private Employee GetEmployeeFromRow(SqlDataReader results)
        {
            Employee newEmployee = new Employee();

            newEmployee.EmployeeId = Convert.ToInt32(results["employee_id"]);
            newEmployee.DepartmentId = Convert.ToInt32(results["department_id"]);
            newEmployee.FirstName = Convert.ToString(results["first_name"]);
            newEmployee.LastName = Convert.ToString(results["last_name"]);
            newEmployee.JobTitle = Convert.ToString(results["job_title"]);
            newEmployee.LastName = Convert.ToString(results["last_name"]);
            newEmployee.BirthDate = Convert.ToDateTime(results["birth_date"]);
            newEmployee.Gender = Convert.ToString(results["gender"]);
            newEmployee.HireDate = Convert.ToDateTime(results["hire_date"]);

            return newEmployee;
        }
    }
}
