using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IEmployeeBusiness
    {
        public EmployeeModel AddEmployee(EmployeeModel employee);
        public IEnumerable<EmployeeEntity> GetAllEmployees();
        public EmployeeEntity GetEmpById(int empId);
        public EmployeeEntity DeleteEmpById(int employeeId);
        public EmployeeEntity UpdateEmpById(EmployeeEntity employee);
        public IEnumerable<EmployeeEntity> GetEmpByName(string name);
        public IEnumerable<EmployeeEntity> GetEmpByNameOrDepartment(string search);
        public IEnumerable<EmployeeEntity> GetEmpBtwDateRange(DateRangeModel model);
    }
}
