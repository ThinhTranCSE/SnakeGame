using Business.DataStructures.Abstracts;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.DataStructures.Snakes
{
    public class SnakeHead : GameObject
    {
        public override Brush Color => Brushes.Black;

        public override Shape Shape => Shape.Ellipse;

        public SnakeHead(int X, int Y, int Width = 1, int Height = 1) : base(X, Y, Width, Height)
        { }

    }
}
