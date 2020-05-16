using Microsoft.Win32;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Drawing.Color;

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

                _bitmap = Util.BitmapImage2Bitmap(image); 
                mainCanvas.Background = new ImageBrush(image);

                mainCanvas.Width = image.Width;
                mainCanvas.Height = image.Height;
            }
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Coordinate coord = new Coordinate((int)e.GetPosition(mainCanvas).X, (int)e.GetPosition(mainCanvas).Y); ;

            //Ellipse cirkel = new Ellipse();
            //cirkel.Fill = Brushes.Red;
            //cirkel.Width = 5;
            //cirkel.Height = 5;

            //Canvas.SetLeft(cirkel, coord.X);
            //Canvas.SetTop(cirkel, coord.Y);
            //mainCanvas.Children.Add(cirkel);

            Color color = Util.GetColor(_bitmap, coord);
            if (Util.IsWhite(color))
            {
                MessageBox.Show("Wit!");
            }
            else
            {
                MessageBox.Show("Niet-Wit!");
            }

            _bitmap = _bitmap.Clone(new Rectangle(0, 0, 50, 50), System.Drawing.Imaging.PixelFormat.DontCare);
            SetCanvas(_bitmap);

        }


        private void SetCanvas(Bitmap bitmap)
        {
            BitmapImage bitmapImage = Util.BitMap2BitmapImage(bitmap);
            mainCanvas.Background = new ImageBrush(bitmapImage);
            mainCanvas.Width = bitmapImage.Width;
            mainCanvas.Height = bitmapImage.Height;
        }


     


      


       

    }
}
