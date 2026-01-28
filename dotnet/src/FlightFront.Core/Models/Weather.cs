namespace FlightFront.Core.Models;

public record Weather
{
    public string Intensity { get; init; } = string.Empty;  // -, +, or empty
    public string Descriptor { get; init; } = string.Empty;  // TS, SH, FZ, etc.
    public List<string> Phenomena { get; init; } = new();    // RA, SN, FG, etc.
}