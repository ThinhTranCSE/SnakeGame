using Business.DataStructures.Abstracts;
using Business.Interfaces;
using Business.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.DataStructures.Snakes
{
    public class Snake : IDisposable
    {
        public List<SnakeBody> Bodies { get; private set; }
        public SnakeHead Head { get; private set; }

        public int Length => Bodies.Count + 1;

        public int PendingBodies { get; set; } = 0;
        public List<GameObject> GameObjects => GetGameObjects();
        public Direction Direction { get; private set; }
        private ISnakeController Controller { get; set; }

        public event Action<Snake, (int, int)> OnMoveEvent;

        public event Action<Snake> OnGrowEvent;

        public event Action<Snake> OnDieEvent;



        public Snake(ISnakeController Controller)
        {
            bool IsGenerated = false;
            while (!IsGenerated)
            {
                (int, int) HeadPosition = Randomizer.Instance.GetValidRandomPosition();
                List<Direction> DirectionOptions = new List<Direction>() { Direction.Up, Direction.Down, Direction.Left, Direction.Right };
                while (DirectionOptions.Count > 0)
                {
                    Direction ChoosenDirection = DirectionOptions[Randomizer.Instance.GetRandomNumber(0, DirectionOptions.Count - 1)];
                    Vector2 DirectionVector = ChoosenDirection.ToVector();

                    int XBodyPosition = HeadPosition.Item1 - (int)DirectionVector.X;
                    int YBodyPosition = HeadPosition.Item2 - (int)DirectionVector.Y;

                    int XTailPosition = XBodyPosition - (int)DirectionVector.X;
                    int YTailPosition = YBodyPosition - (int)DirectionVector.Y;

                    if (
                        GameManager.Instance.Map.ActiveFloors.ContainsKey((XBodyPosition, YBodyPosition)) &&
                        GameManager.Instance.Map.ActiveFloors.ContainsKey((XTailPosition, YTailPosition))
                       )
                    {
                        Head = new SnakeHead(HeadPosition.Item1, HeadPosition.Item2);
                        Bodies = new List<SnakeBody>() { new SnakeBody(XBodyPosition, YBodyPosition), new SnakeBody(XTailPosition, YTailPosition) };
                        Direction = ChoosenDirection;
                        IsGenerated = true;
                        break;
                    }
                    DirectionOptions.Remove(ChoosenDirection);
                }
            }



            GameManager.Instance.OnUpdateEvent += OnUpdate;
            this.Controller = Controller;
        }

        public List<GameObject> GetGameObjects()
        {
            List<GameObject> Objects = Bodies.Cast<GameObject>().ToList();
            Objects.Add(Head);
            return Objects;
        }
        public void Move()
        {
            try
            {
                
                this.Direction = this.Controller.NextDirection(this);
                Vector2 Dir = Direction.ToVector();

                GameObject? CollidedObject = CollidedGameObject((Head.X + (int)Dir.X, Head.Y + (int)Dir.Y));
                CollidedObject?.ColisionEffect(this);

                (int, int) OldTailPosition = Bodies[Bodies.Count - 1].Position;

                Bodies.RemoveAt(Bodies.Count - 1);
                Bodies.Insert(0, new SnakeBody(Head.X, Head.Y));
                Head.X += (int)Dir.X;
                Head.Y += (int)Dir.Y;


                OnMoveEvent?.Invoke(this, OldTailPosition);

                if (PendingBodies > 0)
                {
                    Grow();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private GameObject? CollidedGameObject((int, int) NextPosition)
        {
            if (GameManager.Instance.EntitiesDictionary.ContainsKey(NextPosition))
            {
                return GameManager.Instance.EntitiesDictionary[NextPosition];
            }
            return null;
        }

        public void EatFood()
        {
            PendingBodies++;
        }

        public void Grow()
        {
            SnakeBody Tail = Bodies[Bodies.Count - 1];
            SnakeBody LastBody = Bodies[Bodies.Count - 2];
            int X = Tail.X + (Tail.X - LastBody.X);
            int Y = Tail.Y + (Tail.Y - LastBody.Y);

            if(!GameManager.Instance.Map.Floors.ContainsKey((X, Y)))
            {
                return;
            }

            Bodies.Add(new SnakeBody(X, Y));
            PendingBodies--;

            OnGrowEvent?.Invoke(this);
        }
        public void Die()
        {
            OnDieEvent?.Invoke(this);
            Dispose();
        }

        public void OnUpdate()
        {
            Move();
        }
        public void Dispose()
        {
            GameManager.Instance.OnUpdateEvent -= OnUpdate;
            throw new Exception();
        }

    }
}
