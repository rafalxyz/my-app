// In theme.js
import { defaultTheme } from 'react-admin';
import createTheme from '@mui/material/styles/createTheme';
import createPalette from '@mui/material/styles/createPalette';

const palette = createPalette({
  ...defaultTheme.palette,
  primary: {
    main: '#3f51b5',
  },
  secondary: {
    main: '#f44336',
  },
});

const rawTheme = {
  palette,
};

export const theme = createTheme({
  ...defaultTheme,
  ...rawTheme,
});
