import axios from "axios";

export type WeatherAll = {
  rawMetar: string;
  icao: string;
  observationTime?: string;
  wind?: Wind;
  clouds?: Clouds[];
  parseErrors?: string[];
  visibility?: Visibility;
  temperature?: Temperature;
  dewPoint?: number;
  qnh?: number;
  weather?: Weather;
  airport?: Airport;
};

type Wind = {
  direction?: number;
  isVariable?: boolean;
  speed?: number;
  gust?: number | null;
  unit?: string;
  variationFrom?: number | null;
  variationTo?: number | null;
};

type Clouds = {
  cloudCover?: string;
  cloudCoverDescription?: string;
  cloudHeight?: number;
  modifier?: string | null;
};

type Weather = {
  snow?: "SN" | "-SN";
  rain?: "RA" | "-RA";
  fog?: "FG" | "BR";
};

type Temperature = {
  degree?: number;
  dewpoint?: number;
};

type Airport ={
  type? : string;
  name? : string;
  continent? : string;
  municipality? : string;
  icao?: string;
} 

type Visibility = {
  distance?: number;
  unit?: string;
  isCavok?: boolean;
};
export async function getWeatherByIcao(icao: string): Promise<WeatherAll> {
  const res = await axios.get<WeatherAll>(`https://localhost:7168/api/Metar/${encodeURIComponent(icao)}`);
  return res.data;
}

export async function getWeatherByMetar(metar: string): Promise<WeatherAll> {
 const res = await axios.get<WeatherAll>(`https://localhost:7168/${encodeURIComponent(metar)}`);
  return res.data;
}
