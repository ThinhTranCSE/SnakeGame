using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Networking.DataStructures
{
    public class ReceiveQueue
    {
        private byte[] Queue { get; set; }
        public int Capacity { get; private set; }
        public int Count { get; private set; }

        public bool IsEmpty => Count == 0;
        public byte this[int Index]
        {
            get
            {
                if (Index >= Count)
                {
                    throw new Exception("Index is greater than Count");
                }
                return Queue[Index];
            }
        }   

        public ReceiveQueue()
        {
            Queue = new byte[1024];
            Capacity = 1024;
            Count = 0;
        }

        public void EnqueueRange(byte[] Buffer, int PacketSize)
        {
            if (Count + Buffer.Length > Capacity)
            {
                ExpandQueue();
            }
            Buffer.CopyTo(Queue, Count);
            Count += PacketSize;
        }

        public void Enqueue(byte Value)
        {
            if(Count + 1 > Capacity)
            {
                ExpandQueue();
                
            }
            Queue[Count] = Value;
            Count++;
        }

        public byte[] DequeueRange(int Length)
        {
            if (Length > Count)
            {
                throw new Exception("Length is greater than Count");
            }
            byte[] Result = new byte[Length];
            Array.Copy(Queue, Result, Length);
            Count -= Length;
            for (int i = 0; i < Count; i++)
            {
                Queue[i] = Queue[i + Length];
            }
            return Result;
        }

        private void ExpandQueue()
        {
            byte[] NewQueue = new byte[Capacity * 2];
            Queue.CopyTo(NewQueue, 0);
            Queue = NewQueue;
            Capacity *= 2;
        }

        public void Clear()
        {
            Count = 0;
        }

        public byte[] ToArray()
        {
            byte[] Result = new byte[Count];
            Array.Copy(Queue, Result, Count);
            return Result;
        }
    }
}
