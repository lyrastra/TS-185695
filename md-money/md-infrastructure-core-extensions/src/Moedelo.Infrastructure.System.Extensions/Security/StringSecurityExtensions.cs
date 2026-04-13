using System.Security.Cryptography;
using System.Text;

namespace Moedelo.Infrastructure.System.Extensions.Security
{
    public static class StringSecurityExtensions
    {
        /// <summary> Захешировать строку </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат</returns>
        /// <remarks> Это SecurityHelper.ComputeMD5Hash из md-commonV2/Common.Utils </remarks>
        public static string ToMD5(this string input)
        {
            using var md5Hasher = MD5.Create();

            var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary> Вычисляет хэш SHA512 для входных данных </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат</returns>
        /// <remarks> Это SecurityHelper.ComputeSHA512Hash из md-commonV2/Common.Utils </remarks>
        public static string ToSHA512(this string input)
        {
            using var sha512Hasher = SHA512.Create();

            var data = sha512Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            var sBuilder = new StringBuilder();

            for (var i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }

        /// <summary> Вычисляет хэш SHA256 для входных данных </summary>
        /// <param name="input">Входная строка</param>
        /// <returns>Результат</returns>
        /// /// <remarks> Это SecurityHelper.ComputeSHA256Hash из md-commonV2/Common.Utils </remarks>
        public static string ToSHA256(this string input)
        {
            return input.ToSHA256(Encoding.Default);
        }

        /// <summary> Вычисляет хэш SHA256 для входных данных </summary>
        /// <param name="input">Входная строка</param>
        /// <param name="encoding">кодировка, в которой представлена строка</param>
        /// <returns>Результат</returns>
        /// /// <remarks> Это SecurityHelper.ComputeSHA256Hash из md-commonV2/Common.Utils </remarks>
        public static string ToSHA256(this string input, Encoding encoding)
        {
            using var sha256Hasher = SHA256.Create();
            var data = sha256Hasher.ComputeHash(encoding.GetBytes(input));

            var sBuilder = new StringBuilder();

            foreach (var byteChar in data)
            {
                sBuilder.Append(byteChar.ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}