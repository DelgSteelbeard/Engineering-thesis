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
    public void InitializeSliderValue(TextBox text_box, Slider slider)
    {
        if (text_box != null && slider != null)
        {
            slider.Value = double.Parse(text_box.Text);
        }
        else
        {
            MessageBox.Show("InitializeSliderValue error: TextBox or Slider is null");
        }
    }
    public void SetMapGenerationData(MapGenerationDataSet generation_values)
    {
        generation_values.LandTreshold = LandTreshold.Value;
        generation_values.Amplitude = Amplitude.Value;
        generation_values.BaseFrequency = BaseFrequency.Value;
        generation_values.Octaves = Octaves.Value;
        generation_values.OffsetFactor = OffsetFactor.Value;
    }

    List<VoronoiSite> list_of_centroid = new();  
    
    public MainWindow()
    {
        InitializeComponent();

        InitializeSliderValue(LandTresholdBox, LandTreshold);
        InitializeSliderValue(AmplitudeBox, Amplitude);
        InitializeSliderValue(BaseFrequencyBox, BaseFrequency);
        InitializeSliderValue(OctavesBox, Octaves);
        InitializeSliderValue(OffsetFactorBox, OffsetFactor);

    }
    private void GeneratePoints_Click(object sender, RoutedEventArgs e)
    {
        MapGenerationDataSet generation_values = new();
        SetMapGenerationData(generation_values);
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

        MapData map = new MapData(generation_values);
        map.list_of_centroid = list_of_centroid;
        MapLogic.ClassifyVoronoiCells(map, list_of_edge, Diagram);

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
    private void OffsetFactor_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
    {
        OffsetFactorBox.Text = OffsetFactor.Value.ToString();
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