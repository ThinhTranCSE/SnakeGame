using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface IServer : IDisposable
    {
       
        void Start();
        void Stop();
        void Dispose();
    }
}
