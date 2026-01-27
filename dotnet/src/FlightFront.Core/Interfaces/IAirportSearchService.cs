using FlightFront.Core.Models;
namespace FlightFront.Core.Interfaces;

public interface IAirportSearchService
{
    Task<IEnumerable<Airport>> SearchIcaoAsync(string query, CancellationToken cancellationToken = default);
    Task<IEnumerable<Airport>> SearchNameAsync(string query, CancellationToken cancellationToken = default);
    Task<IEnumerable<Airport>> SearchMuncipalityAsync(string query, CancellationToken cancellationToken = default);
}