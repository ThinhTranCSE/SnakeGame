using Business.DataStructures.Maps;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Implementations.MapLoaders
{
    public class LoadFromFileMapLoader : IMapLoader
    {
        public Map Load()
        {
            return this.LoadMap("Map1.txt");
        }

        private Map LoadMap(string FilePath)
        {
            string AbsolutePath = Path.Combine(Directory.GetCurrentDirectory(), "Maps", FilePath);
            string[] MapContent = File.ReadAllLines(AbsolutePath);
            List<Obstacle> Obstacles = new List<Obstacle>();
            List <Floor> Floors = new List<Floor>();
            for (int Y = 0; Y < MapContent.Length; Y++)
            {
                string Line = MapContent[Y];
                for (int X = 0; X < Line.Length; X++)
                {
                    if (Line[X] == '1')
                    {
                        Obstacles.Add(new Obstacle(X, Y));
                    }
                    else
                    {
                        Floors.Add(new Floor(X, Y));
                    }
                }
            }
            Map Map = new Map(Obstacles, Floors);
            return Map;
        }
    }
}
