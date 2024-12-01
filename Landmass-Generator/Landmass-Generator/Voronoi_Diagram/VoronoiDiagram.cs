using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class VoronoiDiagram
    {
        public double Width { get; set; }
        public double Height { get; set; }
        public List<Centroid> Centroids { get; set; }
        public List<Polygon> Polygons { get; set; }

        public VoronoiDiagram(double width, double height, List<Centroid> centroids)
        {
            Width = width;
            Height = height;
            Centroids = centroids;
            Polygons = new List<Polygon>();

            GenerateDiagram();
        }

        private void GenerateDiagram()
        {
            var edges = Algorithms.GenerateVoronoiEdges(Width, Height, Centroids);

            // Process edges into polygons
            foreach (var centroid in Centroids)
            {
                var polygon = new Polygon();
                // Logic to convert edges to polygons for each centroid
                Polygons.Add(polygon);
            }
        }
    }
}
