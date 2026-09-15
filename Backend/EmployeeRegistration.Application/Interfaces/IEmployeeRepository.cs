using EmployeeRegistration.Domain.Entities;

namespace EmployeeRegistration.Application.Interfaces;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee_Mst>> GetAllAsync();
    Task<Employee_Mst?> GetByIdAsync(int id);
    Task<Employee_Mst> AddAsync(Employee_Mst employee);
    Task UpdateAsync(Employee_Mst employee);
    Task DeleteAsync(int id);
    Task<bool> IsMobileNumberExistsAsync(string mobileNum, int? excludeEmployeeId = null);
}
