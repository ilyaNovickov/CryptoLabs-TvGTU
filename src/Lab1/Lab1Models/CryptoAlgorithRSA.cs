using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Lab1Models
{
    public static class CryptoAlgorithRSA
    {
        static string precompiledFile = "precompPrimeNumbers.bin";

        static uint p = 0;
        static uint q = 0;
        static ulong n = 0;
        static uint d = 0;
        static uint e = 0;

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

        /// <summary>
        /// Генерация файла со списком простых чисел
        /// </summary>
        /// <param name="endRange">Конец диапазона простых чисел для генерации</param>
        public static void GeneratePrimeNumberFile(ulong endRange)
        {
            List<ulong> list = MathExtra.FindPrimesInRange(0UL, endRange);

            FileStream streamWriter = new FileStream(precompiledFile, FileMode.Create, FileAccess.Write);
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(streamWriter, list);
            streamWriter.Close();
        }

        public static void GenerateKeysWithList()
        {
            /*
            List<ulong> primeNumbers = null;

            if (UsePrecompilatedFileofPrimeNumbers)
            {
                FileStream streamWriter = new FileStream(precompiledFile, FileMode.Open, FileAccess.Read);
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                object data = binaryFormatter.Deserialize(streamWriter);
                streamWriter.Close();

                if (data is List<ulong>)
                    primeNumbers = data as List<ulong>;
                else
                    throw new Exception("Не удалось получить данные из файла");
            }
            else
            {
                primeNumbers = MathExtra.FindPrimesInRange(0UL, EndofPrimeNumberRange);
            }
            */

#if DEBUG
            Random rnd = new Random(880035353);

            p = 61;
            q = 53;

            n = p * q;

            ulong eln = (ulong)(p - 1) * (ulong)(q - 1);

            e = 17;
            d = 2753;

            //do
            //{
            //    e = (uint)rnd.Next(0, int.MaxValue);
            //} while (MathExtra.FindMutuallyPrimeNumbers(d, eln));

            //do
            //{
            //    d = (uint)rnd.Next(0, int.MaxValue);
            //} while ((d * e) % eln == 1);
            

            ulong[] message = new ulong[] { 74, 74, 2325, 675};
            ulong[] crM = new ulong[message.Length];
            ulong[] decrM = new ulong[message.Length];

            for (int i = 0; i < message.Length; i++)
            {
                crM[i] = MathExtra.ModularExponentiation(message[i], e, n);
                decrM[i] = MathExtra.ModularExponentiation(crM[i], d, n);
            }

            int w = 1;
#else
            Random rnd = new Random();
#endif

        }
    }
}
