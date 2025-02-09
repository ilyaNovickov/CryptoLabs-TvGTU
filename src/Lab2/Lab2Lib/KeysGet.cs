using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Lab2Lib
{
    public static class KeysGet
    {
        private static string[] prepositions = { "В", "БЕЗ", "ДО", "ИЗ", "К", "НА", "ПО", "О", "ОТ", "ПЕРЕД", 
            "ПРИ",
            "ЧЕРЕЗ", "С", "У", "ЗА", "НАД", "ОБ", "ПОД", "ПРО", "ДЛЯ" };

        public static IEnumerable<int> AnalizeKeys(string cryptedText, int lower, int upper, out List<string> decryptedMessages)
        {
            if (lower > upper)
                throw new Exception("Нижняя граница ключа не может быть больше вверхней");

            List<int> keys = new List<int>(1);

            decryptedMessages = new List<string>(1);

            Lab2Modeling lab = new Lab2Modeling();

            for (int k = lower; k <= upper; k++)
            {
                string str = lab.Decrypt(cryptedText, k);

                if (AnalizePreposition(str))
                {
                    keys.Add(k);
                    decryptedMessages.Add(str);
                }
            }

            return keys;

        }

        private static bool AnalizePreposition(string str)
        {
            string[] words = str.Split(new[] { ' ', '.', ',', '!', '?', ';', ':', '-', '(', ')' }, StringSplitOptions.RemoveEmptyEntries);

            return words.Any(word => prepositions.Contains(word));
        }
    }
}
