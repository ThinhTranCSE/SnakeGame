using Business.Interfaces;
using Business.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.DataStructures
{
    public class Snake : IDisposable
    {
        public List<SnakeBody> Bodies { get; private set; }
        public SnakeHead Head { get; private set; }

        public int Length => this.Bodies.Count + 1;

        public List<GameObject> GameObjects => this.GetGameObjects();   
        public Direction Direction { get; private set; }
        private ISnakeController Controller { get; set; }

        public event Action<Snake, GameObject> OnMoveEvent;

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
                        this.Head = new SnakeHead(HeadPosition.Item1, HeadPosition.Item2);
                        this.Bodies = new List<SnakeBody>() { new SnakeBody(XBodyPosition, YBodyPosition), new SnakeBody(XTailPosition, YTailPosition) };
                        this.Direction = ChoosenDirection;
                        IsGenerated = true;
                        break;
                    }
                    DirectionOptions.Remove(ChoosenDirection);
                }
            }



            GameManager.Instance.OnUpdateEvent += this.OnUpdate;
            this.Controller = Controller;
            this.Controller.OnDirectionChanged += this.ChangeDirection;
        }
        
        public List<GameObject> GetGameObjects()
        {
            List<GameObject> Objects = this.Bodies.Cast<GameObject>().ToList();
            Objects.Add(this.Head);
            return Objects;
        }
        public void Move()
        {
            Vector2 Dir = this.Direction.ToVector();

            this.CheckNextStepColision((this.Head.X + (int)Dir.X, this.Head.Y + (int)Dir.Y));

            SnakeBody OldTail = this.Bodies[this.Bodies.Count - 1];
            this.Bodies.RemoveAt(this.Bodies.Count - 1);
            this.Bodies.Insert(0, new SnakeBody(this.Head.X, this.Head.Y));
            this.Head.X += (int)Dir.X;
            this.Head.Y += (int)Dir.Y;

            this.OnMoveEvent?.Invoke(this, OldTail);
        }

        private void CheckNextStepColision((int, int) NextPosition)
        {
            if (GameManager.Instance.EntitiesDictionary.ContainsKey(NextPosition))
            {
                GameManager.Instance.EntitiesDictionary[NextPosition].ColisionEffect(this);
            }
        }
        public void ChangeDirection(Direction NewDirection)
        {
            Vector2 NewDir = NewDirection.ToVector();
            Vector2 CurrentDir = this.Direction.ToVector();
            if (NewDir.X * CurrentDir.X + NewDir.Y * CurrentDir.Y != 0) return;
            this.Direction = NewDirection;
        }

        public void Grow()
        {
            SnakeBody Tail = this.Bodies[this.Bodies.Count - 1];
            SnakeBody LastBody = this.Bodies[this.Bodies.Count - 2];
            int X = Tail.X + (Tail.X - LastBody.X);
            int Y = Tail.Y + (Tail.Y - LastBody.Y);
            this.Bodies.Add(new SnakeBody(X, Y));
            
            this.OnGrowEvent?.Invoke(this);
        }
        public void Die()
        {
            this.OnDieEvent?.Invoke(this);
            this.Dispose();
        }

        public void OnUpdate()
        {
            this.Move();
        }
        public void Dispose()
        {
            GameManager.Instance.OnUpdateEvent -= this.OnUpdate;
        }

    }
}
