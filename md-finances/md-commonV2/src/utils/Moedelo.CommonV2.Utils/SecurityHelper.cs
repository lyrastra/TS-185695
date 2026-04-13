using System;
using System.Security.Cryptography;
using System.Text;

namespace Moedelo.CommonV2.Utils
{
    public static class SecurityHelper
    {
        /// <summary> Захешировать строку </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат</returns>
        public static string ComputeMD5Hash(string input)
        {
            MD5 md5Hasher = MD5.Create();

            Byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        
        /// <summary> Захешировать строку UTF-8</summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат</returns>
        public static string ComputeMD5HashFromUtf8(string input)
        {
            MD5 md5Hasher = MD5.Create();

            Byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary> Вычисляет хэш SHA512 для входных данных </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат</returns>
        public static string ComputeSHA512Hash(string input)
        {
            SHA512 sha512Hasher = SHA512.Create();

            Byte[] data = sha512Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary> Вычисляет хэш SHA256 для входных данных </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат</returns>
        public static string ComputeSHA256Hash(string input)
        {
            SHA256 sha256Hasher = SHA256.Create();

            Byte[] data = sha256Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}