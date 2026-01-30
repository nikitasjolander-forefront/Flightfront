using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public class MetarParserService
{

    private readonly MetarTrimmingService _trimmingService;
    private readonly Dictionary<TokenType, IParser> _parserMap;

    public MetarParserService(MetarTrimmingService trimmingService)
    {
        _trimmingService = trimmingService;

        // Map each TokenType to its corresponding parser
        _parserMap = new Dictionary<TokenType, IParser>
        {
            { TokenType.Wind, new WindParser() },
            /*{ TokenType.Visibility, new VisibilityParser() },
            { TokenType.Weather, new WeatherParser() },
            { TokenType.Clouds, new CloudsParser() },
            { TokenType.TemperatureDewpoint, new TemperatureParser() },
            { TokenType.Altimeter, new AltimeterParser() }*/            
        };
    }

    public ParsedMetar Parse(string metarString)
    {
        // Step 1: Get classified and grouped tokens
        var tokens = _trimmingService.TrimAndCleanMetar(metarString);

        var parsedMetarBuilder = new ParsedMetarBuilder();

        // Step 2: Process each token with its appropriate parser
        foreach (var token in tokens)
        {
            // Skip tokens we don't have parsers for yet
            if (!_parserMap.ContainsKey(token.Type))
            {
                Console.WriteLine($"No parser registered for {token.Type}: {token.substringTokens}");
                continue;
            }

            var parser = _parserMap[token.Type];

            parsedMetarBuilder = parser.ApplyParsedData(parsedMetarBuilder, token.substringTokens);
    }
     return parsedMetarBuilder.Build();
    }
}