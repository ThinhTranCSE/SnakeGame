using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Business.DataStructures;
using Business.Interfaces;
using Business.Implementations.MapLoaders;

namespace Business
{
    public class MapLoader
    {
        public static MapLoader Instance => GetInstance();

        private static MapLoader _Instance;

        private IMapLoader MapLoaderStrategy;
        protected MapLoader() 
        {
            //this.MapLoaderStrategy = new LoadFromFileMapLoader();
            this.MapLoaderStrategy = new RandomGenerateMapLoader();
        }

        public static MapLoader GetInstance()
        {
            if (MapLoader._Instance == null)
            {
                MapLoader._Instance = new MapLoader();
            }
            return MapLoader._Instance;
        }

        public Map LoadMap()
        {
            return this.MapLoaderStrategy.Load();
        }
    }
}
