using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    //Хеш-функция
    class Task4
    {
        private string message;
        public int p { get; }
        public int q { get; }
        public int n { get; }
        public int h0 { get; }

        private int hash;

        public Task4(string message, int p, int q, int h0) //Хеш-функция
        {
            this.message = message; //Хешируемое сообщение
            this.p = p; //Значение p
            this.q = q; //Значение q
            this.h0 = h0; //Вектор инициализации
            n = p * q; //Значение n
        }



        public int Hash() //Нахождение хеш-образа
        {
            hash = h0;

            foreach (var ch in message.Substring(0))
            {
                int charIndex; //Значение по алфавиту
                if (Alphabet.GetCharCode(ch) >= 192 && Alphabet.GetCharCode(ch) <= 223)
                {
                    charIndex = Alphabet.GetCharCode(ch) - 191;
                }
                else if (Alphabet.GetCharCode(ch) >= 224 && Alphabet.GetCharCode(ch) <= 255)
                {
                    charIndex = Alphabet.GetCharCode(ch) - 223;
                }
                else throw new Exception($"Недопустимый символ!"); 

                hash = f(hash, charIndex);
                Console.WriteLine(hash);
            }

            return hash;
        }
        private int f(int a, int b) //Нахождение значений хеш-функции по ((H(i)+M(i-1))^2)modn
        {
            return (int)BigInteger.ModPow(a + b, 2, n);
        }
    }
}
