namespace FlightFront.Core.Models;

public record Visibility
{
    public int Distance { get; init; }  // in meters
    public string Unit { get; init; } = string.Empty;  // SM, M, KM
}