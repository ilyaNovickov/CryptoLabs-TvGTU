using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Test2and3();
            //Test100and28();
            //Test2and8();
            //Test3and2();
            
            Progress<ulong> reporter = new Progress<ulong>();
            reporter.ProgressChanged += (sender, progress) =>
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"Progress : {progress}");
            };
            Stopwatch sw = new Stopwatch();
            sw.Start();
            // ulong res = FastPowAndMod(uint.MaxValue, uint.MaxValue, 3L, reporter);
            ulong res = ModularExponentiation(2, 10, 7UL);
            sw.Stop();
            Console.WriteLine($"RES := {res}");
            Console.WriteLine($"TIME := {sw.Elapsed.TotalSeconds}");
            Console.ReadLine();
            
        }

        static void TestingMethods(long x, long y)
        {
            for (int i = 0; i < 30; i++)
            {
                var method1F = new Func<ulong>(() =>
                {
                    if (i == 0)
                        return 1UL;
                    ulong count = 1;
                    ulong res = 1;
                    while (count <= (ulong)i)
                    {
                        res *= (ulong)x;
                        count++;
                    }
                    return res;
                });
                ulong method1 = method1F.Invoke() % (ulong)y;
                ulong method2 = FastPowAndMod((long)x, (long)i, (long)y);
                Console.WriteLine($"{i} | {method1} | {method2} | {method1 == method2}");
            }
        }

        static void Test2and3()
        {
            long x = 2;
            long y = 3;
            Console.WriteLine($"| X = {x} and Y = {y} |");
            TestingMethods(x, y);
        }

        static void Test3and2()
        {
            long x = 3;
            long y = 2;
            Console.WriteLine($"| X = {x} and Y = {y} |");
            TestingMethods(x, y);
        }

        static void Test100and28()
        {
            long x = 100;
            long y = 28;
            Console.WriteLine($"| X = {x} and Y = {y} |");
            TestingMethods(x, y);
        }

        static void Test2and8()
        {
            long x = 2;
            long y = 8;
            Console.WriteLine($"| X = {x} and Y = {y} |");
            TestingMethods(x, y);
        }

        static long BinaryPow(long baseNumber, long exponent)
        {
            long result = 1;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    result *= baseNumber;
                baseNumber *= baseNumber;
                exponent >>= 1;
            }
            return result;
        }

        /// <summary>
        /// a^x mod y
        /// </summary>
        /// <returns></returns>
        //static long FastPowAndMod(long a, long x, long y)
        //{
        //    bool IsYGreaterThanAX(long ac, long xc, long yc, out long xf)
        //    {
        //        xf = 0;
        //        while (xc > 1 && yc > ac)
        //        {
        //            ac *= ac;
        //            xc--;
        //            xf++;
        //        }
        //        if (yc > ac)
        //            xf = 1;
        //        return yc > ac;
        //    }

        //    long result = 0L;
        //    long xofFreque = 0L;

        //    if (IsYGreaterThanAX(a, x, y, out xofFreque))
        //        result = BinaryPow(a, x) % y;
        //    else
        //    {
        //        long newX = x % (xofFreque + 1);
        //        result = BinaryPow(a, newX) % y;
        //    }
        //    return result;
        //}
        static ulong FastPowAndMod(long a, long x, long y, IProgress<ulong> progress = null)
        {
            ulong countofExp = 1L;
            ulong prevValue = (ulong)a;

            if (x == 0)
                return 1UL;
            else if (x == 1)
                return (ulong)(a % y);

            while (countofExp != (ulong)x)
            {
                prevValue = (prevValue % (ulong)y) * (ulong)(a % y) % (ulong)y;
                countofExp++;
                if (countofExp % 1000000000 == 0)
                    progress.Report(countofExp);
                if (prevValue == 0)
                    break;
            }

            return prevValue;
        }

        /// <summary>
        /// Быстрое возведение в степень по модулю.
        /// Вычисляет (base^exp) % mod.
        /// </summary>
        /// <param name="baseValue">Основание (a).</param>
        /// <param name="exponent">Показатель степени (x).</param>
        /// <param name="modulus">Модуль (y).</param>
        /// <returns>Результат вычислений (a^x % y).</returns>
        static ulong ModularExponentiation(ulong baseValue, ulong exponent, ulong modulus)
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
    }
}
