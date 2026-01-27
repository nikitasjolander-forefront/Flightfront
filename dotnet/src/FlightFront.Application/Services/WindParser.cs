using FlightFront.Core.Interfaces;

namespace FlightFront.Application.Services;



public class WindParser : IParser
{
    private const string DirectionRegexPattern = @"^([0-9]{3}|VRB|///)";
    private const string SpeedRegexPattern = "P?([/0-9]{2,3}|//)";
    private const string SpeedVariationsRegexPattern = "(GP?([0-9]{2,3}))?"; // optional
    private const string UnitRegexPattern = "(KT|MPS|MPH)";
    private const string DirectionVariationsRegexPattern = "( ([0-9]{3})V([0-9]{3}))?"; // optional


    public object TryParse(string[] substringTokens)
    {
        if (substringTokens.Empty())
            return null;   
        

        foreach (var token in substringTokens)
        {
            var match = Regex.Match(token, $"^{DirectionRegexPattern}{SpeedRegexPattern}{SpeedVariationsRegexPattern}{UnitRegexPattern}{DirectionVariationsRegexPattern}$", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var direction = match.Groups[1].Value.Replace("///", null).Replace("VRB", "");
                var speed = match.Groups[2].Value;
                var gust = match.Groups[4].Success ? match.Groups[4].Value.Replace("G", "") : null; 
                var unit = match.Groups[5].Value;
                var varFrom = match.Groups[7].Success ? match.Groups[7].Value : null;
                var varTo = match.Groups[8].Success ? match.Groups[8].Value.Replace("V", "") : null;
                return new Wind
                {
                    Direction = direction,
                    IsVariable = direction == "VRB",
                    Speed = speed,
                    Gust = gust,
                    Unit = unit,
                    VariationFrom = varFrom,
                    VariationTo = varTo
                };
            }

        }

    }

    public ParsedMetarBuilder ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var wind = TryParse(substringTokens) as Wind;
        if (wind != null)
        {
            builder.SetWind(wind);
        }
        return builder;
    }

}

// "METAR CYFB 221100Z   --> 33017G23KT <-- 4SM BLSN BKN180 M28/M32 A2983 RMK AC6 SLP109";
// "METAR BGTL 221055Z AUTO   --> 10009KT <-- 9999    CLR M20/M23 A2999 RMK AO2 SLP143 T12031234 TSNO $";

/*
 * Vind

        dddffKT
        VRBffKT
        dddffGggKT
*/