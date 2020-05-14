using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using Brushes = System.Windows.Media.Brushes;
using Color = System.Drawing.Color;
using Point = System.Windows.Point;

namespace BalloonTextChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Bitmap _bitmap;
      
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
                _bitmap = BitmapImage2Bitmap(image);


                mainCanvas.Background = new ImageBrush(image);

                mainCanvas.Width = image.Width;
                mainCanvas.Height = image.Height;
                
            }
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point coord = e.GetPosition(mainCanvas); ;

            //Ellipse cirkel = new Ellipse();
            //cirkel.Fill = Brushes.Red;
            //cirkel.Width = 5;
            //cirkel.Height = 5;

            //Canvas.SetLeft(cirkel, coord.X);
            //Canvas.SetTop(cirkel, coord.Y);
            //mainCanvas.Children.Add(cirkel);

            Color color = GetColor(_bitmap, coord);
            if (IsWhite(color))
            {
                MessageBox.Show("Wit!");
            }
            else
            {
                MessageBox.Show("Niet-Wit!");
            }
        }


        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }


        private Color GetColor(Bitmap bitmap, Point coord)
        {
           Color color = bitmap.GetPixel((int)coord.X, (int)coord.Y);
            return color;
        }

        private bool IsWhite(Color color)
        {
            if ((color.R < 200) || (color.G < 200) || (color.B < 200))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

    }
}
