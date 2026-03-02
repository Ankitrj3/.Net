using StudentPortal.Models;

namespace StudentPortal.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync(string q = null);
        Task<Student> GetStudentByIdAsync(int id);
        Task AddStudentAsync(Student student);
        Task UpdateStudentAsync(Student student);
        Task DeleteStudentAsync(int id);

    }
}