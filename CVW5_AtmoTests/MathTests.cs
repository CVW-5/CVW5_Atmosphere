using CVW5_Atmosphere;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.IO;

namespace CVW5_AtmoTests
{
    [TestClass]
    public class MathTests
    {
        Atmosphere atmo = Atmosphere.FromCSV("./US Standard Atmosphere.csv");
        string targetFile = "./testoutput.csv";

        /// <summary>
        /// Tests the atmospheric temperature at the reference altitudes. This should always pass,
        /// as it is effectively circular math.
        /// </summary>
        [TestMethod]
        public void TestTempsAtReferenceAlts ()
        {
            float[] alts = atmo.RefAltitudes.Select(x => x.Altitude).ToArray();
            float[] temps = atmo.RefAltitudes.Select(x => x.Temp).ToArray();

            for(int i = 0; i < alts.Length; i++)
            {
                Assert.AreEqual(temps[i], atmo.GetTemperature(alts[i]));
            }
        }

        [TestMethod]
        public void PrintTempScale()
        {
            int count = 85;
            float step = 1000;

            for (int i = 0; i <= count; i++)
            {
                float alt = step * i;

                Console.WriteLine($"{alt},{atmo.GetTemperature(alt)}");
            }
        }

        [TestMethod]
        public void PrintPresScale ()
        {
            int count = 85;
            float step = 1000;

            for (int i = 0; i <= count; i++)
            {
                float alt = step * i;

                Console.WriteLine($"{alt},{atmo.GetPressure(alt)}");
            }
        }

        [TestMethod]
        public void PrintDensityScale()
        {
            int count = 85;
            float step = 1000;

            for (int i = 0; i <= count; i++)
            {
                float alt = step * i;

                Console.WriteLine($"{alt},{atmo.GetDensity(alt)}");
            }
        }

        [TestMethod]
        public void WriteAtmoValues ()
        {
            int count = 85;
            float step = 1000;

            if (!File.Exists(targetFile))
                File.Create(targetFile).Close();

            using (var stream = File.Open(targetFile, FileMode.Truncate))
            {
                var writer = new StreamWriter(stream);

                writer.WriteLine("Altitude,Temperature,Pressure,Density");

                for (int i = 0; i <= count; i++)
                {
                    float alt = step * i;
                    float temp = atmo.GetTemperature(alt);
                    float pres = atmo.GetPressure(alt);
                    float dens = atmo.GetDensity(alt);

                    writer.WriteLine($"{alt},{temp},{pres},{dens}");
                }
            }

            Console.WriteLine($"Values written to {Path.GetFullPath(targetFile)}");
        }
    }
}
