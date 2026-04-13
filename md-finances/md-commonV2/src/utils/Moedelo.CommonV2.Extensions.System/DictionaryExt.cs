using System.Collections.Generic;

namespace Moedelo.CommonV2.Extensions.System;

public static class DictionaryExt
{
    public static TV GetValueOrDefault<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV defaultValue = default(TV))
    {
        return dictionary.TryGetValue(key, out var v) ? v : defaultValue;
    }

    public static TV GetValueOrDefault<TK, TV>(this IReadOnlyDictionary<TK, TV> dictionary, TK key, TV defaultValue = default(TV))
    {
        return dictionary.TryGetValue(key, out var v) ? v : defaultValue;
    }

    public static TV GetValueOrDefault<TK, TV>(this IDictionary<TK, TV> dictionary, TK key, TV defaultValue = default(TV))
    {
        return dictionary.TryGetValue(key, out var v) ? v : defaultValue;
    }
}