using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace BalloonTextChanger
{
    public class Util
    {

        public static Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new System.Drawing.Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }

        public static BitmapImage BitMap2BitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Png);
                memory.Position = 0;
                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
                return bitmapImage;
            }
        }


        public static Color GetColor(Bitmap bitmap, Coordinate coord)
        {
            Color color = bitmap.GetPixel((int)coord.X, (int)coord.Y);
            return color;
        }


        public static bool IsWhite(Bitmap bitmap, int x, int y)
        {
            Color color = bitmap.GetPixel(x, y);
            return IsWhite(color);
        }


        public static bool IsWhite(Color color)
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


        public static Coordinate[,] PixelsToCoordinates(Bitmap bitmap)
        {
            Coordinate[,] coords = new Coordinate[bitmap.Width, bitmap.Height];

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    {
                        Coordinate coordinate = new Coordinate(x, y, bitmap);
                        coords[x, y] = coordinate;
                    }
                }
            }
            return coords;
        }

        public static void ColorBitmapRegion(Bitmap bitmap, FloodFilledRegion region)
        {
            foreach (Coordinate coordinate in region.Flooded)
            {
                bitmap.SetPixel(coordinate.X, coordinate.Y, Color.White);
            }
        }

    }
}
