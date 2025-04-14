using SharpVoronoiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Controls;

namespace Engineeringthesis
{
    public class MapData(MapGenerationDataSet generation_values)
    {
        public List<VoronoiSite> CentroidList { get; set; } = [];
        public List<VoronoiSite> LandCells { get; set; } = [];
        public List<VoronoiSite> WaterCells { get; set; } = [];
        public List<VoronoiSite> InLandWaterCells { get; set; } = [];
        public MapGenerationDataSet GenerationValues { get; set; } = generation_values;

        public void ColorMap(Canvas diagram, Color land_color = default, Color water_color = default, Color inland_water_color = default)
        {
            if (land_color == default)
            {
                land_color = Colors.Brown;
            }
            if (water_color == default)
            {
                water_color = Colors.Blue;
            }
            if (inland_water_color == default)
            {
                inland_water_color = Colors.LightBlue;
            }
            foreach (var cell in LandCells)
            {
                Rendering.ColorACell(diagram, cell, land_color);
            }
            foreach (var cell in WaterCells)
            {
                Rendering.ColorACell(diagram, cell, water_color);
            }
            foreach (var cell in InLandWaterCells)
            {
                Rendering.ColorACell(diagram, cell, inland_water_color);
            }
        }
    }
}
