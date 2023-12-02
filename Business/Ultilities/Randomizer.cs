using Business.Configurations;
using Business.DataStructures.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.Ultilities
{
    public class Randomizer
    {
        public static Randomizer Instance => GetInstance();

        private static Randomizer _Instance;

        public (int, int ) NextFoodPosition => GetValidRandomPosition();

        private Random Random;
        protected Randomizer() { 
            this.Random = new Random();
        }

        public static Randomizer GetInstance()
        {
            if (Randomizer._Instance == null)
            {
                Randomizer._Instance = new Randomizer();
            }
            return Randomizer._Instance;
        }

        public int GetRandomNumber(int Min, int Max)
        {
            return this.Random.Next(Min, Max + 1);
        }

        public (int, int) GetValidRandomPosition()
        {
            List<Floor> Floors = GameManager.Instance.Map.ActiveFloors.Values.ToList();
            int Index = this.GetRandomNumber(0, Floors.Count);
            Floor ValidFloor = Floors[Index];

            return (ValidFloor.X, ValidFloor.Y); ;
        }

        public Direction GetRandomDirection()
        {
            int RandomIndex = this.GetRandomNumber(0, 3);
            Direction Direction;
            switch (RandomIndex)
            {
                case 0:
                    Direction = Direction.Up;
                    break;
                case 1:
                    Direction = Direction.Down;
                    break;
                case 2:
                    Direction = Direction.Left;
                    break;
                case 3:
                    Direction = Direction.Right;
                    break;
                default:
                    Direction = Direction.Up;
                    break;
            }
            Console.WriteLine(Direction);
            return Direction;
        } 
        
    }
}
