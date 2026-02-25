using Microsoft.AspNetCore.Mvc;
using SeriLog.Models;
using System.Diagnostics;

namespace SeriLog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            _logger.LogInformation("Home Page is Opened");
            return View();
        }
        public IActionResult About()
        {
            _logger.LogInformation("About Page is Opened");
            return View();
        }
        public IActionResult ErrorTest()
        {
            try
            {
                int a = 5;
                int b = 1;
                return Content($"Result: {a / b}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in ErrorTest action.");
                return View();
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
