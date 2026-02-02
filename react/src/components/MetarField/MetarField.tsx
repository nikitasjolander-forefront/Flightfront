import { Box, Button, TextField } from "@mui/material";
import type React from "react";
import { useEffect, useState } from "react";

interface MetarProps {
  metarRaw: string;
  onSubmit: (params: {type: string, value: string}) => void; 
}

export function MetarField({ metarRaw, onSubmit }: MetarProps) {
  const [metar, setMetar] = useState(metarRaw || "");

  useEffect(() => {
    setMetar(metarRaw)
  }, [metarRaw]);

  const handelChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setMetar(e.target.value);
    console.log(e, "send to backend");
  };

const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSubmit({type: 'metar', value: metar});
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
        onSubmit = { handleSubmit }
      >
        <TextField
          id="metar-input"
          label="METAR"
          variant="outlined"
          fullWidth
          value={metar}
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
