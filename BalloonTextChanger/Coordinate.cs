using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace BalloonTextChanger
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Enumerations.FloodFillStatus FloodFillStatus { get; set; }


        public Coordinate(int x, int y, Bitmap bitmap)
        {
            X = x;
            Y = y;

            if (Util.IsWhite(bitmap, X, Y))
            {
                FloodFillStatus = Enumerations.FloodFillStatus.Suitable;
            }
            else
            {
                FloodFillStatus = Enumerations.FloodFillStatus.NotSuitable;
            }
        }
    


        public Coordinate Left(Coordinate[,] coords)
        {
            if (X > 0)
            {
                return coords[X - 1, Y];
            }
            else
            {
                return null;
            }
        }


        public Coordinate Up(Coordinate[,] coords)
        {
            if (Y > 0)
            {
                return coords[X, Y - 1];
            }
            else
            {
                return null;
            }
        }

        public Coordinate Right(Coordinate[,] coords)
        {
            if (X < coords.GetLength(0) - 1)
            {
                return coords[X + 1, Y];
            }
            else
            {
                return null;
            }
        }

        public Coordinate Down(Coordinate[,] coords)
        {
            if (Y < coords.GetLength(1) - 1)
            {
                return coords[X, Y + 1];
            }
            else
            {
                return null;
            }
        }



        public List<Coordinate> Neigbours(Coordinate[,] coords)
        {
            List<Coordinate> neighbours = new List<Coordinate>();
            Coordinate left = Left(coords);
            Coordinate up = Up(coords);
            Coordinate right = Right(coords);
            Coordinate down = Down(coords);

            if (left != null)
            {
                neighbours.Add(left);
            }

            if (up != null)
            {
                neighbours.Add(up);
            }

            if (right != null)
            {
                neighbours.Add(right);
            }

            if (down != null)
            {
                neighbours.Add(down);
            }

            return neighbours;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            Coordinate other = (Coordinate)obj;
            if ((other.X == X) && (other.Y == Y))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(Coordinate coordinate1, Coordinate coordinate2)
        {
            if ((object.ReferenceEquals(coordinate1, null)) && (object.ReferenceEquals(coordinate2, null)))
            {
                return true;
            }
            else
            {
                return coordinate1.Equals(coordinate2);
            }
        }

        public static bool operator !=(Coordinate coordinate1, Coordinate coordinate2)
        {
            return !coordinate1.Equals(coordinate2);
        }



        public override int GetHashCode()
        {
            return new System.Drawing.Point(X, Y).GetHashCode();
        }





      


    }
}
