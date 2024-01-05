using Business.DataStructures.Abstracts;
using Business.DataStructures.Maps;
using Business.DataStructures.Snakes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGameManager
    {
        event Action OnUpdateEvent;

        event Action<Snake> OnSnakeGenerateEvent;

        event Action<Food> OnFoodGenerateEvent;
        List<Snake> Snakes { get; }
        Map Map { get; }
        List<Food> Foods { get; }
        Dictionary<(int, int), GameObject> EntitiesDictionary { get; }
        void Update();
        List<GameObject> GetGameObjects();
         void StartGame();
    }
}
