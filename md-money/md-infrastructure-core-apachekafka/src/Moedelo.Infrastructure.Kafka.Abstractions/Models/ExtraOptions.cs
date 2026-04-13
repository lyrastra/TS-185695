using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Moedelo.Infrastructure.Kafka.Abstractions.Models;

public sealed class ExtraOptions
{
    private readonly Dictionary<Type, object> extraOptions = new();

    public void Add<TExtraOptions>(TExtraOptions options)
    {
        Debug.Assert(options != null);
        Debug.Assert(!extraOptions.ContainsKey(typeof(TExtraOptions)), $"Настройки с типом {typeof(TExtraOptions)} уже добавлены");

        extraOptions[typeof(TExtraOptions)] = options!;
    }
    public TExtraOptions GetRequired<TExtraOptions>()
    {
        return extraOptions.TryGetValue(typeof(TExtraOptions), out var options)
            ? (TExtraOptions)options
            : throw new Exception($"Дополнительные настройки типа {typeof(TExtraOptions).Name} не найдены. Проверьте, что они добавлены");
    }
}
