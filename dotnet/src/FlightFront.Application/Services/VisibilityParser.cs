using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;
using System.Text.RegularExpressions;

namespace FlightFront.Application.Services;

public class VisibilityParser : IParser
{
    
// METAR visibility formats:
    // - 9999 (meters, 10km or more)
    // - 0000 to 9998 (meters)
    // - 1/2SM, 1SM, 10SM (statute miles)
    // - M1/4SM (less than 1/4 statute mile)
    // - CAVOK (Ceiling And Visibility OK - implies 10km+)
    
    private const string MetersPattern = @"^([0-9]{4})$";
    private const string StatuteMilesPattern = @"^(M)?(\d+)?(/?(\d+))?SM$";
    private const string CavokPattern = @"^CAVOK$";

    public object? TryParse(string[] substringTokens)
    {
        if (substringTokens == null || substringTokens.Length == 0)
            return null;

        foreach (var token in substringTokens)
        {
            // Check for CAVOK
            if (Regex.IsMatch(token, CavokPattern, RegexOptions.IgnoreCase))
            {
                return new Visibility
                {
                    Distance = 10000,
                    Unit = "M"
                };
            }

            // Check for meters (e.g., 9999, 5000)
            var metersMatch = Regex.Match(token, MetersPattern);
            if (metersMatch.Success)
            {
                return new Visibility
                {
                    Distance = int.Parse(metersMatch.Groups[1].Value),
                    Unit = "M"
                };
            }

            // Check for statute miles (e.g., 10SM, 1/2SM, M1/4SM)
            var statuteMilesMatch = Regex.Match(token, StatuteMilesPattern, RegexOptions.IgnoreCase);
            if (statuteMilesMatch.Success)
            {
                var isLessThan = statuteMilesMatch.Groups[1].Success; // M prefix
                var whole = statuteMilesMatch.Groups[2].Success ? int.Parse(statuteMilesMatch.Groups[2].Value) : 0;
                var fraction = statuteMilesMatch.Groups[3].Value; // e.g., /4
                
                int distanceInMeters = CalculateStatuteMilesToMeters(whole, fraction, isLessThan);

                return new Visibility
                {
                    Distance = distanceInMeters,
                    Unit = "SM"
                };
            }
        }
        
        return null;
    }

    private int CalculateStatuteMilesToMeters(int whole, string fraction, bool isLessThan)
    {
        const int metersPerStatuteMile = 1609;
        double miles = whole;

        if (!string.IsNullOrEmpty(fraction) && fraction.Contains('/'))
        {
            var parts = fraction.TrimStart('/').Split('/');
            if (parts.Length == 2 && int.TryParse(parts[0], out int numerator) && int.TryParse(parts[1], out int denominator))
            {
                miles += (double)numerator / denominator;
            }
        }

        if (isLessThan)
        {
            miles -= 0.01; // Slightly less than the stated value
        }

        return (int)(miles * metersPerStatuteMile);
    }

    public ParsedMetarBuilder ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var visibility = TryParse(substringTokens) as Visibility;
        if (visibility != null)
        {
            builder.SetVisibility(visibility);
        }
        return builder;
    }
}