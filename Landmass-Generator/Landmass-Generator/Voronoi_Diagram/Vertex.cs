using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class Vertex
    {
        public double X { get; set; }
        public double Y { get; set; }

        /// <summary>
        /// Creates a point with x and y coordinates
        /// </summary>
        /// <param name="x">coordinate x</param>
        /// <param name="y">coordinate y</param>
        public Vertex(double x,double y)
        {
            X = x;
            Y = y;
        }
    }
}
