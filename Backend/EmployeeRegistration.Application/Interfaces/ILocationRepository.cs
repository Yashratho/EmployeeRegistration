using EmployeeRegistration.Domain.Entities;

namespace EmployeeRegistration.Application.Interfaces;

public interface ILocationRepository
{
    Task<IEnumerable<Country_Mst>> GetCountriesAsync();
    Task<IEnumerable<State_Mst>> GetStatesByCountryIdAsync(int countryId);
    Task<IEnumerable<State_Mst>> GetAllStatesAsync();
}
