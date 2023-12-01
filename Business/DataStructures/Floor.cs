﻿using Business.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.DataStructures
{
    public class Floor : GameObject
    {

        public override Brush Color => Brushes.White;

        public override Shape Shape => Shape.Rectangle;

        public Floor(int X, int Y) : base(X, Y)
        {
        }

        public override void ColisionEffect(Snake Snake)
        {
            return;
        }
    }
}
