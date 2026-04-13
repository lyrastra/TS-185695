namespace Moedelo.InfrastructureV2.Domain.Models.Redis;

public struct RedisValue<TValue>
{
    public RedisValue()
    {
        IsSet = false;
    }

    public RedisValue(TValue value)
    {
        IsSet = true;
        Value = value;
    }

    /// <summary>
    /// признак того, что значение выставлено
    /// </summary>
    public bool IsSet { get; set; }
    /// <summary>
    /// Значение
    /// </summary>
    public TValue Value { get; set; }

    public TValue GetValueOrDefault(TValue defaultValue) => IsSet ? Value : defaultValue;
}
