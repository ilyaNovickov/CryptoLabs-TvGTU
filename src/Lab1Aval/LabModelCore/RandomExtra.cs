using System;

namespace Lab1Models
{
    /// <summary>
    /// Доп методы для класса рандомайзера Random
    /// </summary>
    public static class RandomExtra
    {
        /// <summary>
        /// Генерация случайного числа типа uint в заданном диапазоне
        /// </summary>
        /// <param name="random">Класс рандомайзера</param>
        /// <param name="minValue">Минимальная граница</param>
        /// <param name="maxValue">Максимальная граница</param>
        /// <returns>Случайное число из заданного диапазона</returns>
        /// <exception cref="Exception"></exception>
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

        /// <summary>
        /// Генерация случайного числа типа ulong в заданном диапазоне
        /// </summary>
        /// <param name="random">Класс рандомайзера</param>
        /// <param name="minValue">Минимальная граница</param>
        /// <param name="maxValue">Максимальная граница</param>
        /// <returns>Случайное число из заданного диапазона</returns>
        /// <exception cref="Exception"></exception>
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
