using SharpVoronoiLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Engineeringthesis
{
    public static class MapLogic
    {
        /// <summary>
        /// Classifies the Voronoi cells into land and water cells based on their distance from the center of the canvas.
        /// </summary>
        /// <param name="cells">List of sites to devide</param>
        /// <param name="edges">list of all edges that form the diagram</param>
        /// <param name="water_cells">list of sites that water cell will be added to</param>
        /// <param name="land_cells">list of sitest that land cell will be added to</param>
        /// <param name="canvas">the canvas where diagram is located (uses the dimentions)</param>
        /// <param name="treshold">threshlod of when a watercell becoms a land cell</param>
        public static void ClassifyVoronoiCells(List<VoronoiSite> cells, List<VoronoiEdge> edges, List<VoronoiSite> water_cells, List<VoronoiSite> land_cells, Canvas canvas, double treshold = 0.5)
        {
            //border are allways water cells
            water_cells = GeometryData.GetBorderCells(edges);
            foreach (var cell in cells)
            {
                if(IsLand(cell, canvas, treshold))
                {
                    land_cells.Add(cell);
                }
                else
                {
                    water_cells.Add(cell);
                }
            }         
        }

        public static bool IsLand(VoronoiSite centroid, Canvas canvas, double treshold)
        {
            Point center = GeometryData.GetCanvasCenter(canvas);

            double dx = GeometryData.SiteToPointDifferance(centroid, center).X;
            double dy = GeometryData.SiteToPointDifferance(centroid, center).Y;

            double distance = GeometryData.CalculateDistance(dx, dy);

            double corner_to_center = GeometryData.CalculateDistance(center.X, center.Y);

            double r = distance / corner_to_center;

            double angle = Math.Atan2(dy, dx);

            double perturbation = 0.1 * Math.Sin(6 * angle);

            double value = r + perturbation;

            return value < treshold;
        }
    }
}
