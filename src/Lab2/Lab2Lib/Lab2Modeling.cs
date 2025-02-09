using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace Lab2Lib
{
    public class Lab2Modeling
    {
        private List<char> chars;

        public Lab2Modeling()
        {
            chars = new List<char>(32);

            for (char symbol = 'А'; symbol <= 'Я'; symbol++)
            {
                chars.Add(symbol);
            }

            chars.Add(' ');
        }

        public string Crypt(string openText, int k)
        {
            if (openText.Length == 0 || openText == null)
                throw new Exception("Нет октрытого текста");

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < openText.Length; i++)
            {
                int index = chars.IndexOf(openText[i]);

                if (index == -1)
                    throw new Exception("Нет такого символа для шифрования");

                int gamma = (int)(ModularExponentiation(k, i + 1, 32));

                int cryptSymbol = (index + gamma) % 33;

                builder.Append(cryptSymbol);
                builder.Append(';');
            }

            builder.Remove(builder.Length - 1, 1);

            return builder.ToString();
        }

        public string Decrypt(string cryptedMessage, int k)
        {
            if (cryptedMessage.Length == 0 || cryptedMessage == null)
                throw new Exception("Нет сообщения для дешифрования");

            StringBuilder builder = new StringBuilder();

            IEnumerable<int> cryptedVals = from val in cryptedMessage.Split(' ', ';', ',', '\t', '-', '|', '\\', '/')
                                select Convert.ToInt32(val);

            for (int i = 0; i < cryptedVals.Count(); i++)
            {
                int gamma = (int)(ModularExponentiation(k, i + 1, 32));

                int decryptVal = ( (cryptedVals.ElementAt(i) - gamma ) % 33 + 33) % 33;

                builder.Append(chars[decryptVal]);
            }

            return builder.ToString();
        }

        public int[] CheckGamma(int k, int countofElements = 100)
        {
            int[] res = new int[countofElements];

            for (int i = 0; i < countofElements; i++)
            {
                res[i] = (int)(ModularExponentiation(k, i + 1, 32));
            }

            return res;
        }

        public int PeriodAnalizer(int[] vals)
        {
            if (vals == null || vals.Length == 0)
                throw new Exception("Нет массива для проверки или он равень нулю");

            int n = vals.Length;

            // Проверяем все возможные длины периода (от 1 до n / 2)
            for (int periodLength = 1; periodLength <= n / 2; periodLength++)
            {
                bool isPeriodic = true;

                // Проверяем, повторяется ли последовательность длиной periodLength
                for (int i = 0; i < n - periodLength; i++)
                {
                    if (vals[i] != vals[i + periodLength])
                    {
                        if (vals[i + periodLength] == 0)
                            break;
                        isPeriodic = false;
                        break;
                    }
                }

                // Если нашли период, возвращаем его длину
                if (isPeriodic)
                {
                    return periodLength;
                }
            }

            // Если период не найден, возвращаем 0
            return 0;
        }

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
    }
}
