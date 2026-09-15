using EmployeeRegistration.Application.DTOs;
using EmployeeRegistration.Application.Interfaces;
using EmployeeRegistration.Application.Responses;

namespace EmployeeRegistration.Application.Services;

public class LocationService : ILocationService
{
    private readonly ILocationRepository _repository;

    public LocationService(ILocationRepository repository)
    {
        _repository = repository;
    }

    public async Task<ApiResponse<IEnumerable<CountryDto>>> GetCountriesAsync()
    {
        var countries = await _repository.GetCountriesAsync();
        var dtos = countries.Select(c => new CountryDto
        {
            CountryId = c.CountryId,
            CountryName = c.CountryName
        });
        return ApiResponse<IEnumerable<CountryDto>>.SuccessResponse(dtos, "Countries retrieved successfully");
    }

    public async Task<ApiResponse<IEnumerable<StateDto>>> GetAllStatesAsync()
    {
        var states = await _repository.GetAllStatesAsync();
        var dtos = states.Select(s => new StateDto
        {
            StateId = s.StateId,
            StateName = s.StateName,
            CountryId = s.CountryId
        });
        return ApiResponse<IEnumerable<StateDto>>.SuccessResponse(dtos, "States retrieved successfully");
    }

    public async Task<ApiResponse<IEnumerable<StateDto>>> GetStatesByCountryAsync(int countryId)
    {
        var states = await _repository.GetStatesByCountryIdAsync(countryId);
        var dtos = states.Select(s => new StateDto
        {
            StateId = s.StateId,
            StateName = s.StateName,
            CountryId = s.CountryId
        });
        return ApiResponse<IEnumerable<StateDto>>.SuccessResponse(dtos, "States retrieved successfully");
    }
}
