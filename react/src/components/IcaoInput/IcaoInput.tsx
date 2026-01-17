import { Box, Button, TextField } from "@mui/material";

function IcaoInput() {
  return (
    <>
      <Box component="form">
        <TextField id="filled-basic" label="required" variant="filled" />
        <Button  variant="contained" type="submit">Submit</Button>
      </Box>
    </>
  );
}

export default IcaoInput;
