namespace FlightFront.Core.Models;

public record Cloud
{
    public string Coverage { get; init; } = string.Empty;  // FEW, SCT, BKN, OVC
    public int Altitude { get; init; }  // in hundreds of feet
    public string? Type { get; init; }  // CB, TCU, etc. (optional)
}