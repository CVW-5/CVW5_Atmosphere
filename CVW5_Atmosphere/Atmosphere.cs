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
    }
}
