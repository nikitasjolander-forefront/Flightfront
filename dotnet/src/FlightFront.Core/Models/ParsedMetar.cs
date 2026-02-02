namespace FlightFront.Core.Models;

public class ParsedMetar
{
    public string Icao { get; init; } = string.Empty;
    public DateTime? ObservationTime { get; init; }
    public Wind? Wind { get; init; }
    public Visibility? Visibility { get; init; }
    public List<Weather> Weathers { get; init; } = new();
    public Clouds? Clouds { get; init; } 
    public List<string> ParseErrors { get; init; } = new();
}
