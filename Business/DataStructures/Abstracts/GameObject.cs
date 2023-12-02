using System.Drawing;
using static Business.Enums.Enums;
using Business.DataStructures.Snakes;
namespace Business.DataStructures.Abstracts
{

    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public (int, int) Position => (X, Y);
        public virtual Brush Color { get; }

        public virtual Shape Shape { get; }
        public GameObject(int X, int Y, int Width = 1, int Height = 1)
        {
            this.X = X;
            this.Y = Y;

            this.Width = Width;
            this.Height = Height;

            Color = Brushes.White;
            Shape = Shape.Rectangle;
        }

        public virtual void ColisionEffect(Snake Snake)
        {
            Snake.Die();
        }
    }
}