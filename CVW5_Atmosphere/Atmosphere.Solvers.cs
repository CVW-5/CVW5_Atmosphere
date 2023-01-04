namespace CVW5_Atmosphere
{
    public partial class Atmosphere
    {
        public float GetDensity (float altitude)
        {
            ReferenceAltitude ra = GetAltitudeBlock(altitude);

            float pressure = ra.GetPressureAtAltitude(altitude, false);
            float temp = ra.GetTemperatureAtAltitude(altitude, false);

            return pressure / (GasConst * temp);
        }

        public float GetPressure (float altitude)
        {
            ReferenceAltitude ra = GetAltitudeBlock(altitude);

            return ra.GetPressureAtAltitude(altitude, false);
        }

        public float GetTemperature (float altitude)
        {
            ReferenceAltitude ra = GetAltitudeBlock(altitude);

            return ra.GetTemperatureAtAltitude(altitude, false);
        }

        private ReferenceAltitude GetAltitudeBlock (float altitude)
        {
            ReferenceAltitude low = RefAltitudes[0];

            // Below sea level - extrapolate downwards, theoretically anyway. There's water there.
            if(altitude <= 0)
            {
                return low;
            }

            // Iterate through to find the last block below the specified altitude
            foreach(var ra in RefAltitudes)
            {
                if(ra.Altitude > altitude)
                {
                    break;
                }

                low = ra;
            }

            // Return the last block below the specified altitude
            return low;
        }
    }
}