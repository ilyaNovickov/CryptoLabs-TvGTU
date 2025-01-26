using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ImmutableArr = System.Collections.Immutable.ImmutableArray<System.Collections.Immutable.ImmutableArray<uint>>;

//500 * 500 = 250.000 ( 0b11 1101 0000 1001 0000 ) =>
//=> 131.071 
namespace Lab1Models
{
    public class ModelingRSACrypting
    {
        private const int messageMaxLength = 8192;
        private const int lengthofOneMessage = 512;

        //Нижная и верхняя границы поиска простых чисел
        private const int lowerBound = 500;
        private const int higherBound = 1000;

        //Максимальное число двоичных разрядов для шифрования
        private int maxModBinCount = 0;
        //Длина двоичного представления кода символа
        private static int charBinLength = 0;

        //Параметры RSA
        private uint p = 0;
        private uint q = 0;
        private BigInteger n = 0;
        private BigInteger d = 0;
        private BigInteger e = 0;

        //Сообщение для шифрования
        private string message = null;

        //Список зашифрованых сообщений
        private ImmutableArr cryptedMessage = ImmutableArr.Empty;
        //Список доступных для шифроапния символов
        private static List<char> avaibleChars;

        static ModelingRSACrypting()
        {
            //Определение набора символов
            avaibleChars = new List<char>();

            for (char i = '\u0000'; i <= '\u00bf'; i++)
            {
                avaibleChars.Add(i);
            }
            for (char i = '\u02b9'; i <= '\u035f'; i++)
            {
                avaibleChars.Add(i);
            }
            for (char i = '\u0410'; i <= '\u0482'; i++)
            {
                avaibleChars.Add(i);
            }
            //count : 474 => bin max : 521 = 0b 1 1111 1111

            //Подсчёт двоичной длины числа
            int binCounter = 0;
            int count = avaibleChars.Count;

            while (count != 0)
            {
                count >>= 1;
                binCounter++;
            }

            charBinLength = binCounter;
        }

        public uint P => p;
        public uint Q => q;
        public BigInteger N => n;
        public BigInteger D => d;
        public BigInteger E => e;

        /// <summary>
        /// Сообщение для шифрования
        /// При изменение сбрасывается шифр
        /// </summary>
        public string Message
        {
            get => message;
            set
            {
                if (value.Length > messageMaxLength)
                    throw new Exception($"Максимальная длина одного сообщения должшо не превышать {messageMaxLength} символов");

                //Если есть недоступный для шифрования символ
                //то сообщение не изменяется
                if (value.ToCharArray().Except(avaibleChars).Count() != 0)
                    return;

                message = value;
                CryptedMessage.Clear();
            }
        }

        /// <summary>
        /// Набор зафированых сообщений
        /// </summary>
        public ImmutableArr CryptedMessage
        {
            get => cryptedMessage;
            private set => cryptedMessage = value;
        }

        /// <summary>
        /// Определяет, сгенерированы ли ключи RSA
        /// </summary>
        public bool KeysGenerated
        {
            get; private set;
        }

        /// <summary>
        /// Максимальная длина одного сообщения
        /// </summary>
        public int LengthOfOneMessage => lengthofOneMessage;

        /// <summary>
        /// Максимальная длина всего сообщения
        /// </summary>
        public int MessageMaxLength => messageMaxLength;

        /// <summary>
        /// Событие логгирования
        /// </summary>
        public event LogEventHandler LoggingEvent;

        /// <summary>
        /// Генерация ключей RSA
        /// </summary>
        public void GenerateKeys()
        {
            GenerateKeys(ref p, ref q, ref n, ref e, ref d);

            KeysGenerated = true;
        }

        /// <summary>
        /// Определение максимального модификатора сообщений
        /// </summary>
        private void SetExtraValues()
        {
            int t = ((int)N);

            t >>= 1;

            int counterBin = 0;
            int maxValue = 1;

            while (t != 0)
            {
                counterBin++;
                t >>= 1;
                maxValue *= 2;
            }

            maxValue -= 1;
            //counterBin -= 1;

            //this.maxModCode = maxValue;
            this.maxModBinCount = counterBin;
        }

        private void GenerateKeys(ref uint p, ref uint q, ref BigInteger n, 
            ref BigInteger e, ref BigInteger d)
        {
            LoggingEvent?.Invoke(null, new LogEventArgs($"Начало генерации ключей"));

            Random rnd = new Random();

//Начало генерации ключей
sign:
//Генерация p
            do
            {
                //p = rnd.NextUint(0U, ushort.MaxValue);
                p = rnd.NextUint(lowerBound, higherBound);
                //p = 601;
            } while (!MathExtra.IsPrime(p));

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число p := {p}"));

            //Генерация q
            do
            {
                //q = rnd.NextUint(0U, ushort.MaxValue);
                q = rnd.NextUint(lowerBound, higherBound);
                //q = 883;
            } while (!MathExtra.IsPrime(q) || p == q);

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число q := {q}"));

            //Определение модуля n
            n = (BigInteger)p * (BigInteger)q;

            LoggingEvent?.Invoke(null, new LogEventArgs($"Определено число \"n = p * q\" := {n}"));

            //Так как символьные литералы в C# представлены в UniCode,
            //то их можно представить как числа от 0 до 2^16 - 1 (т. е. ushort)
            //Значит, что n должен быть больше ushort.MaxValue, чтобы 
            //можно было закодировать все символы
            if (n <= ushort.MaxValue) //* lengthofOneMessage)
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
                e = rnd.NextUint(0U, (uint)eln);
            } while (!MathExtra.FindMutuallyPrimeNumbers(e, eln) || !(1 < e && e < eln));

