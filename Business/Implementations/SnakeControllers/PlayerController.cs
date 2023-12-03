using Business.Interfaces;
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


        public void HandleInput(Direction Direction)
        {
            this.Direction = Direction;
            OnDirectionChanged?.Invoke(Direction);
        }

        public Direction NextDirection()
        {
            return this.Direction;
        }
    }
}