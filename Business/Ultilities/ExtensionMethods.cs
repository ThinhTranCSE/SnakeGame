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

        public static Direction ToDirection(this Vector2 Vector)
        {
            if (Vector == new Vector2(0, -1))
            {
                return Direction.Up;
            }
            else if (Vector == new Vector2(0, 1))
            {
                return Direction.Down;
            }
            else if (Vector == new Vector2(-1, 0))
            {
                return Direction.Left;
            }
            else if (Vector == new Vector2(1, 0))
            {
                return Direction.Right;
            }
            return Direction.Up;
        }   

        public static bool IsOpposite(this Direction Direction1, Direction Direction2)
        {
            return Direction1.ToVector() == -Direction2.ToVector();
        }   
        public static List<Vector2> NormalVectors(this Vector2 Vector)
        {
            List<Vector2> Result = new List<Vector2>() { new Vector2(Vector.Y, -Vector.X), new Vector2(-Vector.Y, Vector.X)};
            return Result;
        }
    }
}
