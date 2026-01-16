using FlightFront.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightFront.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetarController : ControllerBase
{
    private readonly ICheckWxService _checkWxService;

    public MetarController(ICheckWxService checkWxService)
    {
        _checkWxService = checkWxService;
    }

    [HttpGet("{icaoCode}")]
    public async Task<IActionResult> GetMetar(string icaoCode, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(icaoCode) || icaoCode.Length != 4)
        {
            return BadRequest("Invalid ICAO code. Must be 4 characters.");
        }

        var metar = await _checkWxService.GetMetarAsync(icaoCode, cancellationToken);

        if (metar is null)
        {
            return NotFound($"No METAR found for {icaoCode}");
        }

        return Ok(metar);
    }
}