using EmployeePayRoll.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeePayRoll.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
        //    var exception=HttpContext.Features.Get<IExceptionHandlerFeature>();
        //    _logger.LogError($"End Point : {exception.Endpoint} \n Error :{exception.Error}");

            _logger.LogError("Error occured using logError !!!!!!!........");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            //_logger.LogError("Error occured using logError  in error !!!!!!!........");
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
