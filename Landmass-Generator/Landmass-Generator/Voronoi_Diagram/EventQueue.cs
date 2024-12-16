using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class EventQueue
    {
        private readonly SortedSet<Event> events;

        public EventQueue()
        {
            events = new SortedSet<Event>(Comparer<Event>.Create((e1, e2) =>
            {
                if (e1.Y == e2.Y) return e1.X.CompareTo(e2.X);
                return e1.Y.CompareTo(e2.Y);
            }));
        }

        public void Enqueue(Event evt) => events.Add(evt);
        public Event Dequeue()
        {
            var first = events.Min;
            events.Remove(first);
            return first;
        }

        public int Count => events.Count;
    }
}
