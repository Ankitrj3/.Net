using CodeFirstApprochPractice.Models;
using CodeFirstApprochPractice.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodeFirstApprochPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<List<Employee>> GetAllEmployee()
        {
            return await _employeeService.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeService.GetEmployee(id);
        }
        [HttpPost]
        public async Task AddEmployee(Employee employee)
        {
            var emp = await _employeeService.GetEmployee(employee.Id);
            if (emp == null)
            {
                await _employeeService.Add(employee);
            }
        }
        [HttpPut]
        public async Task UpdateEmployee(Employee employee)
        {
            var emp = await _employeeService.GetEmployee(employee.Id);
            if (emp != null)
            {
                await _employeeService.Update(employee);
            }
        }
    }
}
