#nullable enable
using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.Setting.Extensions;

internal static class ReadOnlyDictionaryExtensions
{
    internal static string? GetValueOrNull(this IReadOnlyDictionary<string, string> dictionary, string key)
    {
        return dictionary.TryGetValue(key, out var value) ? value : null;
    }
}
