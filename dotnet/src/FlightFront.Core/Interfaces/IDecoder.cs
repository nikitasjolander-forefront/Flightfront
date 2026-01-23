namespace FlightFront.Core.Interfaces;

internal interface IDecoder
{
    public Object TryParse(string[] substringTokens);  // Revise return type
}
