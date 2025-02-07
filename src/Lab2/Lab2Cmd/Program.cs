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

        }

    }
}
