using ClassLibDbFirstApproch.Models;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Services;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentService _service;
        public StudentController(IStudentService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<Student>> GetAll()
        {
            var students = await _service.GetAll();
            return students;
        }
        [HttpGet("{id}")]
        public async Task<Student> GetById(int id)
        {
            var student = await _service.GetById(id);
            return student;
        }
        [HttpPost]
        public async Task Create(Student student)
        {
            await _service.Create(student);
        }
        [HttpPut]
        public async Task Update(Student student)
        {
            await _service.Update(student);
        }
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _service.Delete(id);
        }
    }
}
