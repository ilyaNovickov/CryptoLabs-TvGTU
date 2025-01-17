using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        public static BigInteger ModularExponentiation(BigInteger baseValue, BigInteger exponent, BigInteger modulus)
        {
            /*
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

            BigInteger result = 1;          // Начальное значение результата
            //По факрту: a mod y
            BigInteger baseMod = baseValue % modulus; // Приводим основание по модулю

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
        public static bool IsPrime(BigInteger number)
        {
            // 1 не является простым числом
            if (number < 2) return false;

            // Проверка на делимость на 2
            if (number % 2 == 0 && number != 2) return false;

            // Проверяем делители от 3 до √number
            BigInteger limit = Sqrt(number);
            for (long divisor = 3; divisor <= limit; divisor += 2)
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
        public static bool FindMutuallyPrimeNumbers(BigInteger x, BigInteger y)
        {
            return EvklidAlgorithm(x, y) == 1;
        }

        /// <summary>
        /// Поиск наибольшего общего делителя по алгоритму Эвклида
        /// </summary>
        /// <param name="a">Число A</param>
        /// <param name="b">Число B</param>
        /// <returns>НОД (наибольший общий делитель)</returns>
        public static BigInteger EvklidAlgorithm(BigInteger a, BigInteger b)
        {
            if (a < b)
            {
                BigInteger temp = a; 
                a = b; 
                b = temp;
            }

            while (b != 0)
            {
                BigInteger mod = a % b;
                a = b;
                b = mod;
            }

            return a;
        }

        /// <summary>
        /// Расширенный алгоритм Эвклида для поиска наибольшего общего делителя и коофициентов,
        /// которые формируют этот делитель по уравнению a*x+b*x=НОД
        /// </summary>
        /// <param name="a">Число A</param>
        /// <param name="b">Число B</param>
        /// <param name="x">Коофициент X</param>
        /// <param name="y">Коофициент Y</param>
        /// <returns>Наибольший общий делитель</returns>
        public static BigInteger ExtendedEvklidAlgorithm(BigInteger a, BigInteger b, out BigInteger x, out BigInteger y)
        {
            // Если b равно 0, то НОД равен a, и x = 1, y = 0 (a*1+0*0=a)
            if (b == 0)
            {
                x = 1;
                y = 0;
                return a;
            }

            // Рекурсивный вызов для b и a % b
            BigInteger gcd = ExtendedEvklidAlgorithm(b, a % b, out BigInteger x1, out BigInteger y1);

            // Обновляем x и y по обратной подстановке
            x = y1;
            y = x1 - (long)(a / b) * y1;

            return gcd;
        }

        public static BigInteger Sqrt(this BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!isSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        private static Boolean isSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);

            return (n >= lowerBound && n < upperBound);
        }
    }
}
