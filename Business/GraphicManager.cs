using Business.Configurations;
using Business.DataStructures.Abstracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business
{
    public class GraphicManager
    {
        public static GraphicManager Instance => GetInstance();

        private static GraphicManager _Instance;
        private Graphics Canvas { get; set; }
        protected GraphicManager()
        {

        }

        public static GraphicManager GetInstance()
        {
            if (GraphicManager._Instance == null)
            {
                GraphicManager._Instance = new GraphicManager();
            }
            return GraphicManager._Instance;
        }

        public void DrawGameObjects(List<GameObject> GameObjects, Graphics Canvas)
        {
            this.Canvas = Canvas;
            foreach (var GameObject in GameObjects)
            {
                Rectangle Container = new Rectangle(
                    GameObject.X * Settings.GAME_OBJECT_WIDTH,
                    GameObject.Y * Settings.GAME_OBJECT_HEIGHT,
                    GameObject.Width * Settings.GAME_OBJECT_WIDTH,
                    GameObject.Height * Settings.GAME_OBJECT_HEIGHT
                );
                switch (GameObject.Shape)
                {
                    case Shape.Rectangle:
                        this.Canvas.FillRectangle(GameObject.Color, Container);
                        this.Canvas.DrawRectangle(Pens.Black, Container);
                        this.Canvas.DrawString($"{GameObject.X},{GameObject.Y}", new Font("Arial", 5), Brushes.Black, Container);
                        break;

                    case Shape.Ellipse:
                        this.Canvas.FillEllipse(GameObject.Color, Container);
                        break;
                }
            }
            this.Canvas = null;
        }
    }
}
