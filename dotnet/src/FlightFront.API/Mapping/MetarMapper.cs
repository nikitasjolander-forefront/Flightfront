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
			Visibility = parsedMetar.Visibility?.ToDto(),
			Weather = parsedMetar.Weathers?.ToDto(),
			Clouds = parsedMetar.Clouds?.ToDto(),
			Temperature = parsedMetar.Temperature?.ToDto(),
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

	public static string ToDto(this Visibility visibility)
	{
		if (visibility.IsCavok)
			return "CAVOK";

		return visibility.Distance.ToString("D4");
	}

	public static WeatherDto? ToDto(this List<Weather> weathers)
	{
		if (weathers == null || !weathers.Any())
			return null;

		string? snow = null;
		string? rain = null;
		string? fog = null;

		foreach (var weather in weathers)
		{
			foreach (var phenomenon in weather.Phenomena)
			{
				var value = weather.Intensity + phenomenon;

				switch (phenomenon)
				{
					case "SN":
						snow = value;
						break;
					case "RA":
						rain = value;
						break;
					case "FG":
					case "BR":
						fog = value;
						break;
				}
			}
		}

		return new WeatherDto
		{
			Snow = snow,
			Rain = rain,
			Fog = fog
		};
	}
  
  public static CloudsDto ToDto(this Clouds clouds)
  {
      return new CloudsDto
      {
              CloudCover = clouds.CloudCover,
              CloudHeight = clouds.CloudHeight,
              Modifier = clouds.Modifier
      };
  }


  public static TemperatureDto ToDto(this Temperature temperature)
  {
      return new TemperatureDto
      {
          Degree = temperature.Degree,
          Dewpoint = temperature.Dewpoint
      };
  }
	/*
	   public static AirPressureDto ToDto(this AirPressure airPressure)
	   {
		   return new AirPressureDto
		   {

		   };
	   }
	   */


}