            LoggingEvent?.Invoke(null, new LogEventArgs($"Сгенерировано число e := {e}"));

            //Определение ключа d
            {
                MathExtra.ExtendedEvklidAlgorithm(e, eln, out BigInteger x, out BigInteger y);


                d = (x % eln + eln) % eln;

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

            SetExtraValues();
        }

        /*
         * RSA алгоритм может кодировать сиволы в интервале от 0 до n-1
         * Код символа сдвигается на несколько разрядов влево
         * чтобы первые разряды были кодом символа, а остальная часть была модификатором
         * Модификатор заполнается числом, равным кол-во символов
         * 
         * Пример:
         * Символ с кодом 511 (0b0001 1111 1111)
         * n = 8 191 (0b0001 1111 1111 1111)
         * Чтобы были место для мановрёв, сдвиг будет происходить до самого старшего значимого бита
         * Смещает:
         * 0b0000 0001 1111 1111 << Значение сдвига = 0b0000 1111 1111 1111 1???
         * В ? будет храниться модификатор; здесь модификатор будет определён в интервале
         * от 0b000 до 0b111.
         * Если достигнут максимальный модификатор, то происходит кодировка нового сообщения
         * (одно исходное сообщение кодируется в несколько зашифрованых)
         */
        public void SendMessage()
        {
            if (!KeysGenerated)
                throw new Exception("Ключи не сгенерированы");

            if (message == null || message.Length == 0)
                throw new Exception("Сообщение для отправки отсуствует");

            this.CryptedMessage = ImmutableArr.Empty;
            ImmutableArr.Builder builder = this.CryptedMessage.ToBuilder();

            LoggingEvent?.Invoke(this, new LogEventArgs("Начало кодировки сообщений"));

            List<uint> cryptedMessage = new List<uint>();

            int countofMessage = 1;//Кол-во сообщений
            int messageIndex = 0;//Индекс символа сообщения

            for (int messageToCrypt = 0; messageToCrypt < countofMessage; messageToCrypt++)
            {
                LoggingEvent?.Invoke(this, new LogEventArgs($"Кодировка сообщения №{messageToCrypt}"));

                Dictionary<char, uint> modDictionary = new Dictionary<char, uint>();
                List<uint> cryptedValues = new List<uint>();

                for (; messageIndex < message.Length; messageIndex++)
                {
                    char symbol = message[messageIndex];

                    uint code = 0;

                    //Определение кода символа
                    {
                        int codeYoCrypt = avaibleChars.IndexOf(symbol);

                        if (codeYoCrypt == -1)
                            throw new Exception("Недоступный для шифрования символ");

                        code = (uint)codeYoCrypt;
                    }

                    code <<= (maxModBinCount - charBinLength);

                    if (!modDictionary.ContainsKey(symbol))
                    {
                        modDictionary.Add(symbol, 0u);
                    }
                    else
                    {
                        int maxVal = MathExtra.PowInt(2, maxModBinCount - charBinLength) - 1;
                        int modificator = (int)modDictionary[symbol];

                        if (modificator != maxVal)
                        {
                            code += ((uint)modificator + 1);
                            modDictionary[symbol] = (uint)modificator + 1;
                        }
                        else
                        {
                            countofMessage++;
                            //builder.Add(cryptedValues.ToImmutableArray<uint>());
                            //cryptedValues.Clear();
                            //uint cryptedValue2 = (uint)MathExtra.ModularExponentiation(code, e, n);
                            //cryptedValues.Add(cryptedValue2);
                            modDictionary.Clear();
                            break;
                        }
                    }

                    uint cryptedValue = (uint)MathExtra.ModularExponentiation(code, e, n);
                    cryptedValues.Add(cryptedValue);
                    
                }

                builder.Add(cryptedValues.ToImmutableArray<uint>());

                LoggingEvent?.Invoke(this, new LogEventArgs($"Конец кодировки сообщения №{messageToCrypt}"));
            }


            CryptedMessage = builder.ToImmutableArray();
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
                ImmutableArray<uint> oneMessage = cryptedMessage[messageNumber];

                for (int i = 0; i < oneMessage.Length; i++)
                {
                    uint decode = (uint)MathExtra.ModularExponentiation(oneMessage[i], d, n);

                    decode >>= maxModBinCount - charBinLength;

                    char symbol = avaibleChars[(int)decode];

                    decryptedMessage += symbol;
                }

                LoggingEvent?.Invoke(this, new LogEventArgs($"Окончание дешифрации сообщения №{messageNumber}"));
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

        
        struct CryptFix
        {
            public int NumbeofMessage { get; set; }
            public int Index { get; set; }
        }
    }
}
