using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;
using ModelLayer.Models;
using BusinessLayer.Interfaces;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Entity;
namespace EmployeePayRoll.Controllers
{
    public class EmployeeController : Controller
    {
        public readonly IEmployeeBusiness _employeeBusiness;

        //this is for inbuilt logger
        //private readonly ILogger<EmployeeController> logger;

        //public EmployeeController(IEmployeeBusiness employeeBusiness,ILogger<EmployeeController> logger)
        //{
        //    _employeeBusiness = employeeBusiness;
        //    this.logger = logger;
        //}


        ////for custom logger
        //private readonly ILogger logger;
        //public EmployeeController(IEmployeeBusiness employeeBusiness, ILoggerFactory factory)
        //{
        //    _employeeBusiness = employeeBusiness;
        //    this.logger = factory.CreateLogger("custom category");
        //}


        //This written without mentioning the type EmployeeController in Ilogger
        //the code written in program.cs class , which will applied to whole application
        private readonly ILogger logger;

        public EmployeeController(IEmployeeBusiness employeeBusiness, ILogger logger)
        {
            _employeeBusiness = employeeBusiness;
            this.logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Add")]
        public IActionResult AddEmployeeDetails()
        {
            //logger.LogInformation(101,"This is logger inside the AddEmployeeDetails action method");

            //logger.LogInformation("this is message for custome logger");

            logger.LogInformation(101, "this logger is for whole application");
            return View();
        }
        [HttpPost("Add")]
        public IActionResult AddEmployeeDetails(EmployeeModel employee)
        {
            if (ModelState.IsValid)
            {
                _employeeBusiness.AddEmployee(employee);
            }
            return View();
        }

        [HttpGet("Get")]
        public IActionResult GetAllEmp()
        {
            List<EmployeeEntity> emplist = _employeeBusiness.GetAllEmployees().ToList();
            return View(emplist);
        }


        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                //id=HttpContext.Session.GetInt32("EmployeeId");
                if (id == null)
                {
                    return NotFound();
                }
                EmployeeEntity employee = _employeeBusiness.GetEmpById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing your request.");
                return View();
            }
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }
                EmployeeEntity employee = _employeeBusiness.GetEmpById(id);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception ex)
            {

                return RedirectToAction("GetAllEmployee");
            }

        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _employeeBusiness.DeleteEmpById(id);
                return RedirectToAction("GetAllEmp");
            }
            catch (Exception ex)
            {

                return RedirectToAction("GetAllEmp");
            }
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            EmployeeEntity employee = _employeeBusiness.GetEmpById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }



        [HttpPost]
        [Route("Edit/{id}")]
        public IActionResult Edit(int id,EmployeeEntity employee)
        {
            if(id != employee.EmployeeId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var result = _employeeBusiness.UpdateEmpById(employee);
                return RedirectToAction("GetAllEmp");
            }
            return View();
        }



        [HttpGet]
        [Route("Name/{Name}")]
        public IActionResult GetAllEMpByName(String Name)
        {
            List<EmployeeEntity> res = _employeeBusiness.GetEmpByName(Name).ToList();

            return View(res);
        }

        [HttpGet]
        [Route("Search/{search}")]
        public IActionResult GetAllEMpByNameOrDept(String search)
        {
            var res = _employeeBusiness.GetEmpByNameOrDepartment(search).ToList();

            return View(res);
        }

        [HttpGet("DateRange")]
        public IActionResult GetEmpBtwDateRange()
        {
            return View();
        }

        [HttpPost("DateRange")]
        public IActionResult GetEmpBtwDateRange(DateRangeModel model)
        {
            if(model != null)
            {
                var res = _employeeBusiness.GetEmpBtwDateRange(model).ToList();
                return View("GetAllEmp",res);
            }

            return View();
        }

    }
}
