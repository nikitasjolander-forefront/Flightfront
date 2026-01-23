using FlightFront.Core.Interfaces;
using FlightFront.Infrastructure.Validators;
using Microsoft.AspNetCore.Mvc;

namespace FlightFront.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AirportController : ControllerBase
{
    private readonly IAirportSearchService _airportSearchService;

    public AirportController(IAirportSearchService airportSearchService)
    {
        _airportSearchService = airportSearchService;
    }

    [HttpGet("searchIcao")]
    public async Task<IActionResult> SearchIcao([FromQuery] string query, CancellationToken cancellationToken)
    {
        var validation = SearchQueryValidator.ValidateQuery(query);
        if (!validation.IsValid)
            return BadRequest(validation.ErrorMessage);

        var results = await _airportSearchService.SearchIcaoAsync(query, cancellationToken);
        return Ok(results);
    }

    [HttpGet("searchName")]
    public async Task<IActionResult> SearchName([FromQuery] string query, CancellationToken cancellationToken)
    {
        var validation = SearchQueryValidator.ValidateNameQuery(query);
        if (!validation.IsValid)
            return BadRequest(validation.ErrorMessage);

        var results = await _airportSearchService.SearchNameAsync(query, cancellationToken);
        return Ok(results);
    }

    [HttpGet("searchCity")]
    public async Task<IActionResult> SearchMuncipality([FromQuery] string query, CancellationToken cancellationToken)
    {
        var validation = SearchQueryValidator.ValidateQuery(query);
        if (!validation.IsValid)
            return BadRequest(validation.ErrorMessage);

        var results = await _airportSearchService.SearchMuncipalityAsync(query, cancellationToken);
        return Ok(results);
    }
}
