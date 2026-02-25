import {
  Box,
  Card,
  Typography,
  CardContent,
  CardHeader,
  Stack,
} from "@mui/material";
import "../../assets/weather-icons/css/weather-icons.min.css";
import type { WeatherAll } from "../../services/weatherServices";
import { getWeatherIcon } from "../../utils/weatherIconMapper";

interface WeatherRowProps {
  label: string;
  value: string | number;
}

interface WeatherProps {
  weather: WeatherAll;
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

export function WeatherCard({weather: weatherAll}: WeatherProps) {
  const weatherIconClass = getWeatherIcon(weatherAll.weather);
  return (
    <>
      <Card variant="outlined" sx={{ p: 2 }}>
        <CardHeader title={`Weather at ${weatherAll.airport?.name || weatherAll.icao}`} />
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
                {weatherAll?.observationTime && (
                <WeatherRow
                  label="Observation Time"
                  value={new Date(weatherAll.observationTime).toLocaleString()}
                />
              )}
              {weatherAll?.wind?.direction != null && weatherAll?.wind?.speed != null && (
                <WeatherRow
                  label="Wind"
                  value={`${weatherAll.wind.direction}° at ${weatherAll.wind.speed} ${weatherAll.wind.unit || 'KT'}${weatherAll.wind.gust ? ` gusting ${weatherAll.wind.gust}` : ''}`}
                />
              )}
              {weatherAll?.visibility != null && (
                <WeatherRow label="Visibility" value={`${weatherAll.visibility.distance} ${weatherAll.visibility.unit || ''}${weatherAll.visibility.isCavok ? ' (CAVOK)' : ''}`} />
              )}
              {weatherAll?.temperature?.degree != null && (
                <WeatherRow label="Temperature" value={`${weatherAll.temperature.degree}°C`} />
              )}
              {weatherAll?.temperature?.dewpoint != null && (
                <WeatherRow label="Dew Point" value={`${weatherAll.temperature.dewpoint}°C`} />
              )}
              {weatherAll?.qnh != null && <WeatherRow label="QNH" value={`${weatherAll.qnh} hPa`} />}
              {weatherAll?.clouds && weatherAll.clouds.length > 0 && (
                <WeatherRow 
                  label="Clouds" 
                  value={weatherAll.clouds.map(c => 
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
