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
        static BigInteger n = 0;
        static BigInteger d = 0;
        static BigInteger e = 0;

        public static event LogEventHandler LoggingEvent;



        public static void GenerateKeysWithList()
        {

            Random rnd = new Random();
//Начало генерации ключей
sign:
            //Генерация p
            do
            {
                p = rnd.NextUint(0U, uint.MaxValue);
            } while (!MathExtra.IsPrime(p));

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число p := {p}"));

            //Генерация q
            do
            {
                q = rnd.NextUint(0U, uint.MaxValue);
            } while (!MathExtra.IsPrime(q));

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число q := {q}"));

            //Определение модуля n
            n = (BigInteger)p * (BigInteger)q;

            LoggingEvent?.Invoke(null, new LogEventArgs($"Определено число \"n = p * q\" := {n}"));

            //Так как символьные литералы в C# представлены в UniCode,
            //то их можно представить как числа от 0 до 2^16 - 1 (т. е. ushort)
            //Значит, что n должен быть больше ushort.MaxValue, чтобы 
            //можно было закодировать все символы
            if (n <= ushort.MaxValue)
            {
                LoggingEvent?.Invoke(null, new LogEventArgs($"Модуль n = {n} не может закодировать все символьные литералы кодировки UniCode\n" +
                    $"Повторная генерация p и q"));
                goto sign;
            }

            //Фуркция Эйлера
            BigInteger eln = (BigInteger)(p - 1) * (BigInteger)(q - 1);

            LoggingEvent?.Invoke(null, new LogEventArgs($"Вычисление функции эйлера Ф = \"(n - 1) * (q - 1)\" := {eln}"));

            //Определение ключа e
            do
            {
                e = rnd.NextUint(0U, uint.MaxValue);
            } while (!MathExtra.FindMutuallyPrimeNumbers(e, eln) || !(1 < e && e < eln));

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число e := {e}"));

            //Определение ключа d
            {
                MathExtra.ExtendedEvklidAlgorithm(eln, e, out BigInteger x, out BigInteger y);

                BigInteger min = BigInteger.Min(x, y);

                d = eln - BigInteger.Abs(min);

                //Возможны ситуации, когда алгоритм находит 
                //такое d, которое не удовлетворяет требуемому
                //условию (e*d)% ((q-1)(p-1))
                //Тогда требуется перезапуск алгоритма
                BigInteger modTest = (d * e) % eln;

                LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число d := {d}\n" +
                    $"Проверка (d * e) mod Ф = {modTest}"));

                if (modTest != 1)
                {
                    LoggingEvent?.Invoke(null, new LogEventArgs($"Число d не соотведствует требованиям " +
                        $"=> Повторная генерация ключей"));
                    goto sign;
                }

            }

            LoggingEvent?.Invoke(null, new LogEventArgs($"Генерация ключей завершина"));

            ////Тест кодирования
            //string message = testMess;
            //BigInteger[] crM = new BigInteger[message.Length];
            //BigInteger[] decrM = new BigInteger[message.Length];
            //string decrMess = "";

            //for (int i = 0; i < message.Length; i++)
            //{
            //    crM[i] = MathExtra.ModularExponentiation(message[i], e, n);
            //    decrM[i] = MathExtra.ModularExponentiation(crM[i], d, n);
            //    decrMess += ((char)decrM[i]);
            //}
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
