namespace FlightFront.Core.Models;

public class ParsedMetar
{
    public string Icao { get; init; } = string.Empty;
    public DateTime? ObservationTime { get; init; }
    public Wind? Wind { get; set; }
   // public Visibility? Visibility { get; init; }
   // public Weather Weather { get; init; }
    public Clouds? Clouds { get; init; } 
   // public Temperature? Temperature { get; init; }
   // public AirPressure? AirPressure { get; init; }
    public List<string> ParseErrors { get; init; } = new();
}
