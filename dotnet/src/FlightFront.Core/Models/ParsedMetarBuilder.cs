namespace FlightFront.Core.Models;

public class ParsedMetarBuilder
{
    private string RawMetar { get; set; } = string.Empty;
    private string Icao { get; set; } = string.Empty;
    private DateTime? ObservationTime { get; set; }
    private Wind? Wind { get; set; }
   // private Visibility? Visibility { get; set; }
  //  private Weather Weather { get; set; } = new();   // Enskilda objekt med lista?
    private Clouds? Clouds { get; set; }   // Enskilda objekt med lista?
   // private Temperature? Temperature { get; set; }
   // private AirPressure? AirPressure { get; set; }
    private List<string> ParseErrors { get; set; } = new(); 

    public ParsedMetar Build()
    {
        return new ParsedMetar
        {
            RawMetar = RawMetar,
            Icao = Icao,
            ObservationTime = ObservationTime,
            Wind = Wind,
           // Visibility = Visibility,
           // Weather = Weather,
            Clouds = Clouds,
           // Temperature = Temperature,
           //AirPressure = AirPressure,
            ParseErrors = ParseErrors
        };
    }

    // Builder methods for each property
        public ParsedMetarBuilder SetRawMetar(string rawMetar)
    {
        RawMetar = rawMetar;
        return this;
    }
    
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

    /*
        public ParsedMetarBuilder SetVisibility(Visibility visibility)
        {
            Visibility = visibility;
            return this;
        }
    */

    /*    public ParsedMetarBuilder SetWeather(Weather weather)
        {
            Weather.Add(weather);
            return this;
        }
    */


    public ParsedMetarBuilder SetClouds(Clouds clouds)
    {
        Clouds = clouds;
        return this;
    }


    /*
        public ParsedMetarBuilder SetTemperature(Temperature temperature)
        {
            Temperature = temperature;
            return this;
        }
    */
   
    /*
    public ParsedMetarBuilder SetAirPressure(AirPressure airPressure)
        {
            AirPressure = airPressure;
            return this;
        }
    */

    public ParsedMetarBuilder AddParseError(string error)
    {
        ParseErrors.Add(error);
        return this;
    }
    



}