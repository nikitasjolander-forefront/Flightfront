using FlightFront.Core.Models;

namespace FlightFront.Core.Interfaces;

public interface IParser
{
    public object? TryParse(string[] substringTokens);  

    public void ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens);
}
