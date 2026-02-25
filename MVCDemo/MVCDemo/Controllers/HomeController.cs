using Microsoft.AspNetCore.Mvc;
using MVCDemo.Models;
using System.Diagnostics;

namespace MVCDemo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()    
        {
            return View();
        }
        public IActionResult GetProduct()
        {
            int value = 10*10;
            return Content(value.ToString());
        }
        public IActionResult GetPF()
        {
            int? a = 12;
            int? b = 0;
            int? c = a.Value/b.Value;
            return Content(c.ToString());
        }
        public IActionResult Privacy()
        {
            return View();
        }
        //public IActionResult Error()
        //{
        //    return View();
        //}
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
