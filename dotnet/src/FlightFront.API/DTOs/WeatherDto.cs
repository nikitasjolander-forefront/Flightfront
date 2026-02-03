namespace FlightFront.API.DTOs;

public record WeatherDto
{
    public string? Snow { get; init; }  
    public string? Rain { get; init; }  
    public string? Fog { get; init; }   
}
