using System;

namespace Moedelo.InfrastructureV2.Domain.Models.EventBus;

public class ConsumerWithRetryOptions : ConsumerOptionsBase
{
    private const uint DefaultMaxRetryCount = 30;
    private const ushort DefaultPrefetchCount = 10;
    private static TimeSpan GetDefaultRetryDelay(uint _) => TimeSpan.FromMinutes(30);   

    public ConsumerWithRetryOptions(
        uint maxRetryCount = DefaultMaxRetryCount,
        Func<uint, TimeSpan> delayFunc = null,
        ushort prefetchCount = DefaultPrefetchCount,
        bool skipRedelivered = false,
        bool logErrorOnlyOnLastRetryFailure = false) : base(prefetchCount)
    {
        DelayFunc = delayFunc ?? GetDefaultRetryDelay;
        MaxRetryCount = maxRetryCount;
        SkipRedelivered = skipRedelivered;
        LogErrorOnlyOnLastRetryFailure = logErrorOnlyOnLastRetryFailure;
    }

    /// <summary>
    /// Количество попыток обработки сообщения
    /// </summary>
    public uint MaxRetryCount { get; }

    public bool SkipRedelivered { get; }

    public Func<uint, TimeSpan> DelayFunc { get; }

    /// <summary>
    /// Логировать ошибку только при последней попытке, если она завершилась неудачно.
    /// Используйте эту опцию, если в вашем обработчике переход на повторную попытку штатно делается выбрасыванием исключения.
    /// </summary>
    public bool LogErrorOnlyOnLastRetryFailure { get; }
}