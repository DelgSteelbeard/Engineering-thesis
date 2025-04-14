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

        LandTreshold.Value = double.Parse(LandTresholdBox.Text);
        Amplitude.Value = double.Parse(AmplitudeBox.Text);
        BaseFrequency.Value = double.Parse(BaseFrequencyBox.Text);
        Octaves.Value = double.Parse(OctavesBox.Text);
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
        List<VoronoiEdge> list_of_edge = VoronoiPlane.TessellateOnce(list_of_centroid, 0, 0, Diagram.ActualWidth, Diagram.ActualHeight);
        VoronoiPlane plane = GeometryData.GenerateVoronoiPlane(list_of_centroid, Diagram);

        list_of_edge = Rendering.RelaxedEdgesOfVoronoiDiagram(plane, 5);

        Rendering.DrawVoronoiDiagram(Diagram, list_of_edge);

        MapData map = new MapData();
        map.list_of_centroid = list_of_centroid;
        MapLogic.ClassifyVoronoiCells(map, list_of_edge, Diagram, LandTreshold.Value);

        map.ColorMap(Diagram);
    }
    private void LandTreshold_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        LandTresholdBox.Text = LandTreshold.Value.ToString();
    }
    private void Amplitude_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        AmplitudeBox.Text = Amplitude.Value.ToString();
    }
    private void BaseFrequency_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        BaseFrequencyBox.Text = BaseFrequency.Value.ToString();
    }
    private void Octaves_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        string message = Octaves.Value.ToString();
        try
        {
            if (OctavesBox != null)
            {
                OctavesBox.Text = Octaves.Value.ToString();
            }
        }
        catch (NullReferenceException)
        {
            MessageBox.Show("OctavesBox is null");
        }
    }
}