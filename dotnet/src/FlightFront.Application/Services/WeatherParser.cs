using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;
using FlightFront.Core.Models.FlightFront.Core.Models;
using System.Text.RegularExpressions;

namespace FlightFront.Application.Services;

public class WeatherParser : IParser
{       
    private static readonly Regex WeatherRegexPattern = new(@"^(-|\+)?(SN|RA|FG|BR)$");

    public object TryParse(string[] substringTokens)
    {
        if (substringTokens == null || substringTokens.Length == 0)
            return null;

        foreach (var token in substringTokens)
        {
            if (string.IsNullOrWhiteSpace(token))
                continue;

            var match = WeatherRegexPattern.Match(token);
            if (!match.Success)
                continue;

            var intensity = match.Groups[1].Success ? match.Groups[1].Value : string.Empty;
            var phenomenon = match.Groups[2].Value.ToUpperInvariant();

            return new Weather
            {
                Intensity = intensity,
                Descriptor = string.Empty,
                Phenomena = new List<string> { phenomenon }
            };
        }

        return null;
    }

    public ParsedMetarBuilder ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var weather = TryParse(substringTokens) as Weather;
        if (weather != null)
        {
            builder.AddWeather(weather);
        }
        return builder;
    }

}