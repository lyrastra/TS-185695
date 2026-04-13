using System;

namespace Moedelo.InfrastructureV2.Domain.Models.Redis;

public class RedisQueryObject
{
    public RedisQueryObject()
    {
        AttemptCount = 1;
        Delay = new TimeSpan();
        Expiry = new TimeSpan(0, 10, 0);
    }

    /// <summary> Количество попыток записать ключ в Redis </summary>
    public int AttemptCount { get; set; }

    /// <summary> Время, через которое осуществлять повторные попытки записи ключа </summary>
    public TimeSpan Delay { get; set; }

    /// <summary> Как долго хранить ключ в Redis</summary>
    public TimeSpan Expiry { get; set; }

    /// <summary> Значение, которое будет передано в ConfigureAwait() в стеке вызовов </summary>
    public bool ConfigureAwaitValue { get; set; }
}