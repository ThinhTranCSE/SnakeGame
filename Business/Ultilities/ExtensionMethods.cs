using Business.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.Ultilities
{
    public static class ExtensionMethods
    {
        public static Vector2 ToVector(this Direction Direction)
        {
            switch (Direction)
            {
                case Direction.Up:
                    return new Vector2(0, -1);
                case Direction.Down:
                    return new Vector2(0, 1);
                case Direction.Left:
                    return new Vector2(-1, 0);
                case Direction.Right:
                    return new Vector2(1, 0);
            }
            return new Vector2(0, 0);
        }
    }
}
