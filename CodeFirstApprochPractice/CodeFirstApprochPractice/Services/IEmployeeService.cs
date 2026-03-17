using CodeFirstApprochPractice.Models;

namespace CodeFirstApprochPractice.Services
{
    public interface IEmployeeService
    {
         Task Add(Employee employee);
         Task Delete(Employee employee);
         Task<List<Employee>> GetAll();
         Task<Employee> GetEmployee(int id);
         Task Update(Employee employee);
    }
}
