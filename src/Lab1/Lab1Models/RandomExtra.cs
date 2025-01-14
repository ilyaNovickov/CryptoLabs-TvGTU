using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Models
{
    public static class RandomExtra
    {
        public static uint NextUint(this Random random, uint minValue, uint maxValue)
        {
            if (minValue > maxValue)
                throw new Exception("Минимальное значение не может быть ниже максимального");

            byte[] data = new byte[sizeof(uint)];

            random.NextBytes(data);

            uint res = BitConverter.ToUInt32(data, 0);

            res = (res % (maxValue - minValue)) + minValue;

            return res;
        }

        public static ulong NextUlong(this Random random, ulong minValue, ulong maxValue)
        {
            if (minValue > maxValue)
                throw new Exception("Минимальное значение не может быть ниже максимального");

            byte[] data = new byte[sizeof(ulong)];

            random.NextBytes(data);

            ulong res = BitConverter.ToUInt64(data, 0);

            res = (res % (maxValue - minValue)) + minValue;

            return res;
        }
    }
}
