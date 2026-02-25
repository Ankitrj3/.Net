using Microsoft.AspNetCore.Mvc;
using MVCModelAndView.Models;
using System.Diagnostics;

namespace MVCModelAndView.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("../StudentView/View");
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
