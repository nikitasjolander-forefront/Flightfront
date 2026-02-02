namespace FlightFront.API.DTOs;

public class ParsedMetarDto
{
    public string Icao { get; init; } = string.Empty;
    public DateTime? ObservationTime { get; init; }
    public WindDto? Wind { get; init; }
    public string? Visibility { get; init; }
    public WeatherDto? Weather { get; init; }
    // public CloudsDto? Clouds { get; init; }
    // public TemperatureDto? Temperature { get; init; }
    // public AirPressureDto? AirPressure { get; init; }
    public List<string> ParseErrors { get; init; } = new();
}
