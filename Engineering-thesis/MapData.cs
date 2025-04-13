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
    public class MapData
    {
        public List<VoronoiSite> list_of_centroid { get; set; }
        public List<VoronoiSite> land_cells { get; set; }
        public List<VoronoiSite> water_cells { get; set; }

        public MapData()
        {
            list_of_centroid = new List<VoronoiSite>();
            land_cells = new List<VoronoiSite>();
            water_cells = new List<VoronoiSite>();
        }

        public void ColorMap(Canvas diagram, Color landColor = default(Color), Color waterColor = default(Color))
        {
            if (landColor == default(Color))
            {
                landColor = Colors.Brown;
            }
            if (waterColor == default(Color))
            {
                waterColor = Colors.Blue;
            }
            foreach (var cell in land_cells)
            {
                Rendering.ColorACell(diagram, cell, landColor);
            }
            foreach (var cell in water_cells)
            {
                Rendering.ColorACell(diagram, cell, waterColor);
            }
        }
    }
}
