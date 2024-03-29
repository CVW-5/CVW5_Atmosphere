using CVW5_Atmosphere;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CVW5_AtmoTests
{
    [TestClass]
    public class StandardAtmoTests
    {
        const string relativeFilePath = "./US Standard Atmosphere.csv";
        Atmosphere testAtmo = Atmosphere.US_Standard;

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
