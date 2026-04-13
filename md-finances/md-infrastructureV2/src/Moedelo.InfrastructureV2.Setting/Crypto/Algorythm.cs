using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Moedelo.InfrastructureV2.Setting.Crypto
{

    internal static class Algorythm
    {
        public enum AlgorythmType
        {
            Aes = 1
        }

        public static string Decrypt(this string value, AlgorythmType algorythm, string key, string nonce)
        {
            switch (algorythm)
            {
                case AlgorythmType.Aes:
                    return DecryptByAes(value, key, nonce);
                default:
                    throw new ArgumentOutOfRangeException(nameof(algorythm), algorythm, "");
            }
        }

        private static string DecryptByAes(string value, string key, string nonce)
        {
            using (var aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = Convert.FromBase64String(nonce);
                var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream(Convert.FromBase64String(value)))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}