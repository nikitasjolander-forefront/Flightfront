namespace FlightFront.API.DTOs;

public record TemperatureDto
{
    public int Degree { get; init; }
    public int Dewpoint { get; init; }
}