namespace FlightFront.Core.Models;

public class ParsedMetar
{
    public string Icao { get; init; } = string.Empty;
    public DateTime? ObservationTime { get; init; }
    public Wind? Wind { get; set; }
   // public Visibility? Visibility { get; init; }
   // public Weather Weather { get; init; } = new(); //Lista av objekt, eller objekt med lista med string?
    public Clouds? Clouds { get; init; }  //Lista av objekt, eller objekt med lista med string?
   // public Temperature? Temperature { get; init; }
    public List<string> ParseErrors { get; init; } = new();
}
