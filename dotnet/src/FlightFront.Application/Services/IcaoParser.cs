using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public class IcaoParser : IParser
{
    public object? TryParse(string[] substringTokens)
    {
        if (substringTokens == null || substringTokens.Length == 0)
            return null;

        // ICAO codes are exactly 4 letters and all uppercase
        var icao = substringTokens.FirstOrDefault();
        if (string.IsNullOrWhiteSpace(icao) || icao.Length != 4 || !icao.All(char.IsLetter))
            return null;

        return icao.ToUpperInvariant();
    }

    public void ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var icao = TryParse(substringTokens) as string;
        if (!string.IsNullOrEmpty(icao))
            builder.SetIcao(icao);
    }

}
