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
        var response = await _service.GetAllEmployeesAsync();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var response = await _service.GetEmployeeByIdAsync(id);
        if (!response.Success) return NotFound(response);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto employee)
    {
        var response = await _service.CreateEmployeeAsync(employee);
        if (!response.Success) return BadRequest(response);
        return CreatedAtAction(nameof(GetEmployee), new { id = response.Data?.EmployeeId }, response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employee)
    {
        var response = await _service.UpdateEmployeeAsync(id, employee);
        if (!response.Success) return BadRequest(response);
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var response = await _service.DeleteEmployeeAsync(id);
        return Ok(response);
    }
}
