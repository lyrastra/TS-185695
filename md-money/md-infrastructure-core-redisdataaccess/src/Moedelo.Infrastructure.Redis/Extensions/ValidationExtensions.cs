using System;
using System.Collections.Generic;

namespace Moedelo.Infrastructure.Redis.Extensions;

internal static class ValidationExtensions
{
    internal static void ValidateHashSet(this IReadOnlyCollection<KeyValuePair<string, string>> dictionary)
    {
        foreach (var item in dictionary)
        {
            if (item.Key == null)
                throw new ArgumentNullException(nameof(item.Key), "Словарь не может содержать null ключ");
            if (item.Value == null)
                throw new ArgumentNullException(nameof(item.Key), $"Словарь не может содержать null значение (ключ {item.Value}");
        }
    }
}
