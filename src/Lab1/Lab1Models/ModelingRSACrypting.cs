﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ImmutableArr = System.Collections.Immutable.ImmutableArray<System.Collections.Immutable.ImmutableArray<ulong>>;

namespace Lab1Models
{
    public class ModelingRSACrypting
    {
        private const int messageMaxLength = 8192;
        private const int lengthofOneMessage = 512;

        private uint p = 0;
        private uint q = 0;
        private BigInteger n = 0;
        private BigInteger d = 0;
        private BigInteger e = 0;

        private string message = null;

        private ImmutableArr cryptedMessage = ImmutableArr.Empty;

        private List<Test> tests = new List<Test>();

        public string Message
        {
            get => message;
            set
            {
                if (value.Length > messageMaxLength)
                    throw new Exception($"Максимальная длина одного сообщения должшо не превышать {messageMaxLength} символов");
                message = value;
                CryptedMessage.Clear();
            }
        }

        public ImmutableArr CryptedMessage
        {
            get => cryptedMessage;
            private set => cryptedMessage = value;
        }

        public bool KeysGenerated
        {
            get; private set;
        }

        public int LengthOfOneMessage => lengthofOneMessage;

        public int MessageMaxLength => messageMaxLength;

        public static event LogEventHandler LoggingEvent;

        public void GenerateKeys()
        {
            GenerateKeys(ref p, ref q, ref n, ref e, ref d);

            KeysGenerated = true;
        }

        private static void GenerateKeys(ref uint p, ref uint q, ref BigInteger n, 
            ref BigInteger e, ref BigInteger d)
        {
            LoggingEvent?.Invoke(null, new LogEventArgs($"Начало генерации ключей"));

            Random rnd = new Random();

//Начало генерации ключей
sign:
//Генерация p
            do
            {
                p = rnd.NextUint(0U, ushort.MaxValue);
            } while (!MathExtra.IsPrime(p));

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число p := {p}"));

            //Генерация q
            do
            {
                q = rnd.NextUint(0U, ushort.MaxValue);
            } while (!MathExtra.IsPrime(q) || p == q);

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число q := {q}"));

            //Определение модуля n
            n = (BigInteger)p * (BigInteger)q;

            LoggingEvent?.Invoke(null, new LogEventArgs($"Определено число \"n = p * q\" := {n}"));

            //Так как символьные литералы в C# представлены в UniCode,
            //то их можно представить как числа от 0 до 2^16 - 1 (т. е. ushort)
            //Значит, что n должен быть больше ushort.MaxValue, чтобы 
            //можно было закодировать все символы
            if (n <= ushort.MaxValue * lengthofOneMessage)
            {
                LoggingEvent?.Invoke(null, new LogEventArgs($"Модуль n = {n} не может закодировать все символьные литералы кодировки UniCode\n" +
                    $"Повторная генерация p и q"));
                goto sign;
            }

            //Фуркция Эйлера
            BigInteger eln = (BigInteger)(p - 1) * (BigInteger)(q - 1);

            LoggingEvent?.Invoke(null, new LogEventArgs($"Вычисление функции Эйлера Ф = \"(n - 1) * (q - 1)\" := {eln}"));

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
        }

