using ClassLibDbFirstApproch.Models;

namespace WebApplication.Services
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task Create(Student student);
        Task<Student> GetById(int id);
        Task Update(Student student);
        Task Delete(int id);
    }
}
