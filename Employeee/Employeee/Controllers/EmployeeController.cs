using Microsoft.AspNetCore.Mvc;
using Employeee.Models;
using Microsoft.Extensions.Logging;
namespace Employeee.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }
        // STATIC COLLECTION 
        private static List<string> departments = new List<string>
        {
            "HR","IT","Finance","Admin"
        };
        private static List<Employee> empList = new List<Employee>();

        // READ AL
        public IActionResult Index()
        {
            return View(empList);
        }

        // CREATE - GET
        public IActionResult AddEmployee()
        {
            ViewBag.Departments = departments;
            return View();
        }

        // CREATE - POST
        [HttpPost]
        public IActionResult AddEmployee(Employee emp)
        {
            var exists = empList.Any(e => e.Id == emp.Id);
            _logger.LogInformation("Adding Employee with Id {Id}", emp.Id);


            if (exists)
            {
                _logger.LogInformation("{Id} already exists so it cannot be added! ", emp.Id);
                ModelState.AddModelError("", "Employee ID already exists!");
                return View(emp);
            }
            if (!departments.Contains(emp.Department))
            {
                ModelState.AddModelError("Department", "Invalid Department Selected");
                ViewBag.Departments = departments;
                return View(emp);
            }
            empList.Add(emp);
            _logger.LogInformation("Employee Added Successfully");
            return RedirectToAction("Index");
        }

        // EDIT - GET
        public IActionResult Edit(int id)
        {

            var emp = empList.FirstOrDefault(e => e.Id == id);
            ViewBag.Departments = departments;
            return View(emp);
        }

        // EDIT - POST
        [HttpPost]
        public IActionResult Edit(Employee emp)
        {
            _logger.LogInformation("Editing Employee Id {Id}", emp.Id);
            if (!departments.Contains(emp.Department))
            {
                ModelState.AddModelError("Department", "Invalid Department");
                ViewBag.Departments = departments;
                return View(emp);
            }

            var oldEmp = empList.FirstOrDefault(e => e.Id == emp.Id);

            if (oldEmp != null)
            {
                oldEmp.Name = emp.Name;
                oldEmp.Department = emp.Department;
                oldEmp.City = emp.City;
                _logger.LogInformation("Employee Updated");
            }

            return RedirectToAction("Index");
        }

        // DELETE
        public IActionResult Delete(int id)
        {
            _logger.LogWarning("Deleting Employee Id {Id}", id);
            var emp = empList.FirstOrDefault(e => e.Id == id);
            if (emp != null)
            {
                empList.Remove(emp);
                _logger.LogWarning("Employee Removed");

            }

            return RedirectToAction("Index");
        }
    }
}