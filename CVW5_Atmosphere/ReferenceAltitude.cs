using System;

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

        public float GetTemperatureAtAltitude(float alt, bool relative = true)
        {
            float relAlt = relative ? alt : alt - Altitude;

            float TDiff = relAlt * TempLapseRate;

            return (Temp + TDiff);
        }

        public float GetPressureAtAltitude (float alt, bool relative = true)
        {
            float relAlt = relative ? alt : alt - Altitude;

            return (TempLapseRate == 0)
                ? ComputeExp_ZeroLapse(relAlt)
                : ComputeExp_NonZeroLapse(relAlt);
        }

        /// <summary>
        /// Used to compute the atmospheric pressure at a specific altitude when the temperature lapse rate is non-zero
        /// </summary>
        /// <param name="relativeAlt">Altitude relative to this block. Should be positive.</param>
        /// <returns>Air pressure, in Pascals (Pa)</returns>
        private float ComputeExp_NonZeroLapse (float relativeAlt)
        {
            float num = Temp + (relativeAlt * TempLapseRate);
            float exp_num = -Atmosphere.Gravity * Atmosphere.MolarMass;
            float exp_den = Atmosphere.GasConst * TempLapseRate;

            double raised = Math.Pow(num / Temp, exp_num / exp_den);

            return (float)(Pressure * raised);
        }

        /// <summary>
        /// Used to compute the atmospheric pressure at a specific altitude when the temperature lapse rate is zero
        /// </summary>
        /// <param name="relativeAlt">Altitude relative to this block. Should be positive.</param>
        /// <returns>Air pressure, in Pascals (Pa)</returns>
        private float ComputeExp_ZeroLapse (float relativeAlt)
        {
            float exp_num = -Atmosphere.Gravity * Atmosphere.MolarMass * relativeAlt;
            float exp_den = Atmosphere.GasConst * Temp;

            double raised = Math.Pow(Math.E, exp_num / exp_den);

            return (float)(Pressure * raised);
        }
    }
}