# CVW5_Atmosphere
This library provides atmospheric effects like density with altitude, pressure, and so on, to CVW5 and its associated projects.

### Current Features:
- Importing data from .csv files (see "US Standard Atmosphere.csv" for samples)
- Compute Temperature, Pressure, Density based on altitude

### Usage:
- Import the plugin to your Unity (or whatever) project
- Create an instance of CVW5_Atmosphere.Atmosphere by calling Atmosphere.FromCSV(filename)
- Call GetTemperature/GetPressure/GetDensity on the Atmosphere object as needed

### Theory:
[Data from the Wikipedia page for the US Standard Atmosphere](https://en.wikipedia.org/wiki/U.S._Standard_Atmosphere)

[Equations derived from the Barometric Formulas for Pressure](https://en.wikipedia.org/wiki/Barometric_formula)