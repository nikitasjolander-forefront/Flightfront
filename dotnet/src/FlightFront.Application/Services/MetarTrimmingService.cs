namespace FlightFront.Application.Services;

public class MetarTrimmingService
{

    private static readonly Regex IcaoRegex = new(@"^[A-Z]{4}$", RegexOptions.Compiled);
    private static readonly Regex TimeRegex = new(@"^\d{6}Z$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex WindRegex = new(@"^(VRB|\d{3})(\d{2})(G\d{2})?(KT|MPS|MPH)$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex VisibilityRegex = new(@"^\d+(SM|KM|M)?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);      // TODO: NDV can be added seperately after this
    private static readonly Regex WeatherRegex = new(@"^(-|\+)?(VC)?(MI|PR|BC|DR|BL|SH|TS|FZ)?(DZ|RA|SN|SG|IC|PL|GR|GS|UP|BR|FG|FU|VA|DU|SA|HZ|PY|PO|SQ|FC|SS|DS)+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex CloudRegex = new(@"^(FEW|SCT|BKN|OVC|NSC)\d{3}([A-Z]{2,3})?$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex TempDewRegex = new(@"^M?\d{2}/M?\d{2}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);
    private static readonly Regex AltimeterRegex = new(@"^A\d{4}$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public MetarTrimmingService()
    {
        
    }


    public MetarTrimmingService(string metar)
    {
        if (string.IsNullOrWhiteSpace(metar))
            return Array.Empty();

        // Print String before removing whitespaces
        Console.WriteLine($"METAR-string to trim: '{metar}'");


        // Split on whitespace, remove empty entries
        var substrings = metar.Split((char[])null, StringSplitOptions.RemoveEmptyEntries); 
        
        
        substrings[0].remove(); // Remove "METAR"/"SPECI" prefix 


        // Trim everything from "RMK" (inclusive)
        var rmkIndex = Array.FindIndex(substrings, str => string.Equals(str, "RMK", StringComparison.OrdinalIgnoreCase));
        if (rmkIndex >= 0)
        {
            if (rmkIndex == 0)
                return Array.Empty<MetarToken>();

            Array.Resize(ref substrings, rmkIndex);  // Resize array to exclude RMK and everything after - remove rmkIndex and everything after
        }


        var tokens = new List<MetarToken>(substrings.Length);

        foreach (var str in substrings)
        {
            tokens.Add(new MetarToken(Classify(str), str));
        }

        return tokens;
    }


    private static TokenType Classify(string token)
    {
        if (IcaoRegex.IsMatch(token))

            return TokenType.Icao;

        if (TimeRegex.IsMatch(token))
            return TokenType.ObservationTime;

        if (WindRegex.IsMatch(token))
            return TokenType.Wind;

        if (VisibilityRegex.IsMatch(token) && token.EndsWith("SM", StringComparison.OrdinalIgnoreCase))
            return TokenType.Visibility;

        if (WeatherRegex.IsMatch(token))
           return TokenType.Weather;    

        if (CloudRegex.IsMatch(token))
            return TokenType.Clouds;

        if (TempDewRegex.IsMatch(token))
            return TokenType.TemperatureDewpoint;

        if (AltimeterRegex.IsMatch(token))
            return TokenType.Altimeter;

        return TokenType.Other;
    }




public readonly record struct MetarToken(TokenType Type, string Value);

public enum TokenType
{
    Icao,
    ObservationTime,
    Wind,
    Visibility,
    Weather,
    Clouds,
    TemperatureDewpoint,
    Altimeter,
    Other
}







}
