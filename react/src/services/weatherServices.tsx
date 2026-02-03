import axios from "axios";

export type Weather = {
  metarRaw?: string;
  icao: string;
  observationTime?: string;
  wind?: Wind;
  clouds?: Clouds[];
  parseErrors?: string[];
  location?: string;
  visibility?: string | number;
  temperature?: Temperature;
  dewPoint?: number;
  qnh?: number;
  metar?: string;
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
  type Temperature = {
    degree : number;
    dewpoint : number;
  };

export async function getWeatherByIcao(icao: string): Promise<Weather> {
  const res = await axios.get<Weather>(`https://localhost:7168/api/Metar/${encodeURIComponent(icao)}`);
  return res.data;
}

export async function getWeatherByMetar(metar: string): Promise<Weather> {
  const res = await axios.get<Weather>(`https://localhost:7168/${encodeURIComponent(metar)}`);
  return res.data;
}
