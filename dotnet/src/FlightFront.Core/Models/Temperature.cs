namespace FlightFront.Core.Models;

public record Temperature
{
    public int TemperatureCelsius { get; init; }
    public int DewpointCelsius { get; init; }
}
