import { Box, CircularProgress, Stack, Typography } from "@mui/material";
import "./App.css";
import IcaoInput from "./components/IcaoInput/IcaoInput";
import { WeatherCard } from "./components/WatherCard/WeatherCard";
import { MetarField } from "./components/MetarField/MetarField";
// import { useGetWather } from "./hooks/useGetWeather";
import { useMutation } from "@tanstack/react-query";
import {
  getWeatherByIcao,
  getWeatherByMetar,
  type Weather,
} from "./services/weatherServices";

function App() {
  const weatherMutation = useMutation<
    Weather,
    Error,
    { type: string; value: string }
  >({
    mutationFn: async ({ type, value }) => {
      return type === "icao"
        ? await getWeatherByIcao(value)
        : await getWeatherByMetar(value);
    },
  });

  return (
    <>
      <h1>Flightfront Weather Report</h1>
      <Stack spacing={2}>
        <IcaoInput
          onSubmit={weatherMutation.mutate}
          icaoRaw={weatherMutation.data ? weatherMutation.data.icao : ""}
        />
        <MetarField
          metarRaw={weatherMutation.data ? weatherMutation.data.metarRaw : ""}
          onSubmit={weatherMutation.mutate}
        />

        {weatherMutation.isPending ? (
          <Box sx={{ display: "flex" }}>
            <CircularProgress />
          </Box>
        ) : (
          <>
            {weatherMutation.error ? (
              <Typography>Error loading weather..</Typography>
            ) : null}
            {weatherMutation.isSuccess ? (
              <WeatherCard weather={weatherMutation.data} />
            ) : null}
          </>
        )}
      </Stack>
    </>
  );
}

export default App;
