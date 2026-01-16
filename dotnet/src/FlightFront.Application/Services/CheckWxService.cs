using System.Net.Http.Json;
using System.Text.Json;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

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

    public async Task<MetarData?> GetMetarAsync(string icaoCode, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.GetFromJsonAsync<MetarResponse>(
            $"metar/{icaoCode.ToUpperInvariant()}/decoded",
            JsonOptions,
            cancellationToken);

        return response?.Data?.FirstOrDefault();
    }
}