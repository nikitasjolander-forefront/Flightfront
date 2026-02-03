import type { Weather } from "../services/weatherServices";
 
export function getWeatherIcon(weatherPhenomena?: Weather['weatherPhenomena']): string {
  if (!weatherPhenomena) {
    return "wi-day-sunny";
  }
 
  if (weatherPhenomena.fog) {
    return weatherPhenomena.fog === "BR" ? "wi-fog" : "wi-fog";
  }
 
  if (weatherPhenomena.snow) {
    return weatherPhenomena.snow === "-SN" ? "wi-snow" : "wi-snowflake-cold";
  }
 
  if (weatherPhenomena.rain) {
    return weatherPhenomena.rain === "-RA" ? "wi-sprinkle" : "wi-rain";
  }
 
  return "wi-day-sunny";
}