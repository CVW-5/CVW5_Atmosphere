namespace CVW5_Atmosphere
{
    public struct ReferenceAltitude
    {
        public float Altitude, Pressure, Temp, TempLapseRate;

        public ReferenceAltitude(float alt, float pres, float temp, float lapse)
        {
            Altitude = alt;
            Pressure = pres;
            Temp = temp;
            TempLapseRate = lapse;
        }
    }
}