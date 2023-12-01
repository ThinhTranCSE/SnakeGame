using System.Drawing;
using static Business.Enums.Enums;

namespace Business.DataStructures
{

    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public (int, int) Position => (this.X, this.Y);
        public virtual Brush Color { get; }

        public virtual Shape Shape { get; }
        public GameObject(int X, int Y, int Width = 1, int Height = 1)
        {
            this.X = X;
            this.Y = Y;

            this.Width = Width;
            this.Height = Height;

            this.Color = Brushes.White;
            this.Shape = Shape.Rectangle;
        }

        public virtual void ColisionEffect(Snake Snake)
        {
            Snake.Die();
        }
    }
}