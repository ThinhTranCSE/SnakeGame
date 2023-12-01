﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Business.Enums.Enums;

namespace Business.Interfaces
{
    public interface ISnakeController
    {
        public event Action<Direction> OnDirectionChanged;
    }
}
