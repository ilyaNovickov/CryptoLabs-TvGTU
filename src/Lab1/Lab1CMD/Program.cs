using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1CMD
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test2and3();
            Test100and28();
            Test2and8();
            Test3and2();
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
        static ulong FastPowAndMod(long a, long x, long y)
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
            }

            return prevValue;
        }
    }
}
