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
        public static void ClassifyVoronoiCells(MapData map, List<VoronoiEdge> edges, Canvas canvas)
        {
            //border are allways water cells
            map.water_cells = GeometryData.GetBorderCells(edges);
            foreach (var cell in map.list_of_centroid)
            {
                if (IsLand(cell, map, canvas))
                {
                    map.land_cells.Add(cell);
                }
                else
                {
                    map.water_cells.Add(cell);
                }
            }
        }

        /// <summary>
        /// Determines if a Voronoi cell is classified as land or water based on its distance from the center of the canvas and noise generation.
        /// </summary>
        /// <param name="centroid">Central point from which the distance that determines if a cell is land is calculated</param>
        /// <param name="map"></param>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public static bool IsLand(VoronoiSite centroid, MapData map, Canvas canvas)
        {
            Point center = GeometryData.GetCanvasCenter(canvas);

            // Apply random offset to the center for more variation  
            Random random = new Random(canvas.GetHashCode()); // Seed based on canvas for consistency  
            double offsetX = map.generation_values.OffsetFactor * (random.NextDouble() - 0.5) * canvas.ActualWidth;
            double offsetY = map.generation_values.OffsetFactor * (random.NextDouble() - 0.5) * canvas.ActualHeight;
            center.Offset(offsetX, offsetY);

            double dx = GeometryData.SiteToPointDifferance(centroid, center).X;
            double dy = GeometryData.SiteToPointDifferance(centroid, center).Y;

            double distance = GeometryData.CalculateDistance(dx, dy);

            double corner_to_center = GeometryData.CalculateDistance(center.X, center.Y);

            double r = distance / corner_to_center;

            double angle = Math.Atan2(dy, dx);

            // Introduce Ridged noise for more natural shapes  
            random = new Random(centroid.GetHashCode()); // Seed based on centroid for consistency  
            double noise = GenerateRidgedNoise(angle, r, random, map);

            double value = r + noise;

            return value < map.generation_values.LandTreshold;
        }

        /// <summary>
        /// Generates ridged noise based on the angle and radius from the center of the canvas.
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="radius"></param>
        /// <param name="random"></param>
        /// <param name="map"></param>
        /// <returns></returns>
        private static double GenerateRidgedNoise(double angle, double radius, Random random, MapData map)
        {
            double double_octaves = Math.Round(map.generation_values.Octaves);
            int octaves = (int)double_octaves;
            // Generate Perlin-like noise with ridges
            double noiseValue = 0.0;
            double frequency = map.generation_values.BaseFrequency;
            double currentAmplitude = map.generation_values.Amplitude;

            for (int i = 0; i < octaves; i++) // Octaves for multi-frequency noise
            {
                double rawNoise = Math.Sin(frequency * angle + random.NextDouble() * 2 * Math.PI) *
                                  Math.Sin(frequency * radius + random.NextDouble() * 2 * Math.PI);
                rawNoise = Math.Abs(rawNoise); // Ridged effect
                noiseValue += rawNoise * currentAmplitude;

                frequency *= 2.0; // Increase frequency
                currentAmplitude *= 0.5; // Decrease amplitude
            }

            return noiseValue - map.generation_values.Amplitude; // Normalize to center around 0
        }
    }
}
