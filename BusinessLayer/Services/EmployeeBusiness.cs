using BusinessLayer.Interfaces;
using ModelLayer.Models;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class EmployeeBusiness : IEmployeeBusiness
    {
        private readonly IEmployeeRepo _employeeRepo;
        public EmployeeBusiness(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }

        public EmployeeModel AddEmployee(EmployeeModel employee)
        {
            return _employeeRepo.AddEmployee(employee);
        }

        public IEnumerable<EmployeeEntity> GetAllEmployees()
        {
            return _employeeRepo.GetAllEmployees();
        }
        public EmployeeEntity GetEmpById(int empId)
        {
            return _employeeRepo.GetEmpById(empId);
        }

        public EmployeeEntity DeleteEmpById(int employeeId)
        {
            return _employeeRepo.DeleteEmpById(employeeId);
        }

        public EmployeeEntity UpdateEmpById(EmployeeEntity employee)
        {
            return _employeeRepo.UpdateEmpById(employee);
        }

        public IEnumerable<EmployeeEntity> GetEmpByName(string name)
        {
            return _employeeRepo.GetEmpByName(name);
        }

        public IEnumerable<EmployeeEntity> GetEmpByNameOrDepartment(string search)
        {
            return _employeeRepo.GetEmpByNameOrDepartment(search);
        }

        public IEnumerable<EmployeeEntity> GetEmpBtwDateRange(DateRangeModel model)
        {
            return _employeeRepo.GetEmpBtwDateRange(model);
        }
    }
}
