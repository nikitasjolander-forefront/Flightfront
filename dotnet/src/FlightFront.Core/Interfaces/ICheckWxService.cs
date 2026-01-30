using FlightFront.Core.Models;

namespace Flightfront.Core.Interfaces;

public interface ICheckWxService
{

    Task<string?> GetMetar(string icaoCode, CancellationToken cancellationToken = default);
}