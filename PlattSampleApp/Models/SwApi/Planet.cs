using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlattSampleApp.Models.SwApi
{
    public class Planet
    {
        public string Name { get; set; }
        public string Diameter { get; set; }
        public string RotationPeriod { get; set; }
        public string OrbitalPeriod { get; set; }
        public string Gravity { get; set; }
        public string Population { get; set; }
        public string Climate { get; set; }
        public string Terrain { get; set; }
        public string SurfaceWater { get; set; }
        public List<string> Residents { get; set; }
        public List<string> Films { get; set; }
        public string Url { get; set; }
    }
}
