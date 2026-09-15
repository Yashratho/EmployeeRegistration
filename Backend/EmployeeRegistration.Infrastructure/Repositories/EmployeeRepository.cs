using EmployeeRegistration.Application.Interfaces;
using EmployeeRegistration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRegistration.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;

    public EmployeeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Employee_Mst>> GetAllAsync()
    {
        return await _context.Employees
            .Include(e => e.Country)
            .Include(e => e.State)
            .ToListAsync();
    }

    public async Task<Employee_Mst?> GetByIdAsync(int id)
    {
        return await _context.Employees
            .Include(e => e.Country)
            .Include(e => e.State)
            .FirstOrDefaultAsync(e => e.EmployeeId == id);
    }

    public async Task<Employee_Mst> AddAsync(Employee_Mst employee)
    {
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return await _context.Employees
            .Include(e => e.Country)
            .Include(e => e.State)
            .FirstAsync(e => e.EmployeeId == employee.EmployeeId);
    }

    public async Task UpdateAsync(Employee_Mst employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsMobileNumberExistsAsync(string mobileNum, int? excludeEmployeeId = null)
    {
        var query = _context.Employees.Where(e => e.MobileNum == mobileNum);
        if (excludeEmployeeId.HasValue)
        {
            query = query.Where(e => e.EmployeeId != excludeEmployeeId.Value);
        }
        return await query.AnyAsync();
    }
}
