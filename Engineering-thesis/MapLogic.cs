using SharpVoronoiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engineeringthesis
{
    public static class MapLogic
    {
        public static List<VoronoiSite> GetWaterCells(List<VoronoiEdge> edges)
        {
            List<VoronoiSite> water_cells = new List<VoronoiSite>();
            water_cells = GeometryData.GetBorderCells(edges);
            //todo: add cells from the island generaton process
            return water_cells;
        }
    }
}
