using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SharpVoronoiLib;
using static SharpVoronoiLib.VoronoiPoint;

namespace Engineeringthesis
{
    public static class GeometryData
    {
        /// <summary>
        /// Generates random points within the specified canvas and adds them to the provided list of centroids.
        /// </summary>
        /// <param name="count">how many points it will generate</param>
        /// <param name="list_of_centroid">the list of VoronoiSites to generate random points for</param>
        /// <param name="diagram">the canvas for which the points will be generated</param>
        public static void GeneratePoints(int count, List<VoronoiSite> list_of_centroid, Canvas diagram)
        {
            Random random = new Random();
            double canvas_height = diagram.ActualHeight;
            double canvas_width = diagram.ActualWidth;
            for (int i = 0; i < count; i++)
            {
                double x = random.NextDouble() * (canvas_width - 5);
                double y = random.NextDouble() * (canvas_height - 5);
                VoronoiSite centroid = new VoronoiSite(x, y);
                list_of_centroid.Add(centroid);
            }
        }

        /// <summary>
        /// Returns a list of sites that are on the border of the Voronoi diagram.
        /// </summary>
        /// <param name="edges">list of edges that form the diagram</param>
        /// <returns></returns>
        public static List<VoronoiSite> GetBorderCells(List<VoronoiEdge> edges)
        {
            List<VoronoiSite> border_cells = new List<VoronoiSite>();
            foreach (VoronoiEdge edge in edges)
            {
                // if only Right site of edge exists it is a border edge
                if (edge.Left == null)
                {
                    //Try catch which checks if the Right site of the edge is null (should not happen)
                    try
                    {
                        if (edge.Right != null)
                            border_cells.Add(edge.Right);
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("GetBorderCells returned a border edge with a null for a Right site");
                    }
                }
            }
            return border_cells;
        }

        /// <summary>
        /// Generates a Voronoi plane using the provided list of centroids and the specified canvas.
        /// </summary>
        /// <param name="list_of_centroid"></param>
        /// <param name="diagram"></param>
        /// <returns></returns>
        public static VoronoiPlane GenerateVoronoiPlane(List<VoronoiSite> list_of_centroid, Canvas diagram)
        {
            VoronoiPlane plane = new VoronoiPlane(0, 0, diagram.ActualWidth, diagram.ActualHeight);
            plane.SetSites(list_of_centroid);
            plane.Tessellate();
            return plane;
        }

        /// <summary>
        /// Calculates and returns the center of the provided canvas.
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        public static Point GetCanvasCenter(Canvas canvas)
        {
            double center_X = canvas.ActualWidth / 2;
            double center_Y = canvas.ActualHeight / 2;
            return new Point(center_X, center_Y);
        }

        public static Point SiteToPointDifferance(VoronoiSite centroid, Point center)
        {
            double x = centroid.X - center.X;
            double y = centroid.Y - center.Y;
            return new Point(x, y);
        }

        public static double CalculateDistance(double dx, double dy)
        {
            return Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
