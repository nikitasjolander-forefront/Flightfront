import "./App.css";
import IcaoInput from "./components/IcaoInput/IcaoInput";
import WeatherCard from "./components/WatherCard/WeatherCard";

function App() {
  return (
    <>
      <h1>Flightfront Weather Report</h1>
      <IcaoInput />
      <WeatherCard />
    </>
  );
}

export default App;
