import {
  Box,
  Card,
  Typography,
  CardContent,
  CardHeader,
  Stack,
} from "@mui/material";
import "../../assets/weather-icons/css/weather-icons.min.css";
import type { Weather } from "../../services/weatherServices";
import { getWeatherIcon } from "../../utils/weatherIconMapper";
// const location = "Arlanda";
// const time = "13:00";
// const wind = "strong";
// const visibility = "semi";
// const temperature = "23 deg";
// const qnh = "wtf";

interface WeatherRowProps {
  label: string;
  value: string | number;
}

interface WeatherProps {
  weather: Weather;
}

function WeatherRow({ label, value }: WeatherRowProps) {
  return (
    <Stack
      direction="row"
      sx={{ justifyContent: "space-between", alignItems: "center", p: 2 }}
      borderBottom={1}
      borderColor="grey.500"
    >
      <Typography variant="h6">{label}</Typography>
      <Typography variant="h6">{value}</Typography>
    </Stack>
  );
}

export function WeatherCard({weather}: WeatherProps) {
  const weatherIconClass = getWeatherIcon(weather.weatherPhenomena);
  return (
    <>
      <Card variant="outlined" sx={{ p: 2 }}>
        <CardHeader title={`Weather at ${weather.location}`} />
        <CardContent>
          <Box sx={{ display: "flex", p: 0 }}>
            <Box
              sx={{
                justifyContent: "space-between",
                alignItems: "center",
                p: 2,
                fontSize: 150,
              }}
            >
              <i className={`wi ${weatherIconClass}`}></i>
            </Box>
            <Stack direction="column" width="100%">
                            {/*TODO: only show if exist*/}
              <WeatherRow label="Time" value={`${weather.time.day} ${weather.time.time}`} />
              <WeatherRow label="Wind" value={`Direction ${weather.wind.direction} and speed ${weather.wind.speed} knot`}/>
              <WeatherRow label="Visibility" value={weather.visibility} />
              <WeatherRow label="Temperature" value={weather.temperature} />
              <WeatherRow label="QNH" value={weather.qnh} />
            </Stack>
          </Box>
        </CardContent>
      </Card>
    </>
  );
}
