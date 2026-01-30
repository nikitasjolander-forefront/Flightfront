using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public class ObservationTimeParser : IParser
{
    public object? TryParse(string[] substringTokens)
    {
        if (substringTokens == null || substringTokens.Length == 0)
            return null;

        var timeString = substringTokens[0];

        // METAR time format is DDHHMMZ (Day, Hour, Minute, Zulu)
        // Example: 301425Z means 30th day at 14:25 UTC
        if (timeString.Length != 7 || !timeString.EndsWith("Z"))
            return null;

        if (!int.TryParse(timeString[..2], out int day) ||
            !int.TryParse(timeString.Substring(2, 2), out int hour) ||
            !int.TryParse(timeString.Substring(4, 2), out int minute))
            return null;

        // Validate ranges
        if (day < 1 || day > 31 || hour < 0 || hour > 23 || minute < 0 || minute > 59)
            return null;

        try
        {
            var now = DateTime.UtcNow;
            var observationTime = new DateTime(now.Year, now.Month, day, hour, minute, 0, DateTimeKind.Utc);

            // Handle month boundary: if observation day is in future, it's from previous month
            if (observationTime > now.AddDays(1))
            {
                observationTime = observationTime.AddMonths(-1);
            }

            return observationTime;
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }
    }

    public void ApplyParsedData(ParsedMetarBuilder builder, string[] substringTokens)
    {
        var observationTime = TryParse(substringTokens) as DateTime?;
        if (observationTime.HasValue)
        {
            builder.SetObservationTime(observationTime.Value);
        }
    }
}
