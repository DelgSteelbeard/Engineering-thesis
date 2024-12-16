using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Landmass_Generator.Voronoi_Diagram
{
    internal class Algorithms
    {
        private static double sweepLineY;

        private static double GetLeftBreakPoint(BeachlineNode node, double sweepY)
        {
            if (node.Left == null)
            {
                return double.NegativeInfinity;
            }

            return GetParabolaIntersectionX(node.Left.Site.X, node.Left.Site.Y, node.Site.X, node.Site.Y, sweepY);
        }

        private static double GetRightBreakPoint(BeachlineNode node, double sweepY)
        {
            if(node.Right == null || node.Right.Site == null)
            {
                return double.PositiveInfinity;
            }

            return GetParabolaIntersectionX(node.Site.X, node.Site.Y, node.Right.Site.X, node.Right.Site.Y, sweepY);
        }

        private static double GetParabolaIntersectionX(double x1, double y1, double x2, double y2, double sweepY)
        {
            if (y1 == sweepY)
            {
                return x1; // Punkt leży na plaży
            }
            if (y2 == sweepY)
            {
                return x2; // Punkt leży na plaży
            }

            // Oblicz współczynniki równania kwadratowego
            double p1 = 1.0 / (2.0 * (y1 - sweepY));
            double p2 = 1.0 / (2.0 * (y2 - sweepY));

            double a = p1 - p2;
            double b = 2.0 * (x2 * p2 - x1 * p1);
            double c = (x1 * x1) * p1 - (x2 * x2) * p2;

            // Rozwiązanie równania kwadratowego
            double discriminant = b * b - 4 * a * c;

            if (discriminant < 0)
            {
                throw new InvalidOperationException("No real intersection existsF between parabolas.");
            }

            // Zwracamy mniejszą wartość (bliższe przecięcie)
            double sqrtD = Math.Sqrt(discriminant);
            double xIntersection1 = (-b + sqrtD) / (2 * a);
            double xIntersection2 = (-b - sqrtD) / (2 * a);

            return Math.Min(xIntersection1, xIntersection2);
        }

        private static void HandleCircleEvent(Event circleEvent, ref BeachlineNode beachline, List<Edge> edges)
        {
            var arcToRemove = circleEvent.Arc; // Arc is of type BeachlineNode.

            if (arcToRemove == null || arcToRemove.Parent == null)
            {
                return; // Nie można usunąć węzła bez rodzica.
            }

            // Utwórz nowy wierzchołek w diagramie Voronoia.
            var parentEdge = arcToRemove.Parent.Edge;
            var newVertex = new Vertex(circleEvent.X, circleEvent.Y);

            if (parentEdge != null)
            {
                parentEdge.End = newVertex; // Zakończ bieżącą krawędź.
            }

            // Usuń łuk z drzewa plaży.
            RemoveArc(arcToRemove, ref beachline);

            // Dodaj nowe krawędzie (jeśli wymagane) i nowe zdarzenia kołowe.
            // Logika zależy od struktury danych.
        }


        private static void RemoveArc(BeachlineNode arc, ref BeachlineNode beachline)
        {
            var parent = arc.Parent;

            if (parent.Left == arc)
            {
                parent.Left = null;
            }
            else
            {
                parent.Right = null;
            }

            // Further restructuring logic as required
        }

        private static void HandleSiteEvent(Event siteEvent, ref BeachlineNode beachline, List<Edge> edges)
        {
            var newSite = siteEvent.Site;

            if (beachline == null)
            {
                beachline = new BeachlineNode(newSite);
                return;
            }

            var node = FindNodeAbove(beachline, newSite.X);

            if (node.IsLeaf)
            {
                SplitArc(node, newSite, ref beachline, edges);
            }
        }

        private static BeachlineNode FindNodeAbove(BeachlineNode root, double x)
        {
            var current = root;

            while (!current.IsLeaf)
            {
                // Pobierz współrzędne paraboli
                double leftX = GetLeftBreakPoint(current, sweepLineY);
                double rightX = GetRightBreakPoint(current, sweepLineY);

                if (x < leftX)
                {
                    current = current.Left;
                }
                else if (x > rightX)
                {
                    current = current.Right;
                }
                else
                {
                    return current;
                }
            }

            return current;
        }

        private static void SplitArc(BeachlineNode node, Centroid newSite, ref BeachlineNode beachline, List<Edge> edges)
        {
            var leftArc = new BeachlineNode(node.Site);
            var middleArc = new BeachlineNode(newSite);
            var rightArc = new BeachlineNode(node.Site);

            var newEdge = new Edge(new Vertex(newSite.X, sweepLineY), null, leftArc.Site, rightArc.Site);
            edges.Add(newEdge);

            node.Left = leftArc;
            node.Right = new BeachlineNode(null)
            {
                Left = middleArc,
                Right = rightArc
            };

            leftArc.Parent = node;
            middleArc.Parent = node.Right;
            rightArc.Parent = node.Right;
        }


        public static List<Edge> GenerateVoronoiEdges(double width, double height, List<Centroid> centroids)
        {
            if (centroids == null || centroids.Count == 0)
            {
                throw new ArgumentException("Centroids list cannot be null or empty.");
            }

            // Ustaw początkową wartość sweepLineY (maksymalna Y spośród centroidów)
            sweepLineY = centroids.Max(c => c.Y);

            PriorityQueue<Event, double> eventQueue = new PriorityQueue<Event, double>();
            foreach (var centroid in centroids)
            {
                eventQueue.Enqueue(new Event(centroid.X, centroid.Y, true, centroid), centroid.Y);
            }

            // Initialize structures for edges, beachline, and result.
            var edges = new List<Edge>();
            BeachlineNode beachline = null;

            while (eventQueue.Count > 0)
            {
                var currentEvent = eventQueue.Dequeue();

                // Przesuń linię zamiatania
                sweepLineY = currentEvent.Y;

                if (currentEvent.IsSiteEvent)
                {
                    HandleSiteEvent(currentEvent, ref beachline, edges);
                }
                else
                {
                    HandleCircleEvent(currentEvent, ref beachline, edges);
                }
            }

            //TO DEBUGOWANIE
            foreach (var edge in edges)
            {
                System.Diagnostics.Debug.WriteLine($"Edge: Start({edge.Start.X}, {edge.Start.Y}), End({edge.End?.X}, {edge.End?.Y})");
            }

            // Zamknij krawędzie na brzegach
            CloseEdges(edges, width, height);

            return edges;
        }

        private static void CloseEdges(List<Edge> edges, double width, double height)
        {
            foreach (var edge in edges)
            {
                if (edge.End == null)
                {
                    // Oblicz kierunek krawędzi (wektor)
                    double dx = edge.Start.X - width / 2;
                    double dy = edge.Start.Y - height / 2;

                    // Normalizacja wektora
                    double length = Math.Sqrt(dx * dx + dy * dy);
                    dx /= length;
                    dy /= length;

                    // Zamknięcie krawędzi na brzegu obszaru
                    edge.End = new Vertex(
                        Math.Clamp(edge.Start.X + dx * 1000, 0, width),
                        Math.Clamp(edge.Start.Y + dy * 1000, 0, height)
                    );
                }
            }
        }
    }
}
