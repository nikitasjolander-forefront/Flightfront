import type { WeatherAll } from "../services/weatherServices";
 
export function getWeatherIcon(weather?: WeatherAll['weather']): string {
  if (!weather) {
    return "wi-day-sunny";
  }
 
  if (weather.fog) {
    return weather.fog === "BR" ? "wi-fog" : "wi-fog";
  }
 
  if (weather.snow) {
    return weather.snow === "-SN" ? "wi-snow" : "wi-snowflake-cold";
  }
 
  if (weather.rain) {
    return weather.rain === "-RA" ? "wi-sprinkle" : "wi-rain";
  }
 
  return "wi-day-sunny";
}