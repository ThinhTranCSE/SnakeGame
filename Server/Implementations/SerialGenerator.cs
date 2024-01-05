using Server.Abstracts;
using Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Implementations
{
    public class SerialGenerator : Singleton<SerialGenerator>, ISerialGenerator
    { 
        public object _SyncLock { get; private set; }
        public HashSet<uint> AvailableSerials { get; private set; }
        public HashSet<uint> UsedSerials { get; private set; }

        public uint SerialQuantity => (uint)(AvailableSerials.Count + UsedSerials.Count);

        public SerialGenerator(uint InitialQuantity)
        {
            this._SyncLock = new object();
            this.AvailableSerials = new HashSet<uint>();
            this.UsedSerials = new HashSet<uint>();
            
            for (uint i = 0; i < InitialQuantity; i++)
            {
                AvailableSerials.Add(i);
            }
        }

        public uint GetSerial()
        {
            lock (_SyncLock)
            {
                if (AvailableSerials.Count == 0)
                {
                    ExtendSerials();
                }
                uint Serial = AvailableSerials.First();
                AvailableSerials.Remove(Serial);
                UsedSerials.Add(Serial);
                return Serial;
            }
        }
        
        public void ReleaseSerial(uint Serial)
        {
            lock (_SyncLock)
            {
                if (UsedSerials.Contains(Serial))
                {
                    UsedSerials.Remove(Serial);
                    AvailableSerials.Add(Serial);
                }
            }
        }
        private void ExtendSerials()
        {
            lock (_SyncLock)
            {
                for (uint i = SerialQuantity; i < SerialQuantity * 2; i++)
                {
                    AvailableSerials.Add(i);
                }
            }
        }
    
    }
}
