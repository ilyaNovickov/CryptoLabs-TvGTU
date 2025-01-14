using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1Models
{
#if DEBUG
    public static class MathExtra
#else
    internal static class MathExtra
#endif
    {
        /// <summary>
        /// Быстрое возведение в степень по модулю.
        /// Вычисляет (base^exp) % mod.
        /// </summary>
        /// <param name="baseValue">Основание (a).</param>
        /// <param name="exponent">Показатель степени (x).</param>
        /// <param name="modulus">Модуль (y).</param>
        /// <returns>Результат вычислений (a^x % y).</returns>
        public static ulong ModularExponentiation(ulong baseValue, ulong exponent, ulong modulus)
        {
            /*
             * Спасибо ChatGPT за разъяснение.
             * 
             * Идея испольует свойства mod и степеней:
             * - (a*b) mod y = [ (a mod y) * (b mod y) ] mod y
             * - a^x * a^y = a^(x+y)
             * а также в том, что любое число можно представить
             * как степень двойки:
             * 10 = 0b1010
             * Вспоминаем, как переводить числа из двоичной системы 
             * в десятичную:
             * 0b1010 => 2^3 * 1 + 2^2 * 0 + 2^1 * 1 + 2^0 * 0 = 2^3 * 2^1 = 8 + 2 = 10
             * 
             * Таким образом возведение a^10 - это a^[ 2^3 + 2^1 ]
             * Значит "a^10 mod y" => "(a^2^3 * a^2^1) mod y " =>
             * => "[ (a^2^3 mod y) * (a^2^1 mod y) ] mod y"
             * 
             * Всё это в конечном итоге развернётся в:
             * [...[[a mod y * a mod y] * mod y ] * ... ] 
             */
            if (modulus == 1)
                return 0; // Если mod = 1, то результат всегда 0

            ulong result = 1;          // Начальное значение результата
            //По факрту: a mod y
            ulong baseMod = baseValue % modulus; // Приводим основание по модулю

            while (exponent > 0)
            {
                // Если текущая степень нечётная, умножаем результат на основание
                //Нас интирисует только нечётные числа, 
                //которые заканчиваются на "1" в bin
                if ((exponent & 1) == 1)
                {
                    result = (result * baseMod) % modulus;
                }

                // Возводим основание в квадрат, уменьшив степень вдвое
                //По факту: [ (a mod y) * (a mod y) ] mod y на I-ой итерации
                //т. е. a^2 mod y
                //На отсальных итерациях будет a^4 mod y, a^6 mod y и т. д.
                baseMod = (baseMod * baseMod) % modulus;

                // Переходим к следующему разряду экспоненты
                exponent >>= 1;
            }

            return result;
        }

        /// <summary>
        /// Метод для поиска простых чисел в заданном интервале.
        /// </summary>
        public static List<ulong> FindPrimesInRange(ulong start, ulong end)
        {
            List<ulong> primes = new List<ulong>();

            // Если начало интервала меньше 2, начинаем с 2
            if (start < 2) start = 2;

            for (ulong number = start; number <= end; number++)
            {
                if (IsPrime(number))
                {
                    primes.Add(number);
                }
            }

            return primes;
        }

        /// <summary>
        /// Метод для проверки, является ли число простым.
        /// </summary>
        public static bool IsPrime(ulong number)
        {
            // 1 не является простым числом
            if (number < 2) return false;

            // Проверка на делимость на 2
            if (number % 2 == 0 && number != 2) return false;

            // Проверяем делители от 3 до √number
            ulong limit = (ulong)Math.Sqrt(number);
            for (ulong divisor = 3; divisor <= limit; divisor += 2)
            {
                if (number % divisor == 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Определение, являются ли числа взаимопростыми (общий делитель "1")
        /// </summary>
        /// <param name="x">Число X</param>
        /// <param name="y">Число Y</param>
        /// <returns>Являются ли числа взаимопростыми</returns>
        public static bool FindMutuallyPrimeNumbers(ulong x, ulong y)
        {
            return EvklidAlgorithm(x, y) == 1UL;
        }

        /// <summary>
        /// Поиск наибольшего общего делителя по алгоритму Эвклида
        /// </summary>
        /// <param name="a">Число A</param>
        /// <param name="b">Число B</param>
        /// <returns>НОД (наибольший общий делитель)</returns>
        private static ulong EvklidAlgorithm(ulong a, ulong b)
        {
            if (a < b)
            {
                ulong temp = a; 
                a = b; 
                b = temp;
            }

            while (b != 0)
            {
                ulong mod = a % b;
                a = b;
                b = mod;
            }

            return a;
        }
    }
}
