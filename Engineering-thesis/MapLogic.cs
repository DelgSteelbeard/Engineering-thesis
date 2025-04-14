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

        public static void ClassifyVoronoiCells(MapData map, List<VoronoiEdge> edges, Canvas canvas, double treshold = 0.5)
        {
            ClassifyVoronoiCells(map.list_of_centroid, edges, map.water_cells, map.land_cells, canvas, treshold);
        }

        public static bool IsLand(VoronoiSite centroid, Canvas canvas, double treshold, double offsetFactor = 0.1)
        {
            Point center = GeometryData.GetCanvasCenter(canvas);

            // Apply random offset to the center for more variation  
            Random random = new Random(canvas.GetHashCode()); // Seed based on canvas for consistency  
            double offsetX = offsetFactor * (random.NextDouble() - 0.5) * canvas.ActualWidth;
            double offsetY = offsetFactor * (random.NextDouble() - 0.5) * canvas.ActualHeight;
            center.Offset(offsetX, offsetY);

            double dx = GeometryData.SiteToPointDifferance(centroid, center).X;
            double dy = GeometryData.SiteToPointDifferance(centroid, center).Y;

            double distance = GeometryData.CalculateDistance(dx, dy);

            double corner_to_center = GeometryData.CalculateDistance(center.X, center.Y);

            double r = distance / corner_to_center;

            double angle = Math.Atan2(dy, dx);

            // Introduce Ridged noise for more natural shapes  
            random = new Random(centroid.GetHashCode()); // Seed based on centroid for consistency  
            double noise = GenerateRidgedNoise(angle, r, random);

            double value = r + noise;

            return value < treshold;
        }

        private static double GenerateRidgedNoise(double angle, double radius, Random random)
        {
            // Generate Perlin-like noise with ridges
            double baseFrequency = 4.0;
            double amplitude = 0.2;

            double noiseValue = 0.0;
            double frequency = baseFrequency;
            double currentAmplitude = amplitude;

            for (int i = 0; i < 4; i++) // Octaves for multi-frequency noise
            {
                double rawNoise = Math.Sin(frequency * angle + random.NextDouble() * 2 * Math.PI) *
                                  Math.Sin(frequency * radius + random.NextDouble() * 2 * Math.PI);
                rawNoise = Math.Abs(rawNoise); // Ridged effect
                noiseValue += rawNoise * currentAmplitude;

                frequency *= 2.0; // Increase frequency
                currentAmplitude *= 0.5; // Decrease amplitude
            }

            return noiseValue - amplitude; // Normalize to center around 0
        }
    }
}
