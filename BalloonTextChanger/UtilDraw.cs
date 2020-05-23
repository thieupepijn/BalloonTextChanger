using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BalloonTextChanger
{
    public class UtilDraw
    {

        public static void ColorBitmapRegion(Bitmap bitmap, FloodFilledRegion region)
        {
            foreach (Coordinate coordinate in region.Flooded)
            {
                bitmap.SetPixel(coordinate.X, coordinate.Y, Color.White);
            }
        }

        public static void WriteText(Bitmap bitmap, Coordinate coordinate, string text, int fontSize)
        {
            using(Graphics graafix = Graphics.FromImage(bitmap))
            {
                Font font = new Font("Comic Sans MS", fontSize, FontStyle.Italic);
                graafix.DrawString(text, font,  Brushes.Black, new PointF(coordinate.X, coordinate.Y));
            }
        }

    }
}
