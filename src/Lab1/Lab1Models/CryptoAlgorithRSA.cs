using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Lab1Models
{
    public static class CryptoAlgorithRSA
    {
        static uint p = 0;
        static uint q = 0;

        static bool useFileofPrimeNumbers = false;
        static ulong endofPrimeNumberRange = byte.MaxValue;

        public static bool UsePrecompilatedFileofPrimeNumbers
        {
            set => useFileofPrimeNumbers = value;
            get => useFileofPrimeNumbers;
        }

        public static ulong EndofPrimeNumberRange
        {
            get => endofPrimeNumberRange;
            set => endofPrimeNumberRange = value;
        }


    }
}
