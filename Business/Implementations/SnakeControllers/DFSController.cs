using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.Implementations.SnakeControllers
{
    public class DFSController : ISnakeController
    {
        public event Action<Direction> OnDirectionChanged;

        public Direction Direction { get; set; }

        private bool Flag = true;

        public DFSController()
        {
            this.Direction = Direction.Up;
        }
        public Direction NextDirection()
        {
            //if (Flag)
            //{
            //    Flag = false;
            //    return this.Direction;
            //}
            if (this.Direction == Direction.Up)
            {
                this.Direction = Direction.Right;
                this.Flag = true;
            }
            else if (this.Direction == Direction.Right)
            {
                this.Direction = Direction.Down;
                this.Flag = true;

            }
            else if (this.Direction == Direction.Down)
            {
                this.Direction = Direction.Left;
                this.Flag = true;

            }
            else if (this.Direction == Direction.Left)
            {
                this.Direction = Direction.Up;
                this.Flag = false;

            }
            return this.Direction;
        }
    }
}
