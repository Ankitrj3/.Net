using CodeFirstApprochPractice.Models;

namespace CodeFirstApprochPractice.Repository
{
    public interface IEmployeeRepo
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeAsync(int id);
        Task AddAsync(Employee employee);
        Task RemoveAsync(Employee employee);
        Task UpdateAsync(Employee employee);
    }
}
