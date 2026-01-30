using FlightFront.Core.Models;

namespace Flightfront.Core.Interfaces;

public interface ICheckWxService
{
    //Task<MetarDataDecoded?> GetMetarAsync(string icaoCode, CancellationToken cancellationToken = default);

    Task<string?> GetMetar(string icaoCode, CancellationToken cancellationToken = default);
}