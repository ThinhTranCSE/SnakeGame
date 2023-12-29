using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Interfaces
{
    public interface ISerialGenerator
    {
        HashSet<uint> AvailableSerials { get; }
        HashSet<uint> UsedSerials { get; }

        uint SerialQuantity { get; }
        uint GetSerial();
    }
}
