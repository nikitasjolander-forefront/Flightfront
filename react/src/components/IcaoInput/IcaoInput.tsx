import { Box, Button, TextField } from "@mui/material";
import type React from "react";

function IcaoInput() {
  const handleSubmit =(e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    console.log("submited");
  }


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
        <TextField id="icao-input" label="ICAO" variant="outlined" fullWidth />
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
