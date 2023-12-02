using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.DataStructures.Abstracts;
using Business.DataStructures.Snakes;

namespace Business.DataStructures.Maps
{
    public class Map
    {
        public Dictionary<(int, int), Obstacle> Obstacles { get; private set; }

        public Dictionary<(int, int), Floor> ActiveFloors { get; private set; }

        public Dictionary<(int, int), Floor> InactiveFloors { get; private set; }

        public List<GameObject> GameObjects => GetGameObjects();

        public Map(List<Obstacle> Obstacles, List<Floor> Floors)
        {
            this.Obstacles = new Dictionary<(int, int), Obstacle>();
            foreach (Obstacle Obstacle in Obstacles)
            {
                this.Obstacles.TryAdd((Obstacle.X, Obstacle.Y), Obstacle);
            }


            ActiveFloors = new Dictionary<(int, int), Floor>();
            foreach (Floor Floor in Floors)
            {
                ActiveFloors.TryAdd((Floor.X, Floor.Y), Floor);
            }

            InactiveFloors = new Dictionary<(int, int), Floor>();

            GameManager.Instance.OnSnakeGenerateEvent += OnSnakeGenerate;
            GameManager.Instance.OnFoodGenerateEvent += this.OnFoodGenerate;
        }

        public void OnSnakeGenerate(Snake Snake)
        {
            Floor HeadPositionFloor = ActiveFloors[(Snake.Head.X, Snake.Head.Y)];
            ActiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            InactiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            foreach (SnakeBody Body in Snake.Bodies)
            {
                Floor BodyPositionFloor = ActiveFloors[(Body.X, Body.Y)];
                ActiveFloors.Remove((Body.X, Body.Y));
                InactiveFloors.Add((Body.X, Body.Y), BodyPositionFloor);
            }

            Snake.OnGrowEvent += OnSnakeGrow;
            Snake.OnMoveEvent += OnSnakeMove;
            Snake.OnDieEvent += OnSnakeDie;
        }

        public void OnSnakeMove(Snake Snake, GameObject OldTail)
        {
            if (!ActiveFloors.ContainsKey((Snake.Head.X, Snake.Head.Y)))
            {
                return;
            }

            Floor HeadPositionFloor = ActiveFloors[(Snake.Head.X, Snake.Head.Y)];
            ActiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            InactiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            Floor OldTailPositionFloor = InactiveFloors[(OldTail.X, OldTail.Y)];
            InactiveFloors?.Remove((OldTail.X, OldTail.Y));
            ActiveFloors.Add((OldTail.X, OldTail.Y), OldTailPositionFloor);
        }

        public void OnSnakeGrow(Snake Snake)
        {
            SnakeBody Tail = Snake.Bodies[Snake.Bodies.Count - 1];
            Floor TailPositionFloor = ActiveFloors[(Tail.X, Tail.Y)];
            ActiveFloors.Remove((Tail.X, Tail.Y));
            InactiveFloors.Add((Tail.X, Tail.Y), TailPositionFloor);
        }

        public void OnSnakeDie(Snake Snake)
        {
            Floor HeadPositionFloor = InactiveFloors[(Snake.Head.X, Snake.Head.Y)];
            InactiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            ActiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            Snake.Bodies.ForEach(Body =>
            {
                Floor BodyPositionFloor = InactiveFloors[(Body.X, Body.Y)];
                InactiveFloors.Remove((Body.X, Body.Y));
                ActiveFloors.Add((Body.X, Body.Y), BodyPositionFloor);
            });
        }

        public void OnFoodGenerate(Food Food)
        {
            Floor FoodPositionFloor = ActiveFloors[(Food.X, Food.Y)];
            ActiveFloors.Remove((Food.X, Food.Y));
            InactiveFloors.Add((Food.X, Food.Y), FoodPositionFloor);

            Food.OnFoodEatenEvent += OnFoodEaten;
        }

        public void OnFoodEaten(Food Food)
        {
            Floor FoodPositionFloor = InactiveFloors[(Food.X, Food.Y)];
            InactiveFloors.Remove((Food.X, Food.Y));
            ActiveFloors.Add((Food.X, Food.Y), FoodPositionFloor);
        }

        public List<GameObject> GetGameObjects()
        {
            List<GameObject> Objects = new List<GameObject>();
            Objects.AddRange(ActiveFloors.Values);
            Objects.AddRange(Obstacles.Values);
            return Objects;
        }
    }
}
