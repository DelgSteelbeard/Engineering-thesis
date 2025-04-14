using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineeringthesis
{
    public class MapGenerationDataSet(
    double land_treshold = 0, double amplitude = 0,
    double base_frequency = 0, double octaves = 0,
    double offset_factor = 0)
    {
        public double LandTreshold { get; set; } = land_treshold;
        public double Amplitude { get; set; } = amplitude;
        public double BaseFrequency { get; set; } = base_frequency;
        public double Octaves { get; set; } = octaves;
        public double OffsetFactor { get; set; } = offset_factor;
    }
}
