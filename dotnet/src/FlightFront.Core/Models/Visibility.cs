namespace FlightFront.Core.Models;

public record Visibility
{
	public int Distance { get; init; }
	public string Unit { get; init; } = string.Empty;
	public bool IsCavok { get; init; }
}