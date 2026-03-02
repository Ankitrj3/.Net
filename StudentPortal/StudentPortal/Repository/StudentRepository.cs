using Microsoft.EntityFrameworkCore;
using StudentPortal.Models;

namespace StudentPortal.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentPortalDbContext _db;
        public StudentRepository(StudentPortalDbContext db) {
            _db = db;
        }
        public async Task<List<Student>>GetAllAsync(string q = null)
        {
            var query = _db.Students.AsQueryable();
            if (!string.IsNullOrEmpty(q))
            {
                q = q.Trim().ToLower();
                query = query.Where(s => s.FullName.Contains(q) || s.Email.Contains(q));
            }
            return await query.OrderByDescending(s => s.CreatedAt).ToListAsync();
        }

        public async Task AddStudentAsync(Student student)
        {
            if(student == null)
            {
                throw new ArgumentNullException(nameof(student));
            }
            student.CreatedAt = DateTime.UtcNow;
            await _db.Students.AddAsync(student);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteStudentAsync(int id)
        {
            var find = await _db.Students.FindAsync(id);
            _db.Students.Remove(find);
            await _db.SaveChangesAsync();
        }

        public async Task<Student> GetStudentByIdAsync(int id)
        { 
            var query = _db.Students.AsQueryable();
            var student = await query.Where(s => s.StudentId == id).FirstOrDefaultAsync();
            return student;
        }

        public async Task UpdateStudentAsync(Student student)
        {
            var query = _db.Students.AsQueryable();
            var find = query.Where(s => s.StudentId == student.StudentId).FirstOrDefault();
            if(find == null)
            {
                throw new ArgumentException($"Student with ID {student.StudentId} not found.");
            }
            find.FullName = student.FullName;
            find.Email = student.Email;
            find.Phone = student.Phone;
            find.Status = student.Status;
            find.JoinDate = student.JoinDate;
            _db.Students.Update(find);
            await _db.SaveChangesAsync();
        }
    }
}
