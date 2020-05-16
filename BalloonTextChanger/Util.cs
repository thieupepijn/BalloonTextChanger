using System;
using System.Collections.Generic;
using System.Drawing;
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
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        public static Color GetColor(Bitmap bitmap, Coordinate coord)
        {
            Color color = bitmap.GetPixel((int)coord.X, (int)coord.Y);
            return color;
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

    }
}
