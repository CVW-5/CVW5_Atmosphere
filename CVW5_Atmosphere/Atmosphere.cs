/** +===========================================================+
 *  |         ATMOSPHERE OBJECT - CVW5_Atmosphere               |
 *  |         Author: Robin Lodholm (Sendit/OrbitusII)          |
 *  |         Written: January 2023                             |
 *  |                                                           |
 *  |     This is the basic unit of functionality within this   |
 *  | library. Without it, you do nothing.                      |
 *  |                                                           |
 *  | Atmosphere.cs is a partial class. See Atmosphere.*.cs for |
 *  | other relevant functionality like solvers, constants, and |
 *  | initializers/constructors.                                |
 *  |                                                           |
 *  | Basic behavior is based on the US Standard Atmosphere and |
 *  | Barometric formula, see the links below:                  |
 *  | https://en.wikipedia.org/wiki/U.S._Standard_Atmosphere    |
 *  | https://en.wikipedia.org/wiki/Barometric_formula          |
 *  +===========================================================+
 */

namespace CVW5_Atmosphere
{
    /// <summary>
    /// The base Atmosphere object. Contains all necessary data and methods for atmospheric effects.
    /// </summary>
    public partial class Atmosphere
    {
        public string AtmosphereType { get; set; } = "Unknown";
        public int Year { get; set; } = 0;
        public ReferenceAltitude[] RefAltitudes { get; internal set; } = new ReferenceAltitude[0];

        private Atmosphere() { }

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
    }
}
