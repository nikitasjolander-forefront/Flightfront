using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;
using FlightFront.Core.Models.FlightFront.Core.Models;
using System.Text.RegularExpressions;

namespace FlightFront.Application.Services;

public class VisibilityParser : IParser
{
    private static readonly Regex MetricVisibilityPattern = new(@"^([0-9]{4})$");
    private static readonly Regex StatuteMilesPattern = new(@"^(\d+(/\d+)?)?SM$");
	private static readonly Regex CavokPattern = new(@"^CAVOK$", RegexOptions.IgnoreCase);

	public object? TryParse(string[] substringTokens)
    {
        if (substringTokens == null || substringTokens.Length == 0)
            return null;

        foreach (var token in substringTokens)
        {
            if (string.IsNullOrWhiteSpace(token))
                continue;

			if (CavokPattern.IsMatch(token))
			{
                return new ParsedVisibility
				{
                    Distance = 10000,
                    Unit = "M",
                    IsCavok = true
                };
            }

            var metricMatch = MetricVisibilityPattern.Match(token);
            if (metricMatch.Success)
            {
                var distance = int.Parse(metricMatch.Groups[1].Value);
                return new ParsedVisibility
				{
                    Distance = distance,
                    Unit = "M",
                    IsCavok = false
                };
            }

            var milesMatch = StatuteMilesPattern.Match(token);
            if (milesMatch.Success)
            {
                var milesText = milesMatch.Groups[1].Value;
                double miles;

                if (string.IsNullOrEmpty(milesText))
                {
                    miles = 0;
                }
                else if (milesText.Contains('/'))
                {
                    var parts = milesText.Split('/');
                    miles = double.Parse(parts[0]) / double.Parse(parts[1]);
                }
                else
                {
                    miles = double.Parse(milesText);
                }

                var distanceInMeters = (int)(miles * 1609);
                return new ParsedVisibility
				{
                    Distance = distanceInMeters,
                    Unit = "SM",
                    IsCavok = false
                };
            }
        }

        return null;
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