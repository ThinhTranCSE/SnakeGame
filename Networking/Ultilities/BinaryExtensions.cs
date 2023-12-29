using Networking.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Networking.Ultilities
{
    public static class BinaryExtensions
    {
        public static byte[] ToBytes(this string HexString)
        {
            if (HexString.Length % 2 != 0)
                throw new ArgumentException("Hex string must be multiple of 2 in length");

            byte[] Bytes = new byte[HexString.Length / 2];

            for (int i = 0; i < HexString.Length; i += 2)
            {
                Bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }

            return Bytes;
        }

        public static byte[] ToBytes(this ushort Value)
        {
            return BitConverter.GetBytes(Value);
        }

        public static byte[] ToBytes(this PacketTypes PacketType)
        {
            return BitConverter.GetBytes((ushort)PacketType);
        }

        public static string ToHexString(this byte[] Bytes)
        {
            return BitConverter.ToString(Bytes);
        }
     }
}
