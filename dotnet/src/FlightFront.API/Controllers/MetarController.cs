using Microsoft.AspNetCore.Mvc;
using Flightfront.Core.Interfaces;
using FlightFront.Application.Services;

namespace FlightFront.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MetarController : ControllerBase
{
    private readonly ICheckWxService _checkWxService;
    private readonly MetarParserService _metarParserService;

    public MetarController(ICheckWxService checkWxService, MetarParserService metarParserService)
    {
        _checkWxService = checkWxService;
        _metarParserService = metarParserService;
    }

    [HttpGet("/{metarCode}")]
    public async Task<IActionResult> GetParsedMetar(string metarCode) //CancellationToken cancellationToken ?
    {
        if (string.IsNullOrWhiteSpace(metarCode))
        {
            return BadRequest("METAR code cannot be empty.");
        }

        var parsedMetar = _metarParserService.Parse(metarCode);

        if (parsedMetar is null)
        {
            return NotFound($"Could not parse METAR: {metarCode}");
        }

        return Ok(parsedMetar);  //Ändra till DTO

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

