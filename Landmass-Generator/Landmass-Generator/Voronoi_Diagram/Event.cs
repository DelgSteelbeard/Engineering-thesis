using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class Event
    {
        public double X { get; }
        public double Y { get; }
        public bool IsSiteEvent { get; }
        public Centroid Site { get; }
        public BeachlineNode Arc { get; }

        public Event(double x, double y, bool isSiteEvent, Centroid site = null, BeachlineNode arc = null)
        {
            X = x;
            Y = y;
            IsSiteEvent = isSiteEvent;
            Site = site;
            Arc = arc;
        }
    }
}

