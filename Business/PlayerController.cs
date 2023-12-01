using Business.Interfaces;
using static Business.Enums.Enums;

namespace Business
{
    public class PlayerController : ISnakeController
    {
        public static PlayerController Instance => GetInstance();
        
        private static PlayerController _Instance;

        public event Action<Direction> OnDirectionChanged;


        protected PlayerController() { }

        public static PlayerController GetInstance()
        {
            if (PlayerController._Instance == null)
            {
                PlayerController._Instance = new PlayerController();
            }
            return PlayerController._Instance;
        }


        public void HandleInput(Direction Direction)
        {
            OnDirectionChanged?.Invoke(Direction);
        }
    }
}