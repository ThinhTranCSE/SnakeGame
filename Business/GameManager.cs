﻿using Business.DataStructures.Abstracts;
using Business.DataStructures.Maps;
using Business.DataStructures.Snakes;
using Business.Implementations.SnakeControllers;
using Business.Interfaces;
using Business.Ultilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class GameManager : IGameManager
    {
        public static GameManager Instance => GetInstance();

        private static GameManager _Instance;

        public event Action OnUpdateEvent;

        public event Action<Snake> OnSnakeGenerateEvent;

        public event Action<Food> OnFoodGenerateEvent;

        public List<Snake> Snakes { get; private set; }
        public Map Map { get; private set; }
        public List<Food> Foods { get; private set; }

        public Dictionary<(int, int), GameObject> EntitiesDictionary { get; private set; }
        protected GameManager()
        {

        }

        public static GameManager GetInstance()
        {
            if (GameManager._Instance == null)
            {
                GameManager._Instance = new GameManager();
            }
            return GameManager._Instance;
        }
        public void Update()
        {
            if(this.Snakes.Count == 0)
            {
                return;
            }
            OnUpdateEvent?.Invoke();
            this.UpdateGameObjectDictionary();
        }

        public List<GameObject> GetGameObjects()
        {
            List<GameObject> GameObjects = new List<GameObject>(Map.Floors.Count + EntitiesDictionary.Count);
            GameObjects.AddRange(this.Map.Floors.Values.AsEnumerable<GameObject>());
            GameObjects.AddRange(this.EntitiesDictionary.Values.AsEnumerable<GameObject>());
            return GameObjects;
        }

        public void StartGame()
        {
            this.Snakes = new List<Snake>();

            this.Map = MapLoader.Instance.LoadMap();
            this.Foods = new List<Food>();

            this.EntitiesDictionary = new Dictionary<(int, int), GameObject>();

            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewSnake(new DFSController());
            this.GenerateNewFood();

            this.UpdateGameObjectDictionary();
        }

        private void UpdateGameObjectDictionary()
        {
            this.EntitiesDictionary = new Dictionary<(int, int), GameObject>();
            this.Map.Obstacles.Values.ToList().ForEach(Obstacle => this.EntitiesDictionary.TryAdd((Obstacle.X, Obstacle.Y), Obstacle));
            foreach (Food Food in this.Foods)
            {
                this.EntitiesDictionary.TryAdd((Food.X, Food.Y), Food);
            }

            foreach (Snake Snake in this.Snakes)
            {
                this.EntitiesDictionary.TryAdd((Snake.Head.X, Snake.Head.Y), Snake.Head);
                foreach (SnakeBody Body in Snake.Bodies)
                {
                    this.EntitiesDictionary.TryAdd((Body.X, Body.Y), Body);
                }
            }
        }
        private void GenerateNewFood()
        {
            (int, int) NewFoodPostion = Randomizer.Instance.GetValidRandomPosition(this);
            Food NewFood = new Food(NewFoodPostion.Item1, NewFoodPostion.Item2);
            NewFood.OnFoodEatenEvent += this.OnFoodEaten;
            this.Foods.Add(NewFood);
            this.OnFoodGenerateEvent?.Invoke(NewFood);
        }
        private void OnFoodEaten(Food Food)
        {

            this.GenerateNewFood();
        }

        private void GenerateNewSnake(ISnakeController Controller)
        {
            Snake NewSnake = new Snake(Controller);
            this.Snakes.Add(NewSnake);

            NewSnake.OnDieEvent += this.OnSnakeDie;
            this.OnSnakeGenerateEvent?.Invoke(NewSnake);
        }
        private void OnSnakeDie(Snake Snake)
        {
            this.Snakes.Remove(Snake);
        }
    }
}
