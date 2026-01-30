using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;
using System.Text.RegularExpressions;

namespace FlightFront.Application.Services;



public class WindParser : IParser
{
    private const string DirectionRegexPattern = @"^([0-9]{3}|VRB|///)";
    private const string SpeedRegexPattern = "P?([/0-9]{2,3}|//)";
    private const string SpeedVariationsRegexPattern = "(GP?([0-9]{2,3}))?"; // optional
    private const string UnitRegexPattern = "(KT|MPS|MPH)";
    private const string DirectionVariationsRegexPattern = "( ([0-9]{3})V([0-9]{3}))?"; // optional


    public object? TryParse(string[] substringTokens)
    {
       if (substringTokens == null || substringTokens.Length == 0)
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
                    Direction = int.TryParse(direction, out var dirVal) ? dirVal : (int?)null,
                    IsVariable = direction == "VRB",
                    Speed = int.Parse(speed),
                    Gust = int.TryParse(gust, out var gustVal) ? gustVal : (int?)null,
                    Unit = unit,
                    VariationFrom = int.TryParse(varFrom, out var fromVal) ? fromVal : (int?)null,
                    VariationTo = int.TryParse(varTo, out var toVal) ? toVal : (int?)null
                };
            }

        }
        return null;

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

