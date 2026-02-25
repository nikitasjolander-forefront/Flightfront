using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public class MetarParserService
{

    private readonly MetarTrimmingService _trimmingService;
    
    // Map each TokenType to its corresponding parser
    private static readonly Dictionary<TokenType, IParser> _parserMap = new()
    {           
        { TokenType.Wind, new WindParser() },
        { TokenType.Icao, new IcaoParser() },
        { TokenType.ObservationTime, new ObservationTimeParser()},
        { TokenType.Visibility, new VisibilityParser() },
        { TokenType.Weather, new WeatherParser() },
        { TokenType.Clouds, new CloudsParser() },
        { TokenType.Temperature, new TemperatureParser() }         
       // { TokenType.AirPressure, new AirPressureParser() }
    };

    public MetarParserService(MetarTrimmingService trimmingService)
    {
        _trimmingService = trimmingService;
    }


    public ParsedMetar Parse(string metarString)
    {
        if (string.IsNullOrWhiteSpace(metarString))
            return null;

        // Step 1: Get classified and grouped tokens
        var tokens = _trimmingService.TrimAndCleanMetar(metarString);

        var parsedMetarBuilder = new ParsedMetarBuilder();

         // Preserve the raw METAR string
        parsedMetarBuilder.SetRawMetar(metarString);

        // Step 2: Process each token with its appropriate parser
        foreach (var token in tokens)
        {
            // Skip tokens we don't have parsers for yet
            if (!_parserMap.ContainsKey(token.Type))
            {
                parsedMetarBuilder.AddParseError($"No parser for {token.Type}:{token.substringTokens}");
                continue;
            }

            var parser = _parserMap[token.Type];

            parser.ApplyParsedData(parsedMetarBuilder, token.substringTokens);
    }
     return parsedMetarBuilder.Build();
    }
}