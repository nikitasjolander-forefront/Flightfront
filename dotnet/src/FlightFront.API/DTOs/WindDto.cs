namespace FlightFront.API.DTOs;

public record WindDto
{
    public int? Direction { get; init; }
    public bool IsVariable { get; init; }
    public int Speed { get; init; }
    public int? Gust { get; init; }
    public string Unit { get; init; } = string.Empty;
    public int? VariationFrom { get; init; }
    public int? VariationTo { get; init; }
}
