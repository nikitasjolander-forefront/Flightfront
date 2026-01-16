namespace FlightFront.Core.Models;

public record MetarResponse(
    int Results,
    MetarData[] Data
);

public record MetarData(
    string Icao,
    string Name,
    string Raw,
    Barometer? Barometer,
    CloudLayer[]? Clouds,
    Temperature? Dewpoint,
    Temperature? Temperature,
    string? FlightCategory,
    Humidity? Humidity,
    Visibility? Visibility,
    Wind? Wind,
    DateTime Observed
);

public record Barometer(double Hg, double Hpa, double Mb);
public record CloudLayer(int BaseFeetAgl, string Code, string Text);
public record Temperature(int Celsius, int Fahrenheit);
public record Humidity(int Percent);
public record Visibility(double Miles, string MilesText, double Meters);
public record Wind(int Degrees, int SpeedKts, int? GustKts);