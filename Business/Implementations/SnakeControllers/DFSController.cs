using Business.DataStructures.Maps;
using Business.DataStructures.Snakes;
using Business.Interfaces;
using Business.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.Implementations.SnakeControllers
{
    public class DFSController : ISnakeController
    {
        public event Action<Direction> OnDirectionChanged;

        public Direction Direction { get; set; }

        public DFSController()
        {
            this.Direction = Direction.Up;
        }
        public Direction NextDirection(Snake Snake)
        {
            Food Food = GameManager.Instance.Foods[0];
            List<Direction>? Path = FindDSFPath(Snake, Food);
            return Path?[0] ?? Snake.Direction;
        }

        public List<Direction>? FindDSFPath(Snake Snake, Food Food)
        {
            Stack<FindPathState> Stack = new Stack<FindPathState>();
            HashSet<((int, int), Direction)> Visited = new HashSet<((int, int), Direction)>();
            Stack.Push(new FindPathState(Snake.Head.Position, Snake.Direction, null));

            while (Stack.Count > 0)
            {
                FindPathState CurrentState = Stack.Pop();
                if (CurrentState.Position == Food.Position)
                {
                    return GetPath(CurrentState);
                }
                if (Visited.Contains(CurrentState.Key)) continue;
                Visited.Add(CurrentState.Key);

                List<Direction> Directions = GetValidDirections(CurrentState.Position, CurrentState.Direction, Food);
                foreach (Direction Direction in Directions)
                {
                    Vector2 DirectionVector = Direction.ToVector();
                    (int, int) NextPosition = (CurrentState.Position.Item1 + (int)DirectionVector.X, CurrentState.Position.Item2 + (int)DirectionVector.Y);
                    Stack.Push(new FindPathState(NextPosition, Direction, CurrentState));
                }
            }
            return null;
        }

        private List<Direction> GetPath(FindPathState State)
        {
            List<Direction> Result = new List<Direction>();
            while (State.Parent != null)
            {
                Result.Add(State.Direction);
                State = State.Parent;
            }
            Result.Reverse();
            return Result;
        }

        private List<Direction> GetValidDirections((int, int) Position, Direction Direction, Food Food)
        {
            List<Direction> Result = new List<Direction>();
            List<Direction> Directions = new List<Direction>() { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
            foreach(Direction Dir in Directions)
            {
                Vector2 DirVector = Dir.ToVector();
                (int, int) NextPosition = (Position.Item1 + (int)DirVector.X, Position.Item2 + (int)DirVector.Y);
                if(Heuristic(NextPosition, Food) < Heuristic(Position, Food))
                {
                    continue;
                }
                Floor? NextPositionFloor;
                GameManager.Instance.Map.Floors.TryGetValue(NextPosition, out NextPositionFloor);
                if (GameManager.Instance.Foods.Any(Food => Food.Position == NextPosition) || (NextPositionFloor?.IsAvailable ?? false))
                {
                    Result.Add(Dir);
                }
            }
            return Result;
        }

        private double Heuristic((int, int) Position, Food Food)
        {
            return - (Math.Pow(Position.Item1 - Food.Position.Item1, 2) + Math.Pow(Position.Item2 - Food.Position.Item2, 2));
        }
        private class FindPathState
        {
            public (int, int) Position { get; set; }
            public Direction Direction { get; set; }

            public ((int, int), Direction) Key => (Position, Direction);
            public FindPathState? Parent { get; set; }
            public FindPathState((int, int) Position, Direction Direction, FindPathState? Parent)
            {
                this.Position = Position;
                this.Direction = Direction;
                this.Parent = Parent;
            }
        }
    }

    
}
