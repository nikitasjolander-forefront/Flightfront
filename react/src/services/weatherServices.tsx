import axios from "axios";

type Weather = {
  location: string;
  time: string;
  wind: string;
  visibility: string;
  temperature: string;
  qnh: string;
  metar: string;
};

export async function getWeatherByIcao(icao: string): Promise<Weather> {
  const res = await axios.get<Weather>(`URL/${icao}`);
  return res.data
}
