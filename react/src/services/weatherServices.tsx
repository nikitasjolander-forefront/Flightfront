import axios from "axios";

export type Weather = {
  location?: string | undefined;
  time?: DayAndTime| undefined;
  wind?: Wind| undefined;
  visibility?: string | number| undefined;
  temperature?: number| undefined;
  dewPoint?: number| undefined;
  qnh?: number| undefined;
  clouds?: Clouds[]| undefined;
  metar?: string| undefined;
  icao?: string| undefined;
};

type Wind = {
  direction?: number| undefined;
  speed?: number| undefined;
};

type DayAndTime = {
  day?: number| undefined;
  time?: number| undefined;
};

type Clouds = {
  base?: number| undefined;
  type?: CloudType| undefined;
};

type CloudType = "few" | "sct" | "bkn" | "ovc"| undefined;
// const mockWeather: Weather = {
//   location: "Arlanda",
//   time: { day: 28, time: 925 },
//   wind: { direction: 210, speed: 9 },
//   visibility: 5000,
//   temperature: 2,
//   dewPoint: -1,
//   qnh: 1001,
//   clouds: [
//     { base: 700, type: "few" },
//     { base: 1400, type: "bkn" },
//   ],
//   metar:
//     "METAR EHLE 280925Z AUTO 21009G19KT 5000 -RA FEW007 BKN014CB 02/M01 Q1001",
//   icao: 'EHLE'
// };

export async function getWeatherByIcao(icao: string): Promise<Weather> {
  const res = await axios.get<Weather>(`https://localhost:7168/api/Metar/${encodeURIComponent(icao)}`)
  // console.log(icao);
  // const res = { data: mockWeather };
  return res.data;
}

export async function getWeatherByMetar(metar: string): Promise<Weather> {
  const res = await axios.get<Weather>(`https://localhost:7168/${encodeURIComponent(metar)}`);
  // console.log(metar);
  // const res = { data: mockWeather };
  return res.data;
}
