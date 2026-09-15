using EmployeeRegistration.Application.DTOs;
using EmployeeRegistration.Application.Interfaces;
using EmployeeRegistration.Application.Responses;
using EmployeeRegistration.Domain.Entities;

namespace EmployeeRegistration.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<EmployeeDto>>> GetAllEmployeesAsync()
    {
        var employees = await _repository.GetAllAsync();
        var dtos = employees.Select(MapToDto);
        return ApiResponse<IEnumerable<EmployeeDto>>.SuccessResponse(dtos, "Employees retrieved successfully");
    }

    public async Task<ApiResponse<EmployeeDto>> GetEmployeeByIdAsync(int id)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
            return ApiResponse<EmployeeDto>.ErrorResponse("Employee not found");

        return ApiResponse<EmployeeDto>.SuccessResponse(MapToDto(employee), "Employee retrieved successfully");
    }

    public async Task<ApiResponse<EmployeeDto>> CreateEmployeeAsync(EmployeeDto dto)
    {
        if (await _repository.IsMobileNumberExistsAsync(dto.MobileNum))
            return ApiResponse<EmployeeDto>.ErrorResponse("Already registered user. Please enter a new one");

        var entity = MapToEntity(dto);
        var created = await _repository.AddAsync(entity);
        return ApiResponse<EmployeeDto>.SuccessResponse(MapToDto(created), "Employee created successfully");
    }

    public async Task<ApiResponse<object>> UpdateEmployeeAsync(int id, EmployeeDto dto)
    {
        if (id != dto.EmployeeId)
            return ApiResponse<object>.ErrorResponse("Invalid ID");

        if (await _repository.IsMobileNumberExistsAsync(dto.MobileNum, id))
            return ApiResponse<object>.ErrorResponse("Already registered user. Please enter a new one");

        var existing = await _repository.GetByIdAsync(id);
        if (existing == null)
            return ApiResponse<object>.ErrorResponse("Employee not found");

        UpdateEntity(existing, dto);
        await _repository.UpdateAsync(existing);
        return ApiResponse<object>.SuccessResponse(null, "Employee updated successfully");
    }

    public async Task<ApiResponse<object>> DeleteEmployeeAsync(int id)
    {
        await _repository.DeleteAsync(id);
        return ApiResponse<object>.SuccessResponse(null, "Employee deleted successfully");
    }

    private static EmployeeDto MapToDto(Employee_Mst emp) => new()
    {
        EmployeeId = emp.EmployeeId,
        EmployeeName = emp.EmployeeName,
        Age = emp.Age,
        MobileNum = emp.MobileNum,
        Pincode = emp.Pincode,
        DOB = emp.DOB,
        AddressLine1 = emp.AddressLine1,
        AddressLine2 = emp.AddressLine2,
        StateId = emp.StateId,
        CountryId = emp.CountryId,
        Country = emp.Country != null ? new CountryDto { CountryId = emp.Country.CountryId, CountryName = emp.Country.CountryName } : null,
        State = emp.State != null ? new StateDto { StateId = emp.State.StateId, StateName = emp.State.StateName, CountryId = emp.State.CountryId } : null
    };

    private static Employee_Mst MapToEntity(EmployeeDto dto) => new()
    {
        EmployeeId = dto.EmployeeId,
        EmployeeName = dto.EmployeeName,
        Age = dto.Age,
        MobileNum = dto.MobileNum,
        Pincode = dto.Pincode,
        DOB = dto.DOB,
        AddressLine1 = dto.AddressLine1,
        AddressLine2 = dto.AddressLine2,
        StateId = dto.StateId,
        CountryId = dto.CountryId
    };

    private static void UpdateEntity(Employee_Mst entity, EmployeeDto dto)
    {
        entity.EmployeeName = dto.EmployeeName;
        entity.Age = dto.Age;
        entity.MobileNum = dto.MobileNum;
        entity.Pincode = dto.Pincode;
        entity.DOB = dto.DOB;
        entity.AddressLine1 = dto.AddressLine1;
        entity.AddressLine2 = dto.AddressLine2;
        entity.StateId = dto.StateId;
        entity.CountryId = dto.CountryId;
    }
}
