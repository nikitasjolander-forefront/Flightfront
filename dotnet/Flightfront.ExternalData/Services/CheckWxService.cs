using System.Net.Http.Json;
using System.Text.Json;
using Flightfront.Core.Interfaces;
using FlightFront.Core.Models;

namespace Flightfront.ExternalData.Services;

public class CheckWxService : ICheckWxService
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    public CheckWxService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MetarDataDecoded?> GetMetarAsync(string icaoCode, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<MetarResponseDecoded>(
            $"metar/{icaoCode.ToUpperInvariant()}/decoded",
            JsonOptions,
            cancellationToken);

        return response?.Data?.FirstOrDefault();
    }

    public async Task<string?> GetMetar(string icaoCode, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<MetarResponse>(
            $"metar/{icaoCode.ToUpperInvariant()}",
            JsonOptions,
            cancellationToken);

        return response?.Data?.FirstOrDefault();
    }
}