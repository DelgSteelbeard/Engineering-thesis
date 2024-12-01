using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class Polygon
    {
        public List<Vertex> Vertices { get; set; }

        public Polygon()
        {
            Vertices = new List<Vertex>();
        }

        public void AddVertex(Vertex vertex)
        {
            Vertices.Add(vertex);
        }
    }
}
