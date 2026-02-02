using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;
using System.Text.RegularExpressions;

namespace FlightFront.Application.Services;

public class CloudsParser : IParser
{
    // Cloud layer pattern: FEW020, SCT035CB, BKN080TCU, OVC100, VV001, BKN013///
    private const string CoverageRegexPattern = @"^(FEW|SCT|BKN|OVC|VV)";
    private const string HeightRegexPattern = @"(\d{3})";
    private const string ModifierRegexPattern = @"(CB|TCU|///)?$";  // optional

    // Special codes without height: SKC, CLR, NSC, NCD
    private const string SpecialCodesRegexPattern = @"^(SKC|CLR|NSC|NCD)$";

    public object? TryParse(string[] substringTokens)
    {
        if (substringTokens == null || substringTokens.Length == 0)
            return null;

        var clouds = new List<Clouds>();

        foreach (var token in substringTokens)
        {
            // Check for special codes first (no height)
            var specialMatch = Regex.Match(token, SpecialCodesRegexPattern, RegexOptions.IgnoreCase);
            if (specialMatch.Success)
            {
                var specialCode = specialMatch.Groups[1].Value.ToUpperInvariant();
                clouds.Add(new Clouds
                {
                    CloudCover = Enum.Parse<CloudType>(specialCode),
                    CloudHeight = 0,
                    Modifier = null
                });
                continue;
            }

            // Check for standard cloud layers (with height)
            var match = Regex.Match(token, $"{CoverageRegexPattern}{HeightRegexPattern}{ModifierRegexPattern}", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                var coverage = match.Groups[1].Value.ToUpperInvariant();
                var height = match.Groups[2].Value;
                var modifier = match.Groups[3].Success && match.Groups[3].Value != "///" 
                    ? match.Groups[3].Value.ToUpperInvariant() 
                    : null;
                var cloudType = Enum.Parse<CloudType>(coverage);
                clouds.Add(new Clouds
                {
                    CloudCover = cloudType,
                    CloudHeight = int.Parse(height) * 100,  // Convert to feet
                    Modifier = modifier != null ? Enum.Parse<CloudModifier>(modifier) : null
                });
            }
        }

        return clouds.Count > 0 ? clouds : null;
    }

    public void ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var clouds = TryParse(substringTokens) as List<Clouds>;
        if (clouds != null)
        {
            foreach (var cloud in clouds)
            {
                builder.AddClouds(cloud); 
            }
        }
    }
}