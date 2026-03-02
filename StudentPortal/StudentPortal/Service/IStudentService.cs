using StudentPortal.Models;

namespace StudentPortal.Service
{
    public interface IStudentService
    {
        Task<List<Student>> SearchAsync(string q = null);
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);
    }
}
