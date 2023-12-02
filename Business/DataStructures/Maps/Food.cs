using Business.DataStructures.Abstracts;
using Business.DataStructures.Snakes;
using Business.Enums;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.DataStructures.Maps
{
    public class Food : GameObject, IDisposable
    {
        public override Brush Color => Brushes.Red;

        public override Shape Shape => Shape.Ellipse;

        public event Action<Food> OnFoodEatenEvent;

        public Food(int X, int Y) : base(X, Y)
        {
        }

        public override void ColisionEffect(Snake Snake)
        {
            Snake.Grow();
            OnFoodEatenEvent?.Invoke(this);
            Dispose();
        }

        public void Dispose()
        {
            GameManager.Instance.Foods.Remove(this);
        }
    }
}
