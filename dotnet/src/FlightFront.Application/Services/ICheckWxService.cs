using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public interface ICheckWxService
{
    Task<MetarData?> GetMetarAsync(string icaoCode, CancellationToken cancellationToken = default);
}