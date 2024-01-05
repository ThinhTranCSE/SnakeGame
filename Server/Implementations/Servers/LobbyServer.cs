using Server.Abstracts.Servers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Implementations.Servers
{
    public class LobbyServer : ServerBase
    {
        public LobbyServer(int ListenPort) : base(ListenPort)
        {

        }
    }
}
