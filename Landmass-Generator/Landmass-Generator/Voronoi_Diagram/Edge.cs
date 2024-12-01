using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class Edge
    {
        public Vertex Start { get; set; }
        public Vertex End { get; set; }

        /// <summary>
        /// creates a edge with start and end points
        /// </summary>
        /// <param name="start">start point</param>
        /// <param name="end">end point</param>
        public Edge(Vertex start, Vertex end)
        {
            Start = start;
            End = end;
        }
    }
}
