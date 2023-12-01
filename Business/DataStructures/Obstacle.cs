using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.DataStructures
{
    public class Obstacle : GameObject
    {
        public override Brush Color => Brushes.Blue;

        public override Shape Shape => Shape.Rectangle;
        public Obstacle(int X, int Y, int Width = 1, int Height = 1) : base(X, Y, Width, Height)
        {
        }
    }
}
