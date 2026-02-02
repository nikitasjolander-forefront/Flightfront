namespace FlightFront.Core.Models;

public readonly record struct MetarToken(TokenType Type, string[] substringTokens);