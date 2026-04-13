using System;
using System.Security.Cryptography;
using System.Text;

namespace Moedelo.CommonV2.Extensions.System
{
    public static class StringEx
    {
        public static string ToUTF8(this string text)
        {
            var bytes = Encoding.Default.GetBytes(text);

            return Encoding.UTF8.GetString(bytes);
        }

        public static string ToMd5Hash(this string text)
        {
            using (var md5Hasher = MD5.Create())
            {
                var utf8Encoder = Encoding.UTF8;

                var bytes = utf8Encoder.GetBytes(text);
                bytes = md5Hasher.ComputeHash(bytes);

                return BitConverter.ToString(bytes).Replace("-", string.Empty).ToLower();
            }
        }

        public static string ToSha512Hash(this string text)
        {
            var sha512Hasher = SHA512.Create();
            var data = sha512Hasher.ComputeHash(Encoding.Default.GetBytes(text));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string ToSha256Hash(this string text, Encoding encoding)
        {
            var sha512Hasher = SHA256.Create();
            var data = sha512Hasher.ComputeHash(encoding.GetBytes(text));
            var sBuilder = new StringBuilder();
            foreach (var t in data)
            {
                sBuilder.Append(t.ToString("x2"));
            }
            return sBuilder.ToString();
        }

        public static string Truncate(this string text, int length)
        {
            if (string.IsNullOrEmpty(text) || text.Length <= length)
            {
                return text;
            }

            return text.Substring(0, length);
        }

        /// <summary>
        /// Formats the string according to the specified mask
        /// </summary>
        /// <param name="input">The input string.</param>
        /// <param name="mask">The mask for formatting. Like "A##-##-T-###Z"</param>
        /// <returns>The formatted string</returns>
        public static string FormatWithMask(this string input, string mask)
        {
            if (string.IsNullOrEmpty(input)) return input;
            var output = new StringBuilder();
            var index = 0;
            foreach (var m in mask)
            {
                if (m == '#')
                {
                    if (index < input.Length)
                    {
                        output.Append(input[index]);
                        index++;
                    }
                }
                else
                    output.Append(m);
            }
            return output.ToString();
        }
    }
}