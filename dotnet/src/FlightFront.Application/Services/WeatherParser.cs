using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public class WeatherParser : IParser
{
    
    public object? TryParse(string[] substringTokens)
    {
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