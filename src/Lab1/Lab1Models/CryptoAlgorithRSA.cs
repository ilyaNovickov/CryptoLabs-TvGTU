using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Timers;
using System.Numerics;

namespace Lab1Models
{
    public static class CryptoAlgorithRSA
    {
        static string precompiledFile = "precompPrimeNumbers.bin";

        static uint p = 0;
        static uint q = 0;
        static long n = 0;
        static long d = 0;
        static long e = 0;

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
        public static void GeneratePrimeNumberFile(long endRange)
        {
            List<long> list = MathExtra.FindPrimesInRange(0L, endRange);

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


            Random rnd = new Random(880035353);

            sign:
            do
            {
                p = rnd.NextUint(0U, uint.MaxValue);
            } while (!MathExtra.IsPrime(p));

            do
            {
                q = rnd.NextUint(0U, uint.MaxValue);
            } while (!MathExtra.IsPrime(p));
            
            n = p * q;

            if (n <= ushort.MaxValue)
                goto sign;

            long eln = (long)(p - 1) * (long)(q - 1);

            do
            {
                e = rnd.NextUint(0U, uint.MaxValue);
            } while (!MathExtra.FindMutuallyPrimeNumbers(e, eln) || !(1 < e && e < eln));


            
            {
                MathExtra.ExtendedEvklidAlgorithm(eln, e, out long x, out long y);

                long min = Math.Min(x, y);

                d = eln - Math.Abs(min);

                d = ModInverse(e, eln);
            }
            foo(eln, e);


            string message = testMess;
            long[] crM = new long[message.Length];
            long[] decrM = new long[message.Length];
            string decrMess = "";

            for (int i = 0; i < message.Length; i++)
            {
                crM[i] = MathExtra.ModularExponentiation(message[i], e, n);
                decrM[i] = MathExtra.ModularExponentiation(crM[i], d, n);
                decrMess += ((char)decrM[i]);
            }
        }

        // Метод для нахождения мультипликативного обратного
        static long ModInverse(long a, long m)
        {
            long m0 = m, t, q;
            long x0 = 0, x1 = 1;

            if (m == 1)
                return 0;

            while (a > 1)
            {
                // q - это частное
                q = a / m;

                t = m;

                // m - остаток, теперь применим алгоритм Евклида
                m = a % m;
                a = t;

                t = x0;

                x0 = x1 - q * x0;
                x1 = t;
            }

            // Обеспечиваем, что x1 положительно
            if (x1 < 0)
                x1 += m0;

            return x1;
        }

        static void foo(long a, long b)
        {
            long q = 0;
            long r = 0;
            long x1 = 0;
            long x2 = 1;
            long y1 = 1;
            long y2 = 0;

            long bOld = b;
            long aOld = a;

            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                long x = x2 - q * x1;
                long y = y2 - q * y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }

            long d = aOld - Math.Abs(Math.Min(x2, y2));

            long res = ( (bOld%aOld) * (d%aOld)) % aOld;
            BigInteger tel = bOld;
            BigInteger te = bOld;
            BigInteger td = d;

            BigInteger res2 = (te * td) % tel ;
        }


        static string testMess = "Древнегреческие математики называли этот алгоритм ἀνθυφαίρεσις " +
            "или ἀνταναίρεσις — «взаимное вычитание». " +
            "Этот алгоритм не был открыт Евклидом, так как " +
            "упоминание о нём имеется уже в Топике Аристотеля (IV век до н. э.)[3]." +
            " В «Началах» Евклида он описан дважды — в VII книге для нахождения наибольшего " +
            "общего делителя двух натуральных чисел[1] и в X книге для нахождения наибольшей " +
            "общей меры двух однородных величин[2]. В обоих случаях дано геометрическое " +
            "описание алгоритма, для нахождения «общей меры» двух отрезков.\r\n\r\n" +
            "Историками математики было выдвинуто предположение, " +
            "что именно с помощью алгоритма Евклида (процедуры последовательного взаимного вычитания) " +
            "в древнегреческой математике впервые было открыто " +
            "существование несоизмеримых величин (стороны и диагонали " +
            "квадрата, или стороны и диагонали правильного пятиугольника)[10]. " +
            "Впрочем, это предположение не имеет достаточных документальных " +
            "подтверждений. Алгоритм для поиска наибольшего общего делителя " +
            "двух натуральных чисел описан также в I книге древнекитайского т" +
            "рактата Математика в девяти книгах. ";
    }
}
