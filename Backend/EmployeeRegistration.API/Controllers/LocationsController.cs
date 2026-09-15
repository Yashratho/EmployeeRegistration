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
        return Ok(await _service.GetCountriesAsync());
    }

    [HttpGet("states")]
    public async Task<IActionResult> GetAllStates()
    {
        return Ok(await _service.GetAllStatesAsync());
    }

    [HttpGet("countries/{countryId}/states")]
    public async Task<IActionResult> GetStatesByCountry(int countryId)
    {
        return Ok(await _service.GetStatesByCountryAsync(countryId));
    }
}
