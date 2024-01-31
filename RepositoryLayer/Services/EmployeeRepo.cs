using Microsoft.Data.SqlClient;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RepositoryLayer.Services
{
    public class EmployeeRepo : IEmployeeRepo
    {
        string connectionString = @"Server=DESKTOP-BD26R3U\SQLEXPRESS;Database=EmployeePayRollDB;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true";
        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            using(SqlConnection  connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand("InsertEmployee", connection);
                command.CommandType = CommandType.StoredProcedure;

                command.Parameters.AddWithValue("@FullName", employee.FullName);
                command.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                command.Parameters.AddWithValue("@Gender", employee.Gender);
                command.Parameters.AddWithValue("@Department", employee.Department);
                command.Parameters.AddWithValue("@Salary", employee.Salary);
                command.Parameters.AddWithValue("@StartDate", employee.StartDate);
                command.Parameters.AddWithValue("@Notes", employee.Notes);

                command.ExecuteNonQuery();
                connection.Close();
            }
            return employee;
        }

        public IEnumerable<EmployeeEntity> GetAllEmployees() 
        {
            List<EmployeeEntity> empList=new List<EmployeeEntity>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand rcmd = new SqlCommand("GetAllEmployees", connection);
                rcmd.CommandType= CommandType.StoredProcedure;
                SqlDataReader rdata = rcmd.ExecuteReader();
                while (rdata.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity()
                    {
                        EmployeeId = Convert.ToInt32(rdata["EmployeeId"]),
                        FullName = rdata["FullName"].ToString(),
                        ImagePath = rdata["ImagePath"].ToString(),
                        Gender = rdata["Gender"].ToString(),
                        Department = rdata["Department"].ToString(),
                        Salary = Convert.ToDecimal(rdata["salary"]),
                        StartDate = Convert.ToDateTime(rdata["StartDate"]),
                        Notes = rdata["Notes"].ToString()
                    };
                    empList.Add(employee);
                }
                rdata.Close();
                connection.Close();
                return empList;
            }

        }

        public EmployeeEntity GetEmpById(int empId)
        {
            EmployeeEntity employee = new EmployeeEntity();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand rcmd = new SqlCommand("GetEmpById", connection);
                rcmd.CommandType = CommandType.StoredProcedure;

                // string sqlQuery = "SELECT * FROM EmployeePayRoll WHERE EmployeeId= " + empId;
                //SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                rcmd.Parameters.AddWithValue("@EmployeeId", empId);
             
                SqlDataReader rdr = rcmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                }             
            }
            return employee;
        }

        public EmployeeEntity DeleteEmpById(int employeeId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("DeleteEmp", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
                var result = GetEmpById(employeeId);
                cmd.ExecuteNonQuery();
                connection.Close();
                return result;

            }
            return null;
        }


        public EmployeeEntity UpdateEmpById(EmployeeEntity employee)
        {
            using(SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand("UpdateEmployee", connection);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                cmd.Parameters.AddWithValue("@FullName", employee.FullName);
                cmd.Parameters.AddWithValue("@ImagePath", employee.ImagePath);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                cmd.Parameters.AddWithValue("@StartDate", employee.StartDate);
                //DateTime startDate = (employee.StartDate >= SqlDateTime.MinValue.Value && employee.StartDate <= SqlDateTime.MaxValue.Value)
                //              ? employee.StartDate
                //              : SqlDateTime.MinValue.Value;
               // cmd.Parameters.AddWithValue("@StartDate", startDate);

                cmd.Parameters.AddWithValue("@Notes", employee.Notes);
                cmd.ExecuteNonQuery();
                connection.Close();
                return employee ;

            }
        }

        public IEnumerable<EmployeeEntity> GetEmpByName(string name)
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();

            EmployeeEntity employee = new EmployeeEntity();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand rcmd = new SqlCommand("GetEmpByName", connection);
                rcmd.CommandType = CommandType.StoredProcedure;

                // string sqlQuery = "SELECT * FROM EmployeePayRoll WHERE EmployeeId= " + empId;
                //SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                rcmd.Parameters.AddWithValue("@Name", name);

                SqlDataReader rdr = rcmd.ExecuteReader();

                while (rdr.Read())
                {
                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                    lstemployee.Add(employee);

                }
            }
            return lstemployee;
        }

        public IEnumerable<EmployeeEntity> GetEmpByNameOrDepartment(string search)
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand rcmd = new SqlCommand("GetEmpByNameOrDept", connection);
                rcmd.CommandType = CommandType.StoredProcedure;

                // string sqlQuery = "SELECT * FROM EmployeePayRoll WHERE EmployeeId= " + empId;
                //SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                rcmd.Parameters.AddWithValue("@searchString", search);

                SqlDataReader rdr = rcmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity();

                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                    lstemployee.Add(employee);

                }
            }
            return lstemployee;
        }

        public IEnumerable<EmployeeEntity> GetEmpBtwDateRange(DateRangeModel model)
        {
            List<EmployeeEntity> lstemployee = new List<EmployeeEntity>();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand rcmd = new SqlCommand("GetDateBwtRange", connection);
                rcmd.CommandType = CommandType.StoredProcedure;

                // string sqlQuery = "SELECT * FROM EmployeePayRoll WHERE EmployeeId= " + empId;
                //SqlCommand cmd = new SqlCommand(sqlQuery, connection);


                rcmd.Parameters.AddWithValue("@StartDate", model.StartDate);
                rcmd.Parameters.AddWithValue("@EndDate", model.EndDate);

                SqlDataReader rdr = rcmd.ExecuteReader();

                while (rdr.Read())
                {
                    EmployeeEntity employee = new EmployeeEntity();

                    employee.EmployeeId = Convert.ToInt32(rdr["EmployeeId"]);
                    employee.FullName = rdr["FullName"].ToString();
                    employee.ImagePath = rdr["ImagePath"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.Salary = Convert.ToDecimal(rdr["Salary"]);
                    employee.StartDate = Convert.ToDateTime(rdr["StartDate"]);
                    employee.Notes = rdr["Notes"].ToString();
                    lstemployee.Add(employee);

                }
            }
            return lstemployee;
        }
    }
}

