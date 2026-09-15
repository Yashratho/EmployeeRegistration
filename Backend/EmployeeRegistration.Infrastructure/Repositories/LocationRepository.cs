using EmployeeRegistration.Application.Interfaces;
using EmployeeRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegistration.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly AppDbContext _context;

    public LocationRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country_Mst>> GetCountriesAsync()
    {
        return await _context.Countries.ToListAsync();
    }

    public async Task<IEnumerable<State_Mst>> GetStatesByCountryIdAsync(int countryId)
    {
        return await _context.States
            .Where(s => s.CountryId == countryId)
            .ToListAsync();
    }
    
    public async Task<IEnumerable<State_Mst>> GetAllStatesAsync()
    {
        return await _context.States.ToListAsync();
    }
}
