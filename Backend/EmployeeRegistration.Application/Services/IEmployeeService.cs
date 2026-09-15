using EmployeeRegistration.Application.DTOs;
using EmployeeRegistration.Application.Responses;

namespace EmployeeRegistration.Application.Services;

public interface IEmployeeService
{
    Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync();
    Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(int id);
    Task<ApiResponse<EmployeeDto>> CreateEmployeeAsync(EmployeeDto dto);
    Task<ApiResponse<object>> UpdateEmployeeAsync(int id, EmployeeDto dto);
    Task<ApiResponse<object>> DeleteEmployeeAsync(int id);
}
