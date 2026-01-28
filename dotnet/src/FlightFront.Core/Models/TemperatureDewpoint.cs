namespace FlightFront.Core.Models;

public record TemperatureDewpoint
{
    public int TemperatureCelsius { get; init; }
    public int DewpointCelsius { get; init; }
}
