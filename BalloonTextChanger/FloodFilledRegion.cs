﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace BalloonTextChanger
{
    public class FloodFilledRegion
    {

        public List<Coordinate> Flooded { get; set; }
        public int Left { get; private set; }
        public int Top { get; private set; }
        public int Right { get; private set; }
        public int Down { get; private set; }

        public Coordinate Average { get; private set; }


        public FloodFilledRegion(Coordinate startCoordinate, Coordinate[,] coords)
        {
            Stack<Coordinate> workStack = new Stack<Coordinate>();
            workStack.Push(startCoordinate);
            FloodFill(coords, workStack);
            Flooded = GetFlooded(coords);
            GetBorders(Flooded);
            GetRemnantsWithinBorders(coords);
            Average = Coordinate.Average(Flooded);
        }


        private void FloodFill(Coordinate[,] coords, Stack<Coordinate> workStack)
        {
            while (workStack.Count > 0)
            {
                Coordinate current = workStack.Pop();
                if (coords[current.X, current.Y].FloodFillStatus == Enumerations.FloodFillStatus.Suitable)
                {
                    coords[current.X, current.Y].FloodFillStatus = Enumerations.FloodFillStatus.Yes;

                    foreach (Coordinate neighbour in current.Neigbours(coords))
                    {
                        workStack.Push(neighbour);
                    }
                }
            }
        }


        private List<Coordinate> GetFlooded(Coordinate[,] coords)
        {
            List<Coordinate> floodFilled = new List<Coordinate>();
            for (int x = 0; x < coords.GetLength(0); x++)
            {
                for (int y = 0; y < coords.GetLength(1); y++)
                {
                    {
                        if (coords[x, y].FloodFillStatus == Enumerations.FloodFillStatus.Yes)
                        {
                            floodFilled.Add(coords[x, y]);
                        }
                    }
                }
            }
            return floodFilled;
        }

        public void GetBorders(List<Coordinate> flooded)
        {
            Left = flooded.Min(c => c.X);
            Top = flooded.Min(c => c.Y);
            Right = flooded.Max(c => c.X);
            Down = flooded.Max(c => c.Y);
        }


        private void GetRemnantsWithinBorders(Coordinate[,] coords)
        {
            List<Coordinate> allCoords = coords.Cast<Coordinate>().ToList();
            allCoords = allCoords.FindAll(c => c.FloodFillStatus != Enumerations.FloodFillStatus.Yes);
            allCoords = allCoords.FindAll(c => c.FloodFillStatus != Enumerations.FloodFillStatus.Yes && c.X > Left && c.X < Right && c.Y > Top && c.Y < Down);
            List<Coordinate> remnants = new List<Coordinate>();

            Parallel.ForEach(allCoords, (c) =>
            {
                if (c.SomethingLeft(Flooded) && c.SomethingRight(Flooded) && c.SomethingTop(Flooded) && c.SomethingBottom(Flooded))
                {
                    coords[c.X, c.Y].FloodFillStatus = Enumerations.FloodFillStatus.Yes;
                    remnants.Add(c);
                }
            }
            );

            Flooded.AddRange(remnants);
        }




    }
}
