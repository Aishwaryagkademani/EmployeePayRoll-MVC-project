using BusinessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePayRoll.Controllers
{
    public class AjaxController1 : Controller
    {
        private readonly IEmployeeBusiness employeeBusiness;
        public AjaxController1(IEmployeeBusiness employeeBusiness)
        {
            this.employeeBusiness = employeeBusiness;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetAjax")]
        public JsonResult GetAllEmployeeByAjax()
        {
            var data = employeeBusiness.GetAllEmployees().ToList();
            return new JsonResult(data);
        }
    }
}

