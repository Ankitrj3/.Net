using CodeFirstApprochPractice.Data;
using CodeFirstApprochPractice.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApprochPractice.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly MyDbContext _context;
        public EmployeeRepo(MyDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Employee employee)
        {
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> GetEmployeeAsync(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            return employee;
        }

        public async Task RemoveAsync(Employee employee)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            var find = _context.Employees.Find(employee.Id);
            if(find != null)
            {
               _context.Entry(find).CurrentValues.SetValues(employee);
               await _context.SaveChangesAsync();   
            }
        }
    }
}
