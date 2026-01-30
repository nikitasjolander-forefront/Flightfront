using FlightFront.Core.Models;

namespace FlightFront.Core.Interfaces;

public interface IParser
{
    public object? TryParse(string[] substringTokens);  

    public ParsedMetarBuilder ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens);
}
