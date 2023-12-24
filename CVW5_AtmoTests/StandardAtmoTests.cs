using CVW5_Atmosphere;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CVW5_AtmoTests
{
    [TestClass]
    public class AtmosphereObjectTests
    {
        const string relativeFilePath = "./US Standard Atmosphere.csv";
        Atmosphere testAtmo = Atmosphere.FromCSV(relativeFilePath);

        [TestMethod]
        public void FindTestFolder()
        {
            Console.WriteLine(Environment.CurrentDirectory);
        }

        [TestMethod]
        public void TestFileLoaded ()
        {
            Assert.IsNotNull(testAtmo);
        }

        [TestMethod]
        public void TestAtmoType ()
        {
            Assert.AreEqual("US Standard Atmosphere", testAtmo.AtmosphereType);
        }

        [TestMethod]
        public void TestAtmoYear ()
        {
            Assert.AreEqual((int)1976, testAtmo.Year);
        }

        [TestMethod]
        public void TestRefAltitudeCount ()
        {
            Assert.AreEqual(8, testAtmo.RefAltitudes.Length);
        }
    }
}
