using Business.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.Ultilities
{
    public class RandomWalker
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Direction Direction { get; set;} 
        public (int, int) Position => (this.X, this.Y);


        public RandomWalker(int X, int Y, Direction Direction)
        {
            this.X = X;
            this.Y = Y;
            this.Direction = Direction;
        }

        
    }
}
