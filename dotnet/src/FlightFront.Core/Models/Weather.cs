namespace FlightFront.Core.Models;
public record Weather
{
    public string Intensity { get; init; } = string.Empty;  
    public string Descriptor { get; init; } = string.Empty;
    public List<string> Phenomena { get; init; } = new();
}