        public void SendMessage()
        {
            if (!KeysGenerated)
                throw new Exception("Ключи не сгенерированы");

            if (message == null || message.Length == 0)
                throw new Exception("Сообщение для отправки отсуствует");

            //Разрезание 1-ого сообщения на несколько определённой длины
            string[] littleMessage = CuttingMessage(message);

            LoggingEvent?.Invoke(this, new LogEventArgs($"Сообщение было разделено на {littleMessage.Length} подсообщений {lengthofOneMessage} символов каждое"));

            this.CryptedMessage = ImmutableArr.Empty;
            ImmutableArr.Builder builder = this.CryptedMessage.ToBuilder();

            LoggingEvent?.Invoke(this, new LogEventArgs("Начало кодировки сообщений"));

            for (int messageNumber = 0; messageNumber < littleMessage.Length; messageNumber++)
            {
                LoggingEvent?.Invoke(this, new LogEventArgs($"Шифрация сообщения №{messageNumber}"));

                //Сообщение для шифрования
                string messagePart = littleMessage[messageNumber];

                //Шифромассив
                List<ulong> cryptedMessage = new List<ulong>(lengthofOneMessage);

                //Предыдущий код
                BigInteger prevCode = 0;

                for (int i = 0; i < messagePart.Length; i++)
                {
                    BigInteger code = 0;

                    //За каждым символьным литералом скрывается число от 0 до 2^16 - 1
                    //Так как числа могут часто повторяться, то числа кодируюся как
                    //b = (b + a) % n, где a - это превыдущий код числа 

                    //Перекодировка символов
                    if (i == 0)
                        code = messagePart[i];
                    else
                        code = (messagePart[i] + prevCode) % n;

                    //Сохранение предыдущего символа
                    prevCode = code;

                    //Кодируем символ
                    code = MathExtra.ModularExponentiation(code, e, n);

                    //На случай, если в шифросообщение есть повторяющиеся символы
                    if (cryptedMessage.Contains((ulong)code))
                    {
                        throw new Exception("oops");

                        Test test = new Test()
                        {
                            Index = cryptedMessage.IndexOf((ulong)code),
                            NumbeofMessage = messageNumber
                        };
                        tests.Add(test);
                    }


                    //Кодируем сообщение
                    cryptedMessage.Add((ulong)code);
                }

                //Сохраняем шифрсообщение
                builder.Add(cryptedMessage.ToImmutableArray<ulong>());

                LoggingEvent?.Invoke(this, new LogEventArgs($"Окончание шифрации сообщения №{messageNumber}"));
            }

            CryptedMessage = builder.ToImmutable();

            LoggingEvent?.Invoke(this, new LogEventArgs("Сообщение закодировано"));
        }

        public string ReadMessage()
        {
            if (cryptedMessage.Length == 0)
            {
                LoggingEvent?.Invoke(this, new LogEventArgs("Нет шифросообщения"));
                return null;
            }

            string decryptedMessage = "";

            LoggingEvent?.Invoke(this, new LogEventArgs("Начало дешифрации сообщений"));

            for (int messageNumber = 0; messageNumber < cryptedMessage.Length; messageNumber++)
            {
                LoggingEvent?.Invoke(this, new LogEventArgs($"Дешифрация сообщения №{messageNumber}"));

                //Шифрсообщение
                ImmutableArray<ulong> oneMessage = cryptedMessage[messageNumber];

                //Код предыдущего сооьщения
                long prevCode = 0;

                for (int i = 0; i < oneMessage.Length; i++)
                {
                    //Расшифровка сообщения
                    long decryptedCode = (long)MathExtra.ModularExponentiation(oneMessage[i], d, n);

                    //Ести код равень 0, то переход к следующему символу
                    if (decryptedCode == 0)
                    {
                        continue;
                    }

                    //За каждым символьным литералом скрывается число от 0 до 2^16 - 1
                    //Так как числа могут часто повторяться, то числа кодируюся как
                    //b = (b + a) % n, где a - это превыдущий код числа (ДО КОДИРОВКИ)
                    //Для расшифровку использую формулу
                    //b = (b - a) % n, где a - это предыдущий символ ДО КОДИРОВКИ
                    //а это значит, что после этой расшифроки

                    //Расшифровка символов
                    if (i == 0)
                        decryptedMessage += (char)(decryptedCode);
                    else
                        decryptedMessage += (char)((decryptedCode - prevCode) % n);

                    //Сохряняем предыдущий расшифрованый символ
                    prevCode = decryptedCode;

                    LoggingEvent?.Invoke(this, new LogEventArgs($"Окончание дешифрации сообщения №{messageNumber}"));
                }
            }

            LoggingEvent?.Invoke(this, new LogEventArgs("Конец дешифрации сообщений"));

            return decryptedMessage;
        }

        private string[] CuttingMessage(string message)
        {
            int n = message.Length / lengthofOneMessage + 1;

            string[] littleMessage = new string[n];

            for (int i = 0; i < n; i++)
            {
                int modLength = message.Length - i * lengthofOneMessage;

                if (modLength >= lengthofOneMessage)
                    littleMessage[i] = message.Substring(i * lengthofOneMessage, lengthofOneMessage);
                else
                    littleMessage[i] = message.Substring(i * lengthofOneMessage, modLength);
            }

            return littleMessage;
        }

        
        struct Test
        {
            public int NumbeofMessage { get; set; }
            public int Index { get; set; }
        }
    }
}
