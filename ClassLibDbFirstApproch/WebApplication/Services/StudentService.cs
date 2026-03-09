using ClassLibDbFirstApproch.Models;
using WebApplication.Repository;

namespace WebApplication.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepo _repo;

        public StudentService(IStudentRepo repo)
        {
            _repo = repo;
        }

        public async Task Create(Student student)
        {
            await _repo.CreateAsync(student);
        }

        public async Task Delete(int id)
        {
            await _repo.DeleteStudent(id);
        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await _repo.GetAllStudent();
        }

        public async Task<Student> GetById(int id)
        {
            return await _repo.GetStudentById(id);
        }

        public async Task Update(Student student)
        {
            await _repo.UpdateStudent(student);
        }
    }
}