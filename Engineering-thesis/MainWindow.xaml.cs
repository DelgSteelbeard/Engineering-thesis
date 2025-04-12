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
namespace Engineeringthesis;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    List<VoronoiSite> list_of_centroid = new List<VoronoiSite>();   
    public MainWindow()
    {
        InitializeComponent();
    }
    private void GeneratePoints_Click(object sender, RoutedEventArgs e)
    {
        Diagram.Children.Clear();
        // Checks if the users tryes to put something other than numbers into the field for "amount of points"
        if (!int.TryParse(PointCountBox.Text, out int count) || count <= 0)
        {
            MessageBox.Show("Please give a correct amount of points.");
            return;
        }
        list_of_centroid.Clear();
        GeometryData.GeneratePoints(count, list_of_centroid, Diagram);
        List<VoronoiEdge> list_of_edge = VoronoiPlane.TessellateOnce(list_of_centroid, 0,0,Diagram.ActualWidth, Diagram.ActualHeight);
        VoronoiPlane plane = GeometryData.GenerateVoronoiPlane(list_of_centroid, Diagram);

        list_of_edge = Rendering.RelaxedEdgesOfVoronoiDiagram(plane,5);

        Rendering.DrawVoronoiDiagram(Diagram, list_of_edge);


        List<VoronoiSite> border_cells = GeometryData.GetBorderCells(list_of_edge);    

        foreach (VoronoiSite cell in border_cells)
        {
            Rendering.ColorACell(Diagram, cell);
        }
    }
}