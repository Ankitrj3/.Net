using CodeFirstApprochPractice.Models;
using CodeFirstApprochPractice.Repository;

namespace CodeFirstApprochPractice.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo _employeeRepo;
        public EmployeeService(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        public async Task Add(Employee employee)
        {
            await _employeeRepo.AddAsync(employee);
        }

        public async Task Delete(Employee employee)
        {
            await _employeeRepo.RemoveAsync(employee);
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _employeeRepo.GetAllEmployees();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await _employeeRepo.GetEmployeeAsync(id);
        }

        public async Task Update(Employee employee)
        {
            await _employeeRepo.UpdateAsync(employee);
        }
    }
}
