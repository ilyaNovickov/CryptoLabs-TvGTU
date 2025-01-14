using Lab1Models;
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
            //List<ulong> list = MathExtra.FindPrimesInRange(0UL, ulong.MaxValue);
            //Console.WriteLine(list.Count);
           Console.WriteLine("res := " + MathExtra.FindMutuallyPrimeNumbers(20, 3));
            Console.WriteLine("res := " + MathExtra.FindMutuallyPrimeNumbers(21, 3));
            Console.WriteLine("res := " + MathExtra.FindMutuallyPrimeNumbers(100, 11));
            Console.WriteLine("res := " + MathExtra.FindMutuallyPrimeNumbers(77, 13));
            Console.ReadLine();
        }

        
    }
}
