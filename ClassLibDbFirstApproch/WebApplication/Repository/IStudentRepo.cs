using ClassLibDbFirstApproch.Models;

namespace WebApplication.Repository
{
    public interface IStudentRepo
    {
        Task<IEnumerable<Student>> GetAllStudent();
        Task CreateAsync(Student student);
        Task<Student> GetStudentById(int id);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int id);
    }
}
