namespace FlightFront.Core.Models;

// Ändrat namn från Visibility till ParcedVisibility pga namnkonflikt (kanske tillfälligt?)
public record ParsedVisibility
{
    public int Distance { get; init; }  // in meters
    public string Unit { get; init; } = string.Empty;  // SM, M, KM
    public bool IsCavok { get; init; }  
}