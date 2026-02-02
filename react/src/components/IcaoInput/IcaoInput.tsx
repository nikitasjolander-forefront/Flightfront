import { Box, Button, TextField } from "@mui/material";
import type React from "react";
import { useEffect, useState } from "react";

interface IcaoProps {
  onSubmit: (params: { type: string; value: string }) => void;
  icaoRaw: string;
}

function IcaoInput({icaoRaw, onSubmit }: IcaoProps) {
  const [icao, setIcao] = useState("");

  const handelChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setIcao(e.target.value);
  };

  useEffect(() => {
    setIcao(icaoRaw);
  }, [icaoRaw]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit({ type: "icao", value: icao });

    //TODO: send to backend
  };

  return (
    <>
      <Box
        component="form"
        sx={{
          display: "flex",
          alignItems: "strech",
          gap: 2,
        }}
        onSubmit={handleSubmit}
      >
        <TextField
          id="icao-input"
          label="ICAO"
          variant="outlined"
          fullWidth
          value={icao}
          onChange={handelChange}
        />
        <Button
          variant="contained"
          type="submit"
          sx={{ whiteSpace: "nowrap", minWidth: "150px" }}
        >
          Search
        </Button>
      </Box>
    </>
  );
}

export default IcaoInput;
