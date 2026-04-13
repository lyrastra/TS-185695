using System;
using System.Security.Cryptography;
using System.Text;

namespace Moedelo.Infrastructure.System.Extensions;

public static class StringSecurityExtensions
{
    /// <summary> Вычисляет хэш MD5 для входных данных </summary>
    /// <param name="input">Входная строка</param>
    /// <param name="encoding">кодировка, в которой представлена строка</param>
    /// <returns>Результат</returns>
    public static string ToMd5(this string input, Encoding encoding)
    {
        using var hasher = MD5.Create();
        var data = hasher.ComputeHash(encoding.GetBytes(input));

        return Convert.ToHexString(data).ToLower();
    }

    /// <summary> Вычисляет хэш SHA256 для входных данных </summary>
    /// <param name="input">Входная строка</param>
    /// <param name="encoding">кодировка, в которой представлена строка</param>
    /// <returns>Результат</returns>
    public static string ToSha256(this string input, Encoding encoding)
    {
        using var hasher = SHA256.Create();
        var data = hasher.ComputeHash(encoding.GetBytes(input));

        return Convert.ToHexString(data).ToLower();
    }

    /// <summary> Вычисляет хэш SHA512 для входных данных </summary>
    /// <param name="input">Входная строка</param>
    /// <param name="encoding">кодировка, в которой представлена строка</param>
    /// <returns>Результат</returns>
    public static string ToSha512(this string input, Encoding encoding)
    {
        using var hasher = SHA512.Create();
        var data = hasher.ComputeHash(encoding.GetBytes(input));

        return Convert.ToHexString(data).ToLower();
    }
}
