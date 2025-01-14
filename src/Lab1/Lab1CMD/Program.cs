using Lab1Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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
            CryptoAlgorithRSA.GenerateKeysWithList();
            Console.ReadLine();
        }

        
    }
}
