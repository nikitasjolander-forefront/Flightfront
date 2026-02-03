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
                {weather?.observationTime && (
                <WeatherRow
                  label="Observation Time"
                  value={new Date(weather.observationTime).toLocaleString()}
                />
              )}
              {weather?.wind?.direction != null && weather?.wind?.speed != null && (
                <WeatherRow
                  label="Wind"
                  value={`${weather.wind.direction}° at ${weather.wind.speed} ${weather.wind.unit || 'KT'}${weather.wind.gust ? ` gusting ${weather.wind.gust}` : ''}`}
                />
              )}
              {weather?.visibility != null && (
                <WeatherRow label="Visibility" value={weather.visibility} />
              )}
              {weather?.temperature != null && (
                <WeatherRow label="Temperature" value={`${weather.temperature}°C`} />
              )}
              {weather?.dewPoint != null && (
                <WeatherRow label="Dew Point" value={`${weather.dewPoint}°C`} />
              )}
              {weather?.qnh != null && <WeatherRow label="QNH" value={`${weather.qnh} hPa`} />}
              {weather?.clouds && weather.clouds.length > 0 && (
                <WeatherRow 
                  label="Clouds" 
                  value={weather.clouds.map(c => 
                    `${c.cloudCoverDescription || c.cloudCover} at ${c.cloudHeight}ft`
                  ).join(', ')} 
                />
              )}
            </Stack>
          </Box>
        </CardContent>
      </Card>
    </>
  );
}
