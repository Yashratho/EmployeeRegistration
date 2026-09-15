using EmployeeRegistration.Application.DTOs;
using EmployeeRegistration.Application.Responses;

namespace EmployeeRegistration.Application.Services;

public interface ILocationService
{
    Task<ApiResponse<IEnumerable<CountryDto>>> GetCountriesAsync();
    Task<ApiResponse<IEnumerable<StateDto>>> GetAllStatesAsync();
    Task<ApiResponse<IEnumerable<StateDto>>> GetStatesByCountryAsync(int countryId);
}
