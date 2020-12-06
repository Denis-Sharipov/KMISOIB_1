using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace KMiSOIB
{
    //Алгоритм RSA
    class Task3
    {
        string message, encryptedMessage, decryptedMessage;
        public int p { get; }
        public int q { get; }
        public int d { get; }
        public int k { get; }
        public int n { get; }
        public int phi { get; }
        public int e { get; }
        private int maxCharCode;

        public Task3(string message, int p, int q, int d) 
        {
            //Исходные данные и параметры
            this.message = message; 
            this.p = p;
            this.q = q;
            this.d = d;

            n = p * q; //Модуль.
            phi = (p - 1) * (q - 1); //Формула Эйлера
            e = FindE(); //Открытый ключ
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

        public string GetPublicKey() {return e + " " + n; } //Открытый ключ
        public string GetPrivateKey() { return d + " " + n; } //Секретный ключ

        public string Encrypt() //Шифрование
        {
            foreach(var ch in message) 
            {
                int charIndex;
                if (Alphabet.GetCharCode(ch) >= 192 && Alphabet.GetCharCode(ch) <= 223)
                {
                    charIndex = Alphabet.GetCharCode(ch) - 191;
                }
                else if (Alphabet.GetCharCode(ch) >= 224 && Alphabet.GetCharCode(ch) <= 255)
                {
                    charIndex = Alphabet.GetCharCode(ch) - 223;
                }
                else throw new Exception($"Неверный символ!");



                maxCharCode = charIndex > maxCharCode ? charIndex : maxCharCode;
                if (maxCharCode >= n) throw new Exception($"Индекс символа {Alphabet.GetChar(maxCharCode)} = {maxCharCode} больше или равно n = {n}");

                var res = BigInteger.ModPow(charIndex, e, n);
                encryptedMessage += res + " ";
            }

            return encryptedMessage;
        }

        public string Decrypt() //Расшифрование
        {
            foreach (var ch in encryptedMessage.Trim().Split(' '))
            {
                var res = BigInteger.ModPow(int.Parse(ch), d, n);
                decryptedMessage += Alphabet.GetChar(((int)res + n) % n + 191);
            }

            return decryptedMessage;
        }

    }
}
