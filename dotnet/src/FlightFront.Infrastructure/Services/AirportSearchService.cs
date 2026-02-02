using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Infrastructure.Services;

public class AirportSearchService : IAirportSearchService
{
    private static readonly HashSet<string> AllowedTypes = new() { "large_airport", "medium_airport" };
    private readonly Lazy<List<Airport>> _airports;

    public AirportSearchService(string csvFilePath)
    {
        _airports = new Lazy<List<Airport>>(() => LoadAirports(csvFilePath));
    }

    public Task<IEnumerable<Airport>> SearchIcaoAsync(string query, CancellationToken cancellationToken = default)
    {
        var results = _airports.Value
            .Where(a =>
                (!string.IsNullOrEmpty(a.IcaoCode) && a.IcaoCode.Contains(query, StringComparison.OrdinalIgnoreCase)));

        return Task.FromResult(results);
    }

    public Task<IEnumerable<Airport>> SearchNameAsync(string query, CancellationToken cancellationToken = default)
    {
        var results = _airports.Value
            .Where(a =>
                (!string.IsNullOrEmpty(a.Name) && a.Name.Contains(query, StringComparison.OrdinalIgnoreCase)));

        return Task.FromResult(results);
    }

    public Task<IEnumerable<Airport>> SearchMuncipalityAsync(string query, CancellationToken cancellationToken = default)
    {
        var results = _airports.Value
            .Where(a =>
                (!string.IsNullOrEmpty(a.Municipality) && a.Municipality.Contains(query, StringComparison.OrdinalIgnoreCase)));

        return Task.FromResult(results);
    }

    private static List<Airport> LoadAirports(string csvFilePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            BadDataFound = null
        };

        using var reader = new StreamReader(csvFilePath);
        using var csv = new CsvReader(reader, config);

        var airports = new List<Airport>();
        csv.Read();
        csv.ReadHeader();

        while (csv.Read())
        {
            var type = csv.GetField("type") ?? string.Empty;

            if (!AllowedTypes.Contains(type))
                continue;

            airports.Add(new Airport
            {
                Type = type,
                Name = csv.GetField("name") ?? string.Empty,
                Continent = csv.GetField("continent") ?? string.Empty,
                Municipality = csv.GetField("municipality") ?? string.Empty,
                IcaoCode = csv.GetField("icao_code") ?? string.Empty
            });
        }

        return airports;
    }
}