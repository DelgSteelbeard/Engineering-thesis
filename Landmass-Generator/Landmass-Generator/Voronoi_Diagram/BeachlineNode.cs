using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class BeachlineNode
    {
        public Centroid Site { get; private set; }
        public Edge Edge { get; set; }
        public BeachlineNode Left { get; set; }
        public BeachlineNode Right { get; set; }
        public BeachlineNode Parent { get; set; }

        public bool IsLeaf => Left == null && Right == null;

        public BeachlineNode(Centroid site)
        {
            Site = site;
        }
    }

}
