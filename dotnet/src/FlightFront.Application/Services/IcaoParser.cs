using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public class IcaoParser : IParser
{
    public object? TryParse(string[] substringTokens) =>
    substringTokens?.FirstOrDefault();

    public void ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var icao = TryParse(substringTokens) as string;
        if (!string.IsNullOrEmpty(icao))
            builder.SetIcao(icao);
    }

}
