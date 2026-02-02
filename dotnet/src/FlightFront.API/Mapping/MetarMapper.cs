using FlightFront.Core.Models;
using FlightFront.API.DTOs;

namespace FlightFront.API.Mapping;

public static class MetarMapper
{

    
    public static ParsedMetarDto ToDto(this ParsedMetar parsedMetar)
    {
        return new ParsedMetarDto
        {
            Icao = parsedMetar.Icao,
            ObservationTime = parsedMetar.ObservationTime,
            Wind = parsedMetar.Wind?.ToDto(),
            // Visibility = parsedMetar.Visibility?.ToDto(),
            // Weather = parsedMetar.Weather?.ToDto(),
            Clouds = parsedMetar.Clouds.Select(c => c.ToDto()).ToList(), 
            // Temperature = parsedMetar.Temperature?.ToDto(),
            // AirPressure = parsedMetar.AirPressure?.ToDto(),
            ParseErrors = parsedMetar.ParseErrors
        };
    }

    public static WindDto ToDto(this Wind wind)
    {
        return new WindDto
        {
            Direction = wind.Direction,
            IsVariable = wind.IsVariable,
            Speed = wind.Speed,
            Gust = wind.Gust,
            Unit = wind.Unit,
            VariationFrom = wind.VariationFrom,
            VariationTo = wind.VariationTo
        };
    }

    /*
       public static VisibilityDto ToDto(this Visibility visibility)
       {
           return new VisibilityDto
           {

           };
       }
    */
    /*
       public static WeatherDto ToDto(this Weather weather)
       {
           return new WeatherDto
           {

           };
       }
    */

    public static CloudsDto ToDto(this Clouds clouds)
    {
        return new CloudsDto
        {
                CloudCover = clouds.CloudCover,
                CloudHeight = clouds.CloudHeight,
                Modifier = clouds.Modifier
        };
    }

    /*
       public static TemperatureDto ToDto(this Temperature temperature)
       {
           return new TemperatureDto
           {

           };
       }

    */
    /*
       public static AirPressureDto ToDto(this AirPressure airPressure)
       {
           return new AirPressureDto
           {

           };
       }
       */


}