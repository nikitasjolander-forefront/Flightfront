using Microsoft.AspNetCore.Mvc;
using Flightfront.Core.Interfaces;

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

    [HttpGet("{icaoCode}/decoded")]
    public async Task<IActionResult> GetMetarDecoded(string icaoCode, CancellationToken cancellationToken)
    {
        if (!IsValidIcaoCode(icaoCode))
        {
            return BadRequest("Invalid ICAO code. Must be 4 letters.");
        }

        var metar = await _checkWxService.GetMetarAsync(icaoCode, cancellationToken);

        if (metar is null)
        {
            return NotFound($"No METAR found for {icaoCode}");
        }

        return Ok(metar);
    }

    [HttpGet("{icaoCode}")]
    public async Task<IActionResult> GetMetar(string icaoCode, CancellationToken cancellationToken)
    {
        if (!IsValidIcaoCode(icaoCode))
        {
            return BadRequest("Invalid ICAO code. Must be 4 letters.");
        }

        var metar = await _checkWxService.GetMetar(icaoCode, cancellationToken);

        if (metar is null)
        {
            return NotFound($"No METAR found for {icaoCode}");
        }

        return Ok(metar);
    }

    private static bool IsValidIcaoCode(string icaoCode) =>
        !string.IsNullOrWhiteSpace(icaoCode) && icaoCode.Length == 4 && icaoCode.All(char.IsLetter);
}