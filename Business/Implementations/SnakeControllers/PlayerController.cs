using Business.DataStructures.Snakes;
using Business.Interfaces;
using Business.Ultilities;
using System.Numerics;
using static Business.Enums.Enums;

namespace Business.Implementations.SnakeControllers
{
    public class PlayerController : ISnakeController
    {
        public static PlayerController Instance => GetInstance();

        private static PlayerController _Instance;

        public Direction Direction { get; set; }

        public event Action<Direction> OnDirectionChanged;


        protected PlayerController() { }

        public static PlayerController GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new PlayerController();
            }
            return _Instance;
        }


        public void HandleInput(Direction NewDirection)
        {
            Vector2 NewDir = NewDirection.ToVector();
            Vector2 CurrentDir = Direction.ToVector();
            if (NewDir.X * CurrentDir.X + NewDir.Y * CurrentDir.Y != 0) return;
            this.Direction = NewDirection;
            OnDirectionChanged?.Invoke(this.Direction);
        }

        public Direction NextDirection(Snake Snake)
        {
            return this.Direction;
        }
    }
}