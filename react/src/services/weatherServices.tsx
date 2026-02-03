// import axios from "axios";

export type Weather = {
  metarRaw: string;
  icao: string;
  observationTime?: string;
  wind?: Wind;
  clouds?: Clouds[];
  parseErrors?: string[];
  location?: string;
  visibility?: string | number;
  temperature?: number;
  dewPoint?: number;
  qnh?: number;
  weatherPhenomena?: WeatherPhenomena;
};

type Wind = {
  direction: number;
  speed: number;
};

type DayAndTime = {
  day: number;
  time: number;
};

type Clouds = {
  base: number;
  type: CloudType;
};

type CloudType = "few" | "sct" | "bkn" | "ovc";

const mockWeather: Weather = {
  location: "Arlanda",
  time: { day: 28, time: 925 },
  wind: { direction: 210, speed: 9 },
  visibility: 5000,
  temperature: 2,
  dewPoint: -1,
  qnh: 1001,
  clouds: [
    { base: 700, type: "few" },
    { base: 1400, type: "bkn" },
  ],
  metar:
    "METAR EHLE 280925Z AUTO 21009G19KT 5000 -RA FEW007 BKN014CB 02/M01 Q1001",
  icao: 'EHLE'
};

type WeatherPhenomena = {
  snow?: "SN" | "-SN";
  rain?: "RA" | "-RA";
  fog?: "FG" | "BR";
};

export async function getWeatherByIcao(icao: string): Promise<Weather> {
  //const res = await axios.get<Weather>(`https://localhost:7168/api/Metar/${encodeURIComponent(icao)}`);
  console.log(icao);
  const res = { data: mockWeather };
  return res.data;
}

export async function getWeatherByMetar(metar: string): Promise<Weather> {
 // const res = await axios.get<Weather>(`https://localhost:7168/${encodeURIComponent(metar)}`);
 console.log(metar);
  const res = { data: mockWeather };
  return res.data;
}
