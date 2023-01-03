using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace CVW5_Atmosphere
{
    public partial class Atmosphere
    {
        public string AtmosphereType { get; set; } = "Unknown";
        public int Year { get; set; } = 0;
        public ReferenceAltitude[] RefAltitudes { get; internal set; } = new ReferenceAltitude[0];

        private Atmosphere() { }

    }
}
