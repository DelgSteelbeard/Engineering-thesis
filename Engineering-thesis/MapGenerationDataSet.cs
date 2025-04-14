using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineeringthesis
{
    public class MapGenerationDataSet
    {
        public double LandTreshold { get; set; }
        public double Amplitude { get; set; }
        public double BaseFrequency { get; set; }
        public double Octaves { get; set; }
        public double OffsetFactor { get; set; }

        public MapGenerationDataSet(
        double land_treshold = 0, double amplitude = 0, 
        double base_frequency = 0, double octaves = 0,
        double offset_factor = 0)
        {
            LandTreshold = land_treshold;
            Amplitude = amplitude;
            BaseFrequency = base_frequency;
            Octaves = octaves;
            OffsetFactor = offset_factor;
        }
    }
}
