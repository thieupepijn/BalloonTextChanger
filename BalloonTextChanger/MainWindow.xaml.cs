using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BalloonTextChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnLoadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            bool? dialogResult = dialog.ShowDialog();
            if (dialogResult.HasValue && dialogResult.Value)
            {
                Uri uri = new Uri(dialog.FileName);
                BitmapImage image = new BitmapImage(uri);
                mainCanvas.Background = new ImageBrush(image);

                mainCanvas.Width = image.Width;
                mainCanvas.Height = image.Height;
                
            }
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point coord = e.GetPosition(mainCanvas); ;

            Ellipse cirkel = new Ellipse();
            cirkel.Fill = Brushes.Red;
            cirkel.Width = 5;
            cirkel.Height = 5;

            Canvas.SetLeft(cirkel, coord.X);
            Canvas.SetTop(cirkel, coord.Y);
            mainCanvas.Children.Add(cirkel);

        }
    }
}
