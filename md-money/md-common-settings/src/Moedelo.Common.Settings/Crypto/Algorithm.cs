using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Moedelo.Common.Settings.Crypto;

internal static class Algorithm
{
    public enum AlgorithmType
    {
        Aes = 1
    }
    
    public static string Decrypt(this string value, AlgorithmType algorithm, string key, string nonce)
    {
        return algorithm switch
        {
            AlgorithmType.Aes => DecryptByAes(value, key, nonce),
            _ => throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, "Этот алгоритм не поддерживается")
        };
    }
    
    private static string DecryptByAes(string value, string key, string nonce)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key);
        aes.IV = Convert.FromBase64String(nonce);
        var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using var memoryStream = new MemoryStream(Convert.FromBase64String(value));
        using var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        using var streamReader = new StreamReader(cryptoStream);

        return streamReader.ReadToEnd();
    }
}
