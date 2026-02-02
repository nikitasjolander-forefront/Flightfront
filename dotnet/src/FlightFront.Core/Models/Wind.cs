namespace FlightFront.Core.Models;

public record Wind
{
    public int? Direction { get; init; }  // null for VRB (variable)
    public bool IsVariable { get; init; }
    public int Speed { get; init; }
    public int? Gust { get; init; }
    public string Unit { get; init; } = string.Empty;  // KT, MPS, MPH
    public int? VariationFrom { get; init; }
    public int? VariationTo { get; init; }
}