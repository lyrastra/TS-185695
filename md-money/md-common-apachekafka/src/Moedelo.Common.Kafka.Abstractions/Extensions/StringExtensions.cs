#nullable enable
using System;
using System.Runtime.CompilerServices;

namespace Moedelo.Common.Kafka.Abstractions.Extensions;

internal static class StringExtensions
{
    internal static string EnsureIsNotNullOrWhiteSpace(this string? value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Не может быть пустой строкой", paramName);
        }

        return value!;
    }
}
