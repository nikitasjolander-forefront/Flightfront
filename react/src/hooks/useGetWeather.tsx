import { useQuery } from "@tanstack/react-query";
import { getWeatherByIcao } from "../services/weatherServices";

export const useGetWather = (icao: string) => {
  return useQuery({
    queryKey: ["weather", icao],
    queryFn: () => getWeatherByIcao(icao),
    staleTime: 1000,
  });
};
