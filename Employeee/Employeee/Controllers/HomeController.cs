using Employeee.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Employeee.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View("../Employee/AddEmployee");
        }
        public IActionResult ForeachView()
        {
            ViewBag.Message = "Welcome to Employee Management System,India,Russia,Japan";
            ViewData["Message"] = "Australia,Canada,UK,USA";
            ViewBag.NumberOfLines = 10;
            return View();
        }
        //public IActionResult AddEmployee()
        //{
        //    return View("../Employee/AddEmployee");
        //}
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
