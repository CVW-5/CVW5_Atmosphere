using System.Collections.Generic;
using System.IO;
using System;

namespace CVW5_Atmosphere
{
    public partial class Atmosphere
    {
        public static Atmosphere US_Standard => new Atmosphere()
        {
            RefAltitudes = new ReferenceAltitude[]
            {
                new ReferenceAltitude(0, 101325, 288.15f, -0.0065f),
                new ReferenceAltitude(11000, 22632.1f, 216.65f, 0),
                new ReferenceAltitude(20000, 5474.89f, 216.65f, 0.001f),
                new ReferenceAltitude(32000,868.019f,228.65f,0.0028f ),
                new ReferenceAltitude(47000,110.906f,270.65f,0),
                new ReferenceAltitude(51000,66.9389f,270.65f,-0.0028f),
                new ReferenceAltitude(71000,3.95642f,214.65f,-0.002f),
                new ReferenceAltitude(84852,0,187.15f,0)
            },
            Year = 1976,
            AtmosphereType = "US Standard Atmosphere",
        };

        public static Atmosphere FromCSV(string filepath)
        {
            if (!File.Exists(filepath))
            {
                throw new FileNotFoundException($"The file at path {filepath} does not exist!");
            }

            using (var stream = File.OpenRead(filepath))
            {
                var reader = new StreamReader(stream);

                // First line is details on the Atmosphere table
                (string atmoType, int year) = ParseDetailsLine(reader.ReadLine());

                // Second line is the variable that each value in the line corresponds to
                // e.g. "Reference,Altitude,Static Pressure,Temp,Temp Lapse,"
                // This will guide how the values later on are assigned to a ReferenceAltitude struct
                string[] columnOrder = reader.ReadLine().Split(CSVDelimiter, StringSplitOptions.RemoveEmptyEntries);

                var altRefs = new List<ReferenceAltitude>(0);
                while (reader.ReadLine() is string line)
                {
                    altRefs.Add(ParseLine(line, columnOrder));
                }

                Atmosphere atmo = new Atmosphere()
                {
                    AtmosphereType = atmoType,
                    Year = year,
                    RefAltitudes = altRefs.ToArray(),
                };

                return atmo;
            }

            throw new Exception("Unexpected error - an Atmosphere object was not properly constructed!");
        }

        private static (string, int) ParseDetailsLine(string line)
        {
            string[] split = line.Split(CSVDelimiter, StringSplitOptions.RemoveEmptyEntries);

            if (split.Length >= 2)
            {
                return (split[0], int.Parse(split[1]));
            }
            else if (split.Length == 1)
            {
                return (split[0], 0);
            }
            else return ("Unknown", 0);
        }

        private static ReferenceAltitude ParseLine(string line, string[] columnOrder)
        {
            string[] split = line.Split(CSVDelimiter, StringSplitOptions.None);

            ReferenceAltitude refAlt = new ReferenceAltitude();

            for (int i = 0; i < columnOrder.Length; i++)
            {
                float value;

                if (float.TryParse(split[i], out value))
                {
                    switch (columnOrder[i])
                    {
                        case "Altitude":
                            refAlt.Altitude = value;
                            break;
                        case "Static Pressure":
                            refAlt.Pressure = value;
                            break;
                        case "Temp":
                            refAlt.Temp = value;
                            break;
                        case "Temp Lapse":
                            refAlt.TempLapseRate = value;
                            break;
                        default:
                            break;
                    }
                }
            }

            return refAlt;
        }
    }
}