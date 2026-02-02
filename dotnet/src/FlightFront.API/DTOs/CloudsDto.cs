namespace FlightFront.API.DTOs;

public record CloudsDto
{
    public string? CloudCoverDescription { get; init; }
    
    public int CloudHeight { get; init; }
    
    public string? ModifierDescription { get; init; }
}