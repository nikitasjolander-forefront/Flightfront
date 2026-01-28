using FlightFront.Core.Models.FlightFront.Core.Models;

namespace FlightFront.Core.Interfaces;

public interface IParser
{
    public object TryParse(string[] substringTokens);  // Revise return type

    public ParsedMetarBuilder ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens);
}
