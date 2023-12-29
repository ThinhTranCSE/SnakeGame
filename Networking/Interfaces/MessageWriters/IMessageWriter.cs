using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Interfaces.MessageWriters
{
    public interface IMessageWriter
    {
        byte[] GetBytes();

    }
}
