﻿using System;
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

        public Dictionary<(int, int), Floor> Floors { get; private set; }

        public Dictionary<(int, int), Floor> ActiveFloors => Floors.Where(Floor => Floor.Value.IsAvailable).ToDictionary(Floor => Floor.Key, Floor => Floor.Value);

        public Dictionary<(int, int), Floor> InactiveFloors => Floors.Where(Floor => !Floor.Value.IsAvailable).ToDictionary(Floor => Floor.Key, Floor => Floor.Value);

        public List<GameObject> GameObjects => GetGameObjects();

        public Map(List<Obstacle> Obstacles, List<Floor> Floors)
        {
            this.Obstacles = new Dictionary<(int, int), Obstacle>();
            foreach (Obstacle Obstacle in Obstacles)
            {
                this.Obstacles.TryAdd((Obstacle.X, Obstacle.Y), Obstacle);
            }

            this.Floors = new Dictionary<(int, int), Floor>();
            foreach (Floor Floor in Floors)
            {
                this.Floors.TryAdd((Floor.X, Floor.Y), Floor);
            }

            GameManager.Instance.OnSnakeGenerateEvent += OnSnakeGenerate;
            GameManager.Instance.OnFoodGenerateEvent += this.OnFoodGenerate;
        }

        public void OnSnakeGenerate(Snake Snake)
        {
            Floor HeadPositionFloor = this.Floors[(Snake.Head.X, Snake.Head.Y)];
            HeadPositionFloor.IsAvailable = false;
            //ActiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            //InactiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            foreach (SnakeBody Body in Snake.Bodies)
            {
                Floor BodyPositionFloor = ActiveFloors[(Body.X, Body.Y)];
                BodyPositionFloor.IsAvailable = false;
                //ActiveFloors.Remove((Body.X, Body.Y));
                //InactiveFloors.Add((Body.X, Body.Y), BodyPositionFloor);
            }

            Snake.OnGrowEvent += OnSnakeGrow;
            Snake.OnMoveEvent += OnSnakeMove;
            Snake.OnDieEvent += OnSnakeDie;
        }

        public void OnSnakeMove(Snake Snake, (int, int) OldTailPosition)
        {
            Floor HeadPositionFloor = this.Floors[(Snake.Head.X, Snake.Head.Y)];
            HeadPositionFloor.IsAvailable = false;
            //ActiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            //InactiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            Floor OldTailPositionFloor = this.Floors[OldTailPosition];
            OldTailPositionFloor.IsAvailable = true;
            //InactiveFloors?.Remove(OldTailPosition);
            //ActiveFloors.Add(OldTailPosition, OldTailPositionFloor);
        }

        public void OnSnakeGrow(Snake Snake)
        {
            SnakeBody Tail = Snake.Bodies[Snake.Bodies.Count - 1];
            Floor TailPositionFloor = this.Floors[(Tail.X, Tail.Y)];
            TailPositionFloor.IsAvailable = false;
            //ActiveFloors.Remove((Tail.X, Tail.Y));
            //InactiveFloors.Add((Tail.X, Tail.Y), TailPositionFloor);
        }

        public void OnSnakeDie(Snake Snake)
        {
            Floor HeadPositionFloor = this.Floors[(Snake.Head.X, Snake.Head.Y)];
            HeadPositionFloor.IsAvailable = true;
            //InactiveFloors.Remove((Snake.Head.X, Snake.Head.Y));
            //ActiveFloors.Add((Snake.Head.X, Snake.Head.Y), HeadPositionFloor);

            Snake.Bodies.ForEach(Body =>
            {
                Floor BodyPositionFloor = this.Floors[(Body.X, Body.Y)];
                BodyPositionFloor.IsAvailable = true;
                //InactiveFloors.Remove((Body.X, Body.Y));
                //ActiveFloors.Add((Body.X, Body.Y), BodyPositionFloor);
            });
        }

        public void OnFoodGenerate(Food Food)
        {
            Floor FoodPositionFloor = this.Floors[(Food.X, Food.Y)];
            FoodPositionFloor.IsAvailable = false;
            //ActiveFloors.Remove((Food.X, Food.Y));
            //InactiveFloors.Add((Food.X, Food.Y), FoodPositionFloor);

            Food.OnFoodEatenEvent += OnFoodEaten;
        }

        public void OnFoodEaten(Food Food)
        {
            Floor FoodPositionFloor = this.Floors[(Food.X, Food.Y)];
            FoodPositionFloor.IsAvailable = true;
            //InactiveFloors.Remove((Food.X, Food.Y));
            //ActiveFloors.Add((Food.X, Food.Y), FoodPositionFloor);
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
