using System.Numerics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SharpVoronoiLib;

namespace Engineeringthesis
{
    public static class Rendering
    {
        /// <summary>
        /// Draws the Voronoi diagram on the provided canvas using the edges from the Voronoi algorithm.
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="edges"></param>
        public static void DrawVoronoiDiagram(Canvas diagram, List<VoronoiEdge> edges)
        {
            foreach (var edge in edges)
            {
                Line line = new Line
                {
                    X1 = edge.Start.X,
                    Y1 = edge.Start.Y,
                    X2 = edge.End.X,
                    Y2 = edge.End.Y,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                diagram.Children.Add(line);
            }
        }
        /// <summary>
        /// Performs Lloyd's relaxation on the Voronoi diagram plane and returns a list of edges.
        /// </summary>
        /// <param name="plane">the plane that will undergo the relaxation process</param>
        /// <param name="iteration">number of times the relaxatin is performed, 1 by default</param>
        /// <param name="strength">strength of the relaxation</param>
        public static List<VoronoiEdge> RelaxedEdgesOfVoronoiDiagram(VoronoiPlane plane,int iteration = 1,float strength = 1f)
        {
           List<VoronoiEdge> relaxed_edges = plane.Relax(iteration, strength);
           return relaxed_edges;
        }

        /// <summary>
        /// Colors a Voronoi cell on the provided canvas using the specified centroid and color.
        /// </summary>
        /// <param name="diagram"></param>
        /// <param name="centroid">centroid of the polygon to color</param>
        /// <param name="color"></param>
        public static void ColorACell(Canvas diagram, VoronoiSite centroid, Color color = default)
        {
            List<VoronoiPoint> points = centroid.ClockwisePoints.ToList();
            
            if (color == default)
            {
                color = Colors.Blue;
            }          

            SolidColorBrush cell_color = new(color);

            Polygon voronoi_cell = new Polygon
            {
                Points = [.. points.Select(p => new Point(p.X, p.Y))],
                Fill = cell_color
            };
            diagram.Children.Add(voronoi_cell);
        }

        /// <summary>
        /// Converts a Color of type "Color" to a "Brush".
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static SolidColorBrush ColorToBrush(Color color)
        {
            return new SolidColorBrush(color);
        }
    }
}
