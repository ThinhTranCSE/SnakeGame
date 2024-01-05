using Business.DataStructures.Abstracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IGraphicManager
    {
        void DrawGameObjects(List<GameObject> GameObjects, Graphics Canvas);
    }
}
