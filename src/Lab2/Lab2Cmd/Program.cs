using Lab2Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2Cmd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //e = (x % eln + eln) % eln;
            //int k = 5;
            //int r1 = (10 + k) % 33;
            //int r2 = (20 + k) % 33;
            //int r3 = (30 + k) % 33;

            //int t1 = ( (r1 - k) % 33 + 33 ) % 33;
            //int t2 = ((r2 - k) % 33 + 33) % 33;
            //int t3 = ((r3 - k) % 33 + 33) % 33;

            Lab2Lib.Lab2Modeling text = new Lab2Lib.Lab2Modeling();

            int k = 30;

            string crypt = text.Crypt("ЭКЗАМЕН", k);

            string decrypt = text.Decrypt(crypt, k);

            var res = text.CheckGamma(k, 30);

            text.PeriodAnalizer(new int[] { 2, 4, 8, 2, 4, 8 });

            int index = text.PeriodAnalizer(new int[] { 2, 4, 8, 0, 0, 0, 0, 0, 0, 0 });//text.PeriodAnalizer(res);


            string text1 = "Данная часть речи сформировалась за счет других лексикограмматических разрядов " +
                "Этим во многом обусловлена неоднородность предлогов В течение девятнатчатого дватцатых веков наблюдается " +
                "непрерывное пополнение состава производных предлогов " +
                "Интереснее всего развиваются предлоги выражающие наиболее " +
                "отвлеченные значения  объективные причинные целевые и т д " +
                "В развитии новых предлогов сказывается возрастающая роль в " +
                "русском языке девятнатчатого века публицистической и научной речи ";

            var keys = KeysGet.AnalizeKeys(text.Crypt(text1.ToUpper(), 10), 0, 100);
        }

    }
}
