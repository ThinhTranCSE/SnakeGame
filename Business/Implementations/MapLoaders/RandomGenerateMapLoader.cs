using Business.Configurations;
using Business.DataStructures.Maps;
using Business.Interfaces;
using Business.Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations.MapLoaders
{
    public class RandomGenerateMapLoader : IMapLoader
    {
        private const double FILL_PERCENTAGE = 0.8f;
        private const int MAX_WALKER = 10;
        private const int CHANCE_TO_CREATE_WALKER = 40;
        private const int CHANCE_TO_DESTROY_WALKER = 20;
        private const int CHANCE_TO_CHANGE_DIRECTION = 30;


        private List<RandomWalker> Walkers { get; set; }

        private List<Floor> Floors { get; set; }
        private Dictionary<(int, int), Floor> FloorsDictionary { get; set; }
        private List<Obstacle> Obstacles { get; set; }

        public RandomGenerateMapLoader()
        {
            this.Walkers = new List<RandomWalker>();
            this.Floors = new List<Floor>();
            this.FloorsDictionary = new Dictionary<(int, int), Floor>();
            this.Obstacles = new List<Obstacle>();
        }
        public Map Load()
        {
            return this.GenerateMap();
        }

        private Map GenerateMap()
        {
            this.Walkers.Add(new RandomWalker(Settings.MAP_WIDTH / 2, Settings.MAP_HEIGHT / 2, Randomizer.Instance.GetRandomDirection()));

            this.GenerateFloors();
            this.GenrateObstacles();

            Map Map = new Map(this.Obstacles, this.Floors);
            this.Walkers.Clear();
            this.Floors.Clear();
            this.FloorsDictionary.Clear();
            return Map;
        }

        private void GenerateFloors()
        {
            while (this.Floors.Count < Settings.MAP_WIDTH * Settings.MAP_HEIGHT * FILL_PERCENTAGE)
            {
                foreach (RandomWalker Walker in this.Walkers)
                {
                    if (this.FloorsDictionary.ContainsKey(Walker.Position))
                    {
                        continue;
                    }
                    this.Floors.Add(new Floor(Walker.X, Walker.Y));
                    this.FloorsDictionary.Add((Walker.X, Walker.Y), new Floor(Walker.X, Walker.Y));
                }

                this.ChanceToRemoveWalker();
                this.ChanceToRedirectWalker();
                this.ChanceToCreateWalker();
                this.UpdateWalkerPosition();
            }
        }

        private void GenrateObstacles()
        {
            foreach(Floor Floor in this.Floors)
            {
                if (!this.FloorsDictionary.ContainsKey((Floor.X - 1, Floor.Y)))
                {
                    this.Obstacles.Add(new Obstacle(Floor.X - 1, Floor.Y));
                }
                if (!this.FloorsDictionary.ContainsKey((Floor.X + 1, Floor.Y)))
                {
                    this.Obstacles.Add(new Obstacle(Floor.X + 1, Floor.Y));
                }
                if (!this.FloorsDictionary.ContainsKey((Floor.X, Floor.Y - 1)))
                {
                    this.Obstacles.Add(new Obstacle(Floor.X, Floor.Y - 1));
                }
                if (!this.FloorsDictionary.ContainsKey((Floor.X, Floor.Y + 1)))
                {
                    this.Obstacles.Add(new Obstacle(Floor.X, Floor.Y + 1));
                }
            }
        }

        private void ChanceToCreateWalker()
        {
            List<RandomWalker> WalkersWaited = new List<RandomWalker>();
            foreach(RandomWalker Walker in this.Walkers)
            {
                if(Randomizer.Instance.GetRandomNumber(0, 100) < CHANCE_TO_CREATE_WALKER)
                {
                    if(this.Walkers.Count < MAX_WALKER + WalkersWaited.Count)
                    {
                        WalkersWaited.Add(new RandomWalker(Walker.X, Walker.Y, Randomizer.Instance.GetRandomDirection()));
                    }
                }
            }
            this.Walkers.AddRange(WalkersWaited);
        }
        
        private void ChanceToRemoveWalker()
        {
            foreach(RandomWalker Walker in this.Walkers)
            {
                if(Randomizer.Instance.GetRandomNumber(0, 100) < CHANCE_TO_DESTROY_WALKER && this.Walkers.Count > 1)
                {
                    this.Walkers.Remove(Walker);
                    break;
                }
            }
        }

        private void ChanceToRedirectWalker()
        {
            for(int i = 0; i < this.Walkers.Count; i++)
            {
                if(Randomizer.Instance.GetRandomNumber(0, 100) < CHANCE_TO_CHANGE_DIRECTION)
                {
                    this.Walkers[i].Direction = Randomizer.Instance.GetRandomDirection();
                }
            }
        }

        private void UpdateWalkerPosition()
        {
            foreach(RandomWalker Walker in this.Walkers)
            {
                Walker.X += (int)Walker.Direction.ToVector().X;
                Walker.Y += (int)Walker.Direction.ToVector().Y;
                Walker.X = Math.Clamp(Walker.X, 1, Settings.MAP_WIDTH - 2);
                Walker.Y = Math.Clamp(Walker.Y, 1, Settings.MAP_HEIGHT - 2);
            }
        }

    }
}
