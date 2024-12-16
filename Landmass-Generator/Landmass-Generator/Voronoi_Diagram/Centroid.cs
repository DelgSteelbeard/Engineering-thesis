using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    public class Centroid
    {
        public double X { get; set; }
        public double Y { get; set; }

        /// <summary>
        /// Creates a centroid with x and y coordinates
        /// </summary>
        /// <param name="x">coordinate x</param>
        /// <param name="y">coordinate y</param>
        public Centroid(double x, double y)
        {
            X = x;
            Y = y;
        }

        private List<Centroid> GenerateRandomCentroids(int count, double width, double height)
        {
            Random rand = new Random();
            var centroids = new List<Centroid>();

            for (int i = 0; i < count; i++)
            {
                centroids.Add(new Centroid((rand.NextDouble() * width), (rand.NextDouble() * height)));
            }

            return centroids;
        }

    }
}
