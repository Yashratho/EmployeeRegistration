using EmployeeRegistration.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeRegistration.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LocationsController : ControllerBase
{
    private readonly ILocationService _service;

    public LocationsController(ILocationService service)
    {
        _service = service;
    }

    [HttpGet("countries")]
    public async Task<IActionResult> GetCountries()
    {
        var response = await _service.GetCountriesAsync();
        return Ok(response);
    }

    [HttpGet("states")]
    public async Task<IActionResult> GetAllStates()
    {
        var response = await _service.GetAllStatesAsync();
        return Ok(response);
    }

    [HttpGet("countries/{countryId}/states")]
    public async Task<IActionResult> GetStatesByCountry(int countryId)
    {
        var response = await _service.GetStatesByCountryAsync(countryId);
        return Ok(response);
    }
}
