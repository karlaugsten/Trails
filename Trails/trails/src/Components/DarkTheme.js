import React from 'react';
import { ThemeProvider } from 'styled-components';

const darkTheme = {
  background: "#424242",
  aboveBackground: "#313131",
  text: "#E9E8E8",
  subText: "#BEBEBE",
  subSubText: "#929292",
  errorText: "#ff6666",
  successText: "#66ff66"
};

const lightTheme = {
  background: "#F6F4FD",
  aboveBackground: "#E7E4F1",
  text: "#1A1A1A",
  subText: "#1C1C1C",
  subSubText: "#1F1F1F",
  errorText: "#b30000",
  successText: "00b300"
};

export default ({children}) => <ThemeProvider theme={darkTheme}>{children}</ThemeProvider>