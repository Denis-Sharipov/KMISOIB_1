using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
  //Шифрование ГОСТ 28147-89
{
    class Task2
    {
        private string message;
        private string key;

        public string l0, r0, x0, sumR0AndX0, filled, f, r1;

        public Task2(string message, string key)
        {
            this.message = message;
            this.key = key;
            Init();
        }

        private void Init()
        {
            l0 = Utills.StickedBinaryMsg(message.Substring(0, message.Length / 2)); //Блок L0 (32 бит)
            r0 = Utills.StickedBinaryMsg(message.Substring(message.Length / 2, message.Length / 2)); //Блок R0 (32 бит)
            x0 = Utills.StickedBinaryMsg(key); //Блок ключа X0 (32 бит)
            sumR0AndX0 = Utills.Modulo2Pow32(r0, x0); //Сложение блоков R0 и X0 по модулю 2^32
            filled = Function(sumR0AndX0); //Преобразование в блоке подстановки
            f = Utills.Shift(filled, -11); //f(R0, X0), сдвиг на 11 бит влево
            r1 = Utills.Modulo2(l0, f); //Результат R1: сложение по модулю 2 блоков L0 и f(R0, X0)
        }

        //Подстановочная таблица
        private readonly int[,] substituteTable = new int[,]
        {
           // 8   7  6  5  4  3  2   1. Столбцы соответствуют номерам блоков,строки соответствуют десятичному значению блоков
            { 1, 13, 4, 6, 7, 5, 14, 4 },       //0
            { 15, 11, 11, 12, 13, 8, 11, 10 },  //1
            { 13, 4, 10, 7, 10, 1, 4, 9 },      //2
            { 0, 1, 0, 1, 1, 13, 12, 2 },       //3
            { 5, 3, 7, 5, 0, 10, 6, 13 },       //4
            { 7, 15, 2, 15, 8, 3, 13, 8 },      //5
            { 10, 5, 1, 13, 9, 4, 15, 0 },      //6
            { 4, 9, 13, 8, 15, 2, 10, 14 },     //7
            { 9, 0, 3, 4, 14, 14, 2, 6 },       //8
            { 2, 10, 6, 10, 4, 15, 3, 11 },     //9
            { 3, 14, 8, 9, 6, 12, 8, 1 },       //10
            { 14, 7, 5, 14, 12, 7, 1, 12 },     //11
            { 6, 6, 9, 0, 11, 6, 0, 7 },        //12
            { 11, 8, 12, 3, 2, 0, 7, 15 },      //13
            { 8, 2, 15, 11, 5, 9, 5, 5 },       //14
            { 12, 12, 14, 2, 3, 11, 9, 3 }      //15
        };

        //Функция подстановки
        private string Function(string str)
        {
            StringBuilder substitutedStr = new StringBuilder();
            string[] blocks = Utills.BinaryFormat(str, 4).Split(' ');
            int col = 0;
            int row;

            foreach (var b in blocks)
            {
                row = Convert.ToInt32(b, 2);
                substitutedStr.Append(Utills.BinaryFormat(Convert.ToString(substituteTable[row, col], 2), 4));
                col++;
            }

            return substitutedStr.ToString();
        }
    }
}
