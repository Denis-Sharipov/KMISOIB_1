using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
    //ЭЦП
{
    class Task5
    {
        private string message;
        public int p { get; }
        public int q { get; }
        public int n { get; }
        public int h0 { get; }
        public int d { get; }
        public int k { get; }
        public int phi { get; }
        public int e { get; }

        private int hash;
        private int s;
        private int h;


        public Task5(string message, int p, int q, int d, int h0)
        {
            this.message = message; //Исходное сообщение
            this.p = p; //Значение p
            this.q = q; //Значение q
            this.d = d; //Значение d
            this.h0 = h0; //Вектор инициализации

            n = p * q; //Значение n
            phi = (p - 1) * (q - 1); //Функция Эйлера
            e = FindE(); //Знаение e
            Hash(); //Функция нахождения хеш-образа
        }


        private int FindE() //Нахождение открытого ключа e
        {
            int e = 0;
            while ((d * e - 1) % phi != 0)
            {
                e++;
            }

            return e;
        }

        public string GetPublicKey() { return e + " " + n; } //Открытый ключ
        public string GetPrivateKey() { return d + " " + n; } //Закрытый ключ

        public int Hash() //Нахождение хеш-образа
        {
            hash = h0;

            foreach (var ch in message.Substring(0))
            {
                int charIndex; //Значение символа по алфавиту
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
        private int f(int a, int b) //Нахождение значений по формуле ((H(i)+M(i-1))^2)modn
        {
            return (int)BigInteger.ModPow(a + b, 2, n);
        }
    

        public int Encrypt() //Шифрование
        {
            s = (int)BigInteger.ModPow(hash, d, n);

            return s;
        }

        public int Decrypt() //Дешифрование
        {
            h = (int)BigInteger.ModPow(s, e, n);

            return h;
        }
    }
}
