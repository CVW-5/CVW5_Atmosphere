namespace CVW5_Atmosphere
{
    internal struct ReferenceAltitude
    {
        internal float Altitude, Pressure, Temp, TempLapseRate;

        internal ReferenceAltitude(float alt, float pres, float temp, float lapse)
        {
            Altitude = alt;
            Pressure = pres;
            Temp = temp;
            TempLapseRate = lapse;
        }
    }
}