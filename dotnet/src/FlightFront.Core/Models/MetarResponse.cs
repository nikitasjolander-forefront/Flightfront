using System.Text.Json.Serialization;

namespace FlightFront.Core.Models;

public record MetarResponse(
    int Results,
    MetarData[] Data
);

public record MetarData(
    string Icao,
    [property: JsonPropertyName("raw_text")] string? Raw,
    Barometer? Barometer,
    Ceiling? Ceiling,
    CloudLayer[]? Clouds,
    Condition[]? Conditions,
    Temperature? Dewpoint,
    Elevation? Elevation,
    [property: JsonPropertyName("flight_category")] string? FlightCategory,
    Humidity? Humidity,
    DateTime Observed,
    Rain? Rain,
    Station? Station,
    Temperature? Temperature,
    Visibility? Visibility,
    Wind? Wind
);

public record Barometer(double Hg, double Hpa, double Kpa, double Mb);

public record Ceiling(double Feet, double Meters);

public record CloudLayer(
    [property: JsonPropertyName("base_feet_agl")] int BaseFeetAgl,
    [property: JsonPropertyName("base_meters_agl")] int BaseMetersAgl,
    string Code,
    string Text,
    int Feet,
    int Meters
);

public record Condition(string Code, string Text);

public record Elevation(double Feet, double Meters);

public record Humidity(int Percent);

public record Rain(double Inches, double Millimeters);

public record Station(
    Geometry? Geometry,
    string? Location,
    string? Name,
    string? Type
);

public record Geometry(
    double[] Coordinates,
    string Type
);

public record Temperature(int Celsius, int Fahrenheit);

public record Visibility(
    double Miles,
    [property: JsonPropertyName("miles_text")] string? MilesText,
    double Meters,
    [property: JsonPropertyName("meters_text")] string? MetersText
);

public record Wind(
    int Degrees,
    [property: JsonPropertyName("speed_kph")] int SpeedKph,
    [property: JsonPropertyName("speed_kts")] int SpeedKts,
    [property: JsonPropertyName("speed_mph")] int SpeedMph,
    [property: JsonPropertyName("speed_mps")] int SpeedMps,
    [property: JsonPropertyName("gust_kts")] int? GustKts
);