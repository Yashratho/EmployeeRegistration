using EmployeeRegistration.Application.DTOs;
using EmployeeRegistration.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegistration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _service;

    public EmployeesController(IEmployeeService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        return Ok(await _service.GetAllEmployeesAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        return Ok(await _service.GetEmployeeByIdAsync(id));
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employee)
    {
        return Ok(await _service.CreateEmployeeAsync(employee));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employee)
    {
        return Ok(await _service.UpdateEmployeeAsync(id, employee));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        return Ok(await _service.DeleteEmployeeAsync(id));
    }
}
