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

namespace it2a_spol_blind_map
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<MapPoint> points = new List<MapPoint>()
        {
            new MapPoint { Name = "Praha",       XPercent = 0.3519, YPercent = 0.3916 },
            new MapPoint { Name = "Brno",        XPercent = 0.6864, YPercent = 0.6958 },
            new MapPoint { Name = "Ostrava",     XPercent = 0.8562, YPercent = 0.4570 },
        };

        private MapPoint currentActive;
        private Random rnd = new Random();
        private int skore = 0;
        private int celkemKol = 0;
        private int maxKol;

        public MainWindow()
        {
            InitializeComponent();
            maxKol = points.Count;
            StartNewRound();
            DrawPoints();
        }

        private void StartNewRound()
        {
            currentActive = points[rnd.Next(points.Count)];
            Aktivni_Mesto.Text = $"Aktivní město: {currentActive.Name}";
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DrawPoints();
        }

        private void MapImage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            DrawPoints();
        }

        void DrawPoints()
        {
            OverlayCanvas.Children.Clear();

            foreach (var point in points)
            {
                double x = MapImage.ActualWidth * point.XPercent;
                double y = MapImage.ActualHeight * point.YPercent;

                Button btn = new Button()
                {
                    Content = "X",
                    Width = 20,
                    Height = 20,
                    Tag = point
                };

                btn.Click += Btn_Click;

                Canvas.SetLeft(btn, x);
                Canvas.SetTop(btn, y);

                OverlayCanvas.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            MapPoint point = btn.Tag as MapPoint;
            if (point == currentActive)
            {
                skore++;
                MessageBox.Show("Správně");
            } else
            {
                MessageBox.Show($"Špatně, správná odpověď byla {currentActive.Name}");
            }
            celkemKol++;
            if (celkemKol == maxKol)
            {
                MessageBox.Show($"Konec hry! Skóre: {skore}/{celkemKol}");
                skore = 0;
                celkemKol = 0;
            } else
            {
                StartNewRound();
            }


        }
        private void MapImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var pos = e.GetPosition(MapImage);

            double xPercent = pos.X / MapImage.ActualWidth;
            double yPercent = pos.Y / MapImage.ActualHeight;

            MessageBox.Show($"{xPercent:F4} , {yPercent:F4}");
        }
    }
}
