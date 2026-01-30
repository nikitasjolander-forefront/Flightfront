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
                builder.SetClouds(cloud);
            }
        }
    }

    public string CloudTypeConverter(CloudType cloudType)
    {
        return cloudType switch
        {
            CloudType.SKC => "Sky Clear",
            CloudType.CLR => "Clear (automated)",
            CloudType.NSC => "No Significant Cloud",
            CloudType.NCD => "No Cloud Detected",
            CloudType.FEW => "Few",
            CloudType.SCT => "Scattered",
            CloudType.BKN => "Broken",
            CloudType.OVC => "Overcast",
            CloudType.VV => "Vertical Visibility",
            _ => "-"
        };
    }
}

// Examples:
// "FEW007"       -> Few at 700 ft
// "BKN014CB"     -> Broken at 1400 ft with Cumulonimbus
// "SCT035TCU"    -> Scattered at 3500 ft with Towering Cumulus
// "OVC100"       -> Overcast at 10000 ft
// "VV001"        -> Vertical visibility 100 ft
// "BKN013///"    -> Broken at 1300 ft, cloud type unknown (modifier ignored)
// "SKC"          -> Sky clear
// "CLR"          -> Clear (automated)
