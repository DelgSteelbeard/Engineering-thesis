using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    public class Edge
    {
        public Vertex Start { get; set; }
        public Vertex End { get; set; }
        public Centroid LeftSite { get; set; }
        public Centroid RightSite { get; set; }

        public Edge(Vertex start, Vertex end, Centroid leftSite, Centroid rightSite)
        {
            Start = start;
            End = end;
            LeftSite = leftSite;
            RightSite = rightSite;
        }
    }
}
