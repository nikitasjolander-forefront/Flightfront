namespace FlightFront.Core.Models;

public class ParsedMetarBuilder
{
    private string Icao { get; set; } = string.Empty;
    private DateTime? ObservationTime { get; set; }
    private Wind? Wind { get; set; }
    private Visibility? Visibility { get; set; }
    private List<Weather> Weather { get; set; } = new();   // Enskilda objekt med lista?
    private List<Clouds> Clouds { get; set; } = new();   // Enskilda objekt med lista?
    private Temperature? Temperature { get; set; }
    //private Altimeter? Altimeter { get; set; }   // Ta bort?
    private List<string> ParseErrors { get; set; } = new();

    public ParsedMetar Build()
    {
        return new ParsedMetar
        {
            Icao = Icao,
            ObservationTime = ObservationTime,
            Wind = Wind,
            Visibility = Visibility,
            Weather = Weather,
            Clouds = Clouds,
            Temperature = Temperature,
            // Altimeter = Altimeter,
            ParseErrors = ParseErrors
        };
    }

    // Builder methods for each property
    public ParsedMetarBuilder SetIcao(string icao)
    {
        Icao = icao;
        return this;
    }

    public ParsedMetarBuilder SetObservationTime(DateTime observationTime)
    {
        ObservationTime = observationTime;
        return this;
    }

    public ParsedMetarBuilder SetWind(Wind wind)
    {
        Wind = wind;
        return this;
    }

    public ParsedMetarBuilder SetVisibility(Visibility visibility)
    {
        Visibility = visibility;
        return this;
    }

    public ParsedMetarBuilder AddWeather(Weather weather)
    {
        Weather.Add(weather);
        return this;
    }

    public ParsedMetarBuilder AddCloud(Clouds clouds)
    {
        Clouds.Add(clouds);
        return this;
    }

    public ParsedMetarBuilder SetTemperature(Temperature temperature)
    {
        Temperature = temperature;
        return this;
    }

    /*    public ParsedMetarBuilder SetAltimeter(Altimeter altimeter)
        {
            Altimeter = altimeter;
            return this;
        } */

    public ParsedMetarBuilder AddParseError(string error)
    {
        ParseErrors.Add(error);
        return this;
    }


}