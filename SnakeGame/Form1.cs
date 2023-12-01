using Business;
using Business.DataStructures;
using static Business.Enums.Enums;

namespace SnakeGame
{
    public partial class Form1 : Form
    {
        private Thread GameThread;


        public Form1()
        {
            InitializeComponent();

            GameManager.Instance.StartGame();
            this.GameThread = new Thread(this.GameLoop);
            this.GameThread.Start();
        }

        private void GameLoop()
        {
            try
            {
                while (true)
                {
                    this.PtbGamePlay.Invalidate();
                    Thread.Sleep(100);
                }
            }
            catch (ThreadInterruptedException)
            {
                return;
            }


        }

        private void UpdateGameFrame(object sender, PaintEventArgs e)
        {
            GraphicManager.Instance.DrawGameObjects(GameManager.Instance.GetGameObjects(), e.Graphics);
            GameManager.Instance.Update();
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            this.GameThread?.Interrupt();
        }

        private void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            //similar onkeydown
            if (e.KeyChar == 'w')
            {
                PlayerController.Instance.HandleInput(Direction.Up);
            }
            else if (e.KeyChar == 's')
            {
                PlayerController.Instance.HandleInput(Direction.Down);
            }
            else if (e.KeyChar == 'a')
            {
                PlayerController.Instance.HandleInput(Direction.Left);
            }
            else if (e.KeyChar == 'd')
            {
                PlayerController.Instance.HandleInput(Direction.Right);
            }
        }
    }
}