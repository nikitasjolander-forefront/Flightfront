import {
  Box,
  Card,
  Typography,
  CardContent,
  CardHeader,
  Stack,
} from "@mui/material";
import "../../assets/weather-icons/css/weather-icons.min.css";
import { grey } from "@mui/material/colors";

const location = "Arlanda";
const time = "13:00";
const wind = "strong";
const visibility = "semi";
const temperature = "23 deg";
const qnh = "wtf";
const metar = "112 CB12 2323 CC NE12";

interface WeatherRowProps {
  label: string;
  value: string;
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

function WeatherCard() {
  return (
    <>
      <Card variant="outlined" sx={{ maxWidth: 700, p: 2 }}>
        <CardHeader title={`Weather at ${location}`} />
        <CardContent sx={{ display: "flex", p: 0 }}>
          <Box sx={{ justifyContent: "space-between", alignItems: "center", p: 2, fontSize: 64 }}>
            <i className="wi wi-rain"></i>
          </Box>
          <Stack direction="column" width="100%">
            <WeatherRow label="Time" value={time} />
            <WeatherRow label="Wind" value={wind} />
            <WeatherRow label="Visibility" value={visibility} />
            <WeatherRow label="Temperature" value={temperature} />
            <WeatherRow label="QNH" value={qnh} />
          </Stack>
        </CardContent>
      </Card>
    </>
  );
}

export default WeatherCard;
