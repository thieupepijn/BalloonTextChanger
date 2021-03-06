﻿using Microsoft.Win32;
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
        Coordinate[,] _allCoordinates;
        int _fontSize = 8;

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
                _bitmap = new Bitmap(dialog.FileName);
                _allCoordinates = Util.PixelsToCoordinates(_bitmap);
                SetCanvas(_bitmap);
            }
        }

        private void mainCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Coordinate clickedCoordinate = new Coordinate((int)e.GetPosition(mainCanvas).X, (int)e.GetPosition(mainCanvas).Y, _bitmap);
            InputBox inputbox = new InputBox(_fontSize);
            if ((clickedCoordinate.FloodFillStatus == Enumerations.FloodFillStatus.Suitable) && (inputbox.ShowDialog().Value))
            {
                FloodFilledRegion region = new FloodFilledRegion(clickedCoordinate, _allCoordinates);
                UtilDraw.ColorBitmapRegion(_bitmap, region);
                Util.ResetFloodedCoords(_allCoordinates, _bitmap);
                _fontSize = inputbox.TextSize;
                UtilDraw.WriteText(_bitmap, region.Average, inputbox.Text, _fontSize);
             //   UtilDraw.DrawCircle(_bitmap, region.Average);
                SetCanvas(_bitmap);
            }
        }
        

        private void SetCanvas(Bitmap bitmap)
        {
            BitmapImage bitmapImage = Util.BitMap2BitmapImage(bitmap);
            mainCanvas.Background = new ImageBrush(bitmapImage);
            mainCanvas.Width = bitmapImage.Width;
            mainCanvas.Height = bitmapImage.Height;
        }

        private void btnSaveImage_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "JPG | *.jpg";
            dialog.DefaultExt = "*.jpg";
            bool? dialogResult = dialog.ShowDialog();


            if (dialogResult.HasValue && dialogResult.Value)
            {
                _bitmap.Save(dialog.FileName);
            }
        }
    }
}
