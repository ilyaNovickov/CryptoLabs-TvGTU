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
                p = rnd.NextUint(0U, byte.MaxValue);
            } while (!MathExtra.IsPrime(p));

            do
            {
                q = rnd.NextUint(0U, byte.MaxValue);
            } while (!MathExtra.IsPrime(p));
            
            n = p * q;

            if (n <= byte.MaxValue)
                goto sign;

            long eln = (long)(p - 1) * (long)(q - 1);

            do
            {
                e = rnd.NextUint(0U, uint.MaxValue);
            } while (!MathExtra.FindMutuallyPrimeNumbers(e, eln) || !(1 < e && e < eln));

            //do
            //{
            //    d = rnd.NextUint(0U, uint.MaxValue);
            //} while ((e * d) % eln == 1);
            //d = foo(eln, e);
            {
                MathExtra.ExtendedEvklidAlgorithm(eln, e, out long x, out long y);

                long min = Math.Min(x, y);

                d = eln - Math.Abs(min);
            }



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
