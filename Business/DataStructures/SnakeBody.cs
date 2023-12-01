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
    public class SnakeBody : GameObject
    {
        public override Brush Color { get => Brushes.Green; }

        public override Shape Shape => Shape.Ellipse;

        public SnakeBody(int X, int Y, int Width = 1, int Height = 1) : base(X, Y, Width, Height)
        { }

    }
}
