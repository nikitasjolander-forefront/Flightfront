import { Box, Button, TextField } from "@mui/material";
import type React from "react";
import { useState } from "react";

function IcaoInput() {
  const [icao, setIcao] = useState("");

  const handelChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setIcao(e.target.value);
  };

  const handleSubmit = (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log(icao);
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
          onChange={handelChange}
        />
        <Button
          variant="contained"
          type="submit"
          sx={{ whiteSpace: "nowrap", minWidth: "150px" }}
        >
          Submit
        </Button>
      </Box>
    </>
  );
}

export default IcaoInput;
