using ClassLibDbFirstApproch.Models;
using Microsoft.EntityFrameworkCore;
namespace WebApplication.Repository
{
    public class StudentRepo : IStudentRepo
    {
        private readonly StudentPortalDbContext _context;
        public StudentRepo(StudentPortalDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Student student)
        {
            var newStudent = new Student
            {
                FullName = student.FullName,
                Email = student.Email,
                Phone = student.Phone,
                Status = student.Status,
                JoinDate = student.JoinDate,
                CreatedAt = DateTime.Now
            };
            _context.Students.Add(newStudent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int id)
        {
            var find = await _context.Students.FindAsync(id);
            if (find != null)
            {
                _context.Students.Remove(find);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Student>> GetAllStudent()
        {
            return await _context.Students.ToListAsync();
        }
        public async Task<Student> GetStudentById(int id)
        {
            var find = await _context.Students.FindAsync(id);
            if (find != null)
            {
                return find;
            }
            return null;
        }

        public async Task UpdateStudent(Student student)
        {
            var find = await _context.Students.FindAsync(student.StudentId);
            if (find != null)
            {
                find.FullName = student.FullName;
                find.Email = student.Email;
                find.Phone = student.Phone;
                find.Status = student.Status;
                find.JoinDate = student.JoinDate;
                await _context.SaveChangesAsync();
            }
        }
    }
}
