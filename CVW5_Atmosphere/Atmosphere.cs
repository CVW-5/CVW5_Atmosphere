using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CVW5_Atmosphere
{
    public class Atmosphere
    {
        public const float Gravity = 9.80665f;
        public const float MolarMass = 0.0289644f;
        public const float GasConst = 8.3144598f;
        /// <summary>
        /// It's literally just an array with one comma: { ',' }, that's all it needs to be, that's all it will ever be because of UNITY.
        /// The ONE SINGLE FUCKING DELIMITER EVER NEEDED FOR CSV because it's LITERALLY IMPOSSIBLE
        /// to do string.Split() with a single non-array char and StringSplitOptions in this ANCIENT STUPID
        /// VERSION of C# and Dotnet. Fuck you Unity. Bastard.
        /// I'm going to write a whole goddamn manifesto in this documentation comment because
        /// I'm THAT mad. Unity's a fucking nightmare in 2022. Just update your fucking Dotnet version under the hood
        /// you amateurs, stop buying other companies and making them into your mobile advert jockies, just update
        /// your fucking game engine so it works good with modern language features and not this shit from 2003. God it's
        /// fucking frustrating to deal with this shit on a daily basis. Hell, even monthly (or less!) when I actually sit down to code
        /// things in Unity it makes me mad. I'm trying to be better about my anger management and you're hurting that effort
        /// more than testosterone blockers are helping. Fuck me, I can't get this mad much anymore, and you're setting me off on a whole new
        /// world of frustration and frantic typing as I suffer through the effort that is trying to get ONE FUCKING CHARACTER
        /// to split a line from a CSV file without any effort. Can't use using declarations, can't meaningfully use the goddamn
        /// nullable reference types, which is arguably how C# should work, can't do anything sane because of the archaic version
        /// of C# that Unity's advertising devs refuse to update the engine to. But how would they know how to? They fired
        /// all of the actual programmers in the name of buying up more advertising companies to do more goddamn
        /// mobile advertising because, quote from the CEO, "You're a fool if you're not doing mobile advertising"
        /// or something to that effect. What an asshat. Hate all this shit. Just give me normal modern C# again. Please,
        /// I fucking beg you.
        /// </summary>
        private readonly char[] CSVDelimiter = { ',' };
        private readonly Regex CSVRegex = new Regex(@"((?<value>[^,\n]*),)+");

        public string AtmosphereType = "Unknown";
        public int Year = 0;
        internal ReferenceAltitude[] RefAltitudes = new ReferenceAltitude[0];

        private Atmosphere() { }

        public Atmosphere FromCSV(string filepath)
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

            throw new System.Exception("Unexpected error - an Atmosphere object was not properly constructed!");
        }

        private (string, int) ParseDetailsLine(string line)
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

        private ReferenceAltitude ParseLine(string line, string[] columnOrder)
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
