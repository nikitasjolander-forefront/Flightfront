using FlightFront.Core.Interfaces;
using FlightFront.Core.Models;

namespace FlightFront.Application.Services;

public class MetarParserService
{

    private readonly MetarTrimmingService _trimmingService;
    private readonly List<IParser> _parsers;

    public MetarParserService(MetarTrimmingService trimmingService)
    {
        _trimmingService = trimmingService;


        // Map each TokenType to its corresponding parser
        _parserMap = new Dictionary<TokenType, IParser>
        {
            { TokenType.Wind, new WindParser() },
            // { TokenType.Visibility, new VisibilityParser() },
            // { TokenType.Weather, new WeatherParser() },
            // { TokenType.Clouds, new CloudsParser() },
            // { TokenType.TemperatureDewpoint, new TemperatureDewpointParser() },
            // { TokenType.Altimeter, new AltimeterParser() },
            // Other parsers...
        };
    }

    public ParsedMetar Parse(string metarString)
    {
        // Step 1: Get classified and grouped tokens
        var tokens = _trimmingService.TrimAndCleanMetar(metarString);
        
        var parsedMetar = new ParsedMetar();

        // Step 2: Process each token with its appropriate parser
        foreach (var token in tokens)
        {
            // Skip tokens we don't have parsers for yet
            if (!_parserMap.ContainsKey(token.Type))
            {
                Console.WriteLine($"No parser registered for {token.Type}: {token.RawText}");
                continue;
            }

            var parser = _parserMap[token.Type];
            var result = parser.TryParse(token.RawText);

            if (result != null)
            {
                // Map parsed results to ParsedMetar properties
                switch (token.Type)
                {
                    case TokenType.Wind:
                        parsedMetar.Wind = (Wind)result;
                        break;                    
                   
                     case TokenType.Visibility:
                    //     parsedMetar.Visibility = (Visibility)result;
                         break;
                    
                     case TokenType.Weather:
                    //     parsedMetar.Weather = (Weather)result;
                         break;
                    
                    case TokenType.Clouds:
                    //     parsedMetar.Clouds = (Cloud)result;
                         break;

                    case TokenType.TemperatureDewpoint:
                    //     parsedMetar.Temperature = (Temperature)result;
                            break;
                            
                    case TokenType.Altimeter:
                    //     parsedMetar.Altimeter = (Altimeter)result;
                            break;


                    // ... etc
                }
            }
            else
            {
                Console.WriteLine($"Failed to parse {token.Type}: {token.RawText}");
            }
        }

        return parsedMetar;
    }
}