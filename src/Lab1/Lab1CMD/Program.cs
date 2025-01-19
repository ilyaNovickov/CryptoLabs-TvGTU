﻿using Lab1Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Reflection;

namespace Lab1CMD
{
    internal class Program
    {

        static string[] strs = new string[]
        {
            new string('\u0010', 10),
            new string('\uffff', 513),

            "История английского языка — процесс, в результате которого диалект одного из германских племён, на котором в V веке говорили жители Британии, за полтора тысячелетия превратился" +
            " в язык, на котором общаются более 2 млрд человек по всему миру[1].\r\n\r\nАнглийский язык является запад" +
            "ногерманским языком, который возник на основе англо-фризских наречий, привнесённых в Британию в V—VII в" +
            "еках н. э. германскими завоевателями и поселенцами нынешней Северо-Западной Германии, Западной Дании и Нидерландов.\r\n\r\nДревнеанглийский язык англосаксонской эпохи развился " +
            "в среднеанглийский язык, на котором говорили со вре" +
            "мён нормандских завоеваний и до конца XV века. Значительное влияние на формирование английского оказали контакты с северогерманскими языками, на кото" +
            "рых говорили скандинавы, за" +
            "воевавшие и колонизировавшие Британию с VIII по IX век; этот контакт привёл к многочисленным лексическим з" +
            "аимствованиям и грамматическим упрощениям. Влияние на язык оказали завоевания норманнов, которые говорили на" +
            " старонормандском языке, который в Британии развился в англо-нормандский язык. Много норманнских и французских заимствований вошло в языковую лексику, относящуюся к церкви и с" +
            "удебной системе. Система орфографии, утвердившаяся в среднеанглийс" +
            "кий период, используется и сего" +
            "дня.\r\n\r\nРанний современный английский язык — язык Шекспира, был распространён примерно с 1500 года. В нём нашли отражение многие заимствования эпохи Ренессанса с латинског" +
            "о и древнегреческого языков, а также заимствования из других европейских языков, включая французский, " +
            "немецкий и голландский. Изменения в произношении в этот период включали в себя «великий сдвиг гласных» " +
            "(фонетические изменения в английском языке в XIV—XV веках), что сказалось на свойствах долгих гласных. Современный английский язык, на котором говорят и поныне, был в употреб" +
            "лении с конца XVII века. Со времён Британской колонизации " +
            "английский язык получил распространение в Великобритании, Ирландии, США, Канаде, Австралии, Новой Зеландии, Индии, части Африки и в других странах. " +
            "В настоящее время он также является средством межнационального общения — лингва франка.\r\n\r\nДревнеанглийский язык включал в себя различные группы диалектов, отражающих пр" +
            "оисхождение англосаксонских королевств, созданных в разных районах Великобритании. В итоге запад" +
            "но-саксонский диалект языка стал доминирующим. Большое " +
            "влияние на среднеанглийский язык оказал " +
            "древнеанглийский язык. Разновидностью английского языка является шотландский язык. На нём традиционно говорят в " +
            "некоторых районах Шотландии и Северной Ирландии. " +
            "Иногда он рассматривается как самостоятельный язык. ",

            "Чтобы этого не происходило, используются специальные дополнительные алгоритмы, " +
            "суть которых в том, что каждая предыдущая часть сообщения начинает влиять на следующую.\r\n\r\n" +
            "Упрощённо, это выглядит так. Перед шифрованием, мы применяем к сообщению правило: b := (b + a) % n." +
            " Где a — предыдущая часть сообщения, а b — следующая. То есть наше сообщение (11, 17, 15, 19) " +
            "изменяется. 11 остаётся без изменений. 17 превращается в (11 + 17) % 323 = 28. 15 становится (15 + 28) " +
            "% 323 = 43. A 19 превращается в 62.\r\n\r\nПоследовательность (11, 28, 43, 62) получается «запутанной». " +
            "Все буквы в ней как бы перемешаны, в том смысле, что на каждый код влияет не одна буква, а все предыдущие.",

            "Древнегреческие математики называли этот алгоритм ἀνθυφαίρεσις " +
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
            "рактата Математика в девяти книгах."

        };

        static void Main(string[] args)
        {
            //ModelingRSACrypting model = new ModelingRSACrypting();



            //for (int i = 0; i < 1000000000; i++)
            //{
            //    foreach (string str in strs)
            //    {
            //        model.Message = str;

            //        model.GenerateKeys();
            //        model.SendMessage();
            //        var test = model.ReadMessage();
            //    }
            //}

            uint p = 48847;
            uint q = 62099;
            BigInteger n = p * q;
            BigInteger eln = (BigInteger)(p - 1) * (BigInteger)(q - 1);

            BigInteger e = 534368047;

            BigInteger d = FindD(e, eln);

            BigInteger m = (e * d) % eln;

            MathExtra.ExtendedEvklidAlgorithm(e, eln, out BigInteger x, out BigInteger y);

            BigInteger min = BigInteger.Min(x, y);

            BigInteger d2 = eln - BigInteger.Abs(min);

            BigInteger m2 = (e * d2) % eln;


            Console.ReadLine();
        }

        static BigInteger FindD(BigInteger e, BigInteger phi)
        {
            var (gcd, x, y) = ExtendedGCD(e, phi);
            if (gcd != 1)
                throw new ArgumentException("e и φ(n) должны быть взаимно простыми");

            // d должно быть положительным
            return (x % phi + phi) % phi;
        }

        static (BigInteger gcd, BigInteger x, BigInteger y) ExtendedGCD(BigInteger a, BigInteger b)
        {
            if (b == 0)
                return (a, 1, 0);

            var (gcd, x1, y1) = ExtendedGCD(b, a % b);
            BigInteger x = y1;
            BigInteger y = x1 - (a / b) * y1;
            return (gcd, x, y);
        }


    }
}
