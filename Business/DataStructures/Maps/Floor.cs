using Business.DataStructures.Abstracts;
using Business.DataStructures.Snakes;
using Business.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.DataStructures.Maps
{
    public class Floor : GameObject
    {

        public override Brush Color => IsAvailable ? Brushes.White : Brushes.Yellow;

        public override Shape Shape => Shape.Rectangle;

        public bool IsAvailable { get; set; } = true;

        public Floor(int X, int Y) : base(X, Y)
        {
        }

        public override void ColisionEffect(Snake Snake)
        {
            return;
        }
    }
}
