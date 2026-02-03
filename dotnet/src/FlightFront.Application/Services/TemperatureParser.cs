using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;
using System.Text.RegularExpressions;

namespace FlightFront.Application.Services;

public class TemperatureParser : IParser
{
    // Pattern: temperature/dewpoint where M indicates negative (e.g., 02/M01, M05/M10, 15/12)
    private const string TemperatureRegexPattern = @"^(M)?(\d{2})/(M)?(\d{2})$";

    public object? TryParse(string[] substringTokens)
    {
        if (substringTokens == null || substringTokens.Length == 0)
            return null;

        foreach (var token in substringTokens)
        {
            var match = Regex.Match(token, TemperatureRegexPattern);
            if (match.Success)
            {
                var tempIsNegative = match.Groups[1].Success;
                var tempValue = int.Parse(match.Groups[2].Value);
                var dewpointIsNegative = match.Groups[3].Success;
                var dewpointValue = int.Parse(match.Groups[4].Value);

                return new Temperature
                {
                    Degree = tempIsNegative ? -tempValue : tempValue,
                    Dewpoint = dewpointIsNegative ? -dewpointValue : dewpointValue
                };
            }
        }

        return null;
    }

    public void ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var temperature = TryParse(substringTokens) as Temperature;
        if (temperature != null)
        {
            builder.SetTemperature(temperature);
        }
    }
}
