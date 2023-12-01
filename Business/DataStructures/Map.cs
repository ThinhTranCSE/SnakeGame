using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DataStructures
{
    public class Map
    {
        public Dictionary<(int, int), Obstacle> Obstacles { get; private set; }
       
        public Dictionary<(int, int), Floor> ActiveFloors { get; private set; }

        public Dictionary<(int, int), Floor> InactiveFloors { get; private set; }

        public List<GameObject> GameObjects => this.GetGameObjects();

        public Map(List<Obstacle> Obstacles, List<Floor> Floors)
        {
            this.Obstacles = new Dictionary<(int, int), Obstacle>();
            foreach(Obstacle Obstacle in Obstacles)
            {
                this.Obstacles.TryAdd((Obstacle.X, Obstacle.Y), Obstacle);
            }


            this.ActiveFloors = new Dictionary<(int, int), Floor>();
            foreach(Floor Floor in Floors)
            {
                this.ActiveFloors.TryAdd((Floor.X, Floor.Y), Floor);
            }

            this.InactiveFloors = new Dictionary<(int, int), Floor>();

            GameManager.Instance.OnSnakeGenerateEvent += this.OnSnakeGenerate;
            GameManager.Instance.OnFoodGenerateEvent += this.OnFoodGenerate;
        }

        public void OnSnakeGenerate(Snake Snake)
        {
            Floor HeadPositionFloor = this.ActiveFloors[(Snake.Head.X, Snake.Head.Y)];
            this.ActiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            this.InactiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            foreach(SnakeBody Body in Snake.Bodies)
            {
                Floor BodyPositionFloor = this.ActiveFloors[(Body.X, Body.Y)];
                this.ActiveFloors.Remove((Body.X, Body.Y));
                this.InactiveFloors.Add((Body.X, Body.Y), BodyPositionFloor);
            }

            Snake.OnGrowEvent += this.OnSnakeGrow;
            Snake.OnMoveEvent += this.OnSnakeMove;
            Snake.OnDieEvent += this.OnSnakeDie;
        }
        
        public void OnSnakeMove(Snake Snake, GameObject OldTail)
        {
            if(!this.ActiveFloors.ContainsKey((Snake.Head.X, Snake.Head.Y)))
            {
                return;
            }

            Floor HeadPositionFloor = this.ActiveFloors[(Snake.Head.X, Snake.Head.Y)];
            this.ActiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            this.InactiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            Floor OldTailPositionFloor = this.InactiveFloors[(OldTail.X, OldTail.Y)];
            this.InactiveFloors?.Remove((OldTail.X, OldTail.Y));
            ActiveFloors.Add((OldTail.X, OldTail.Y), OldTailPositionFloor);
        }

        public void OnSnakeGrow(Snake Snake)
        {
            SnakeBody Tail = Snake.Bodies.Last();
            Floor TailPositionFloor = this.ActiveFloors[(Tail.X, Tail.Y)];
            this.ActiveFloors.Remove((Tail.X, Tail.Y));
            this.InactiveFloors.Add((Tail.X, Tail.Y), TailPositionFloor);
        }

        public void OnSnakeDie(Snake Snake)
        {
            Floor HeadPositionFloor = this.InactiveFloors[(Snake.Head.X, Snake.Head.Y)];
            this.InactiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            this.ActiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            Snake.Bodies.ForEach(Body =>
            {
                Floor BodyPositionFloor = this.InactiveFloors[(Body.X, Body.Y)];
                this.InactiveFloors.Remove((Body.X, Body.Y));
                this.ActiveFloors.Add((Body.X, Body.Y), BodyPositionFloor);
            });
        }

        public void OnFoodGenerate(Food Food)
        {
            Floor FoodPositionFloor = this.ActiveFloors[(Food.X, Food.Y)];
            this.ActiveFloors.Remove((Food.X, Food.Y));
            this.InactiveFloors.Add((Food.X, Food.Y), FoodPositionFloor);

            Food.OnFoodEatenEvent += this.OnFoodEaten;
        }

        public void OnFoodEaten(Food Food)
        {
            Floor FoodPositionFloor = this.InactiveFloors[(Food.X, Food.Y)];
            this.InactiveFloors.Remove((Food.X, Food.Y));
            this.ActiveFloors.Add((Food.X, Food.Y), FoodPositionFloor);
        }

        public List<GameObject> GetGameObjects()
        {
            List<GameObject> Objects = new List<GameObject>();
            Objects.AddRange(this.ActiveFloors.Values);
            Objects.AddRange(this.Obstacles.Values);
            return Objects;
        }
    }
}
