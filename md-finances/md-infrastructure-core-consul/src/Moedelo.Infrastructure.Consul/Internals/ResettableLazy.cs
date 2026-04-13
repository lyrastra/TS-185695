using System;

namespace Moedelo.Infrastructure.Consul.Internals;

/// <summary>
/// Класс для управления ленивым значением с возможностью сброса.
/// </summary>
internal sealed class ResettableLazy<TValue>
{
    private readonly Func<TValue> factory;
    private TValue value;
    private readonly object lockObject = new object();

    public ResettableLazy(Func<TValue> factory)
    {
        this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }

    public TValue Value
    {
        get
        {
            if (!IsValueCreated)
            {
                lock (lockObject)
                {
                    if (!IsValueCreated)
                    {
                        value = factory();
                        IsValueCreated = true;
                    }
                }
            }
            return value;
        }
    }

    public bool IsValueCreated { get; private set; }

    /// <summary>
    /// Сбрасывает значение
    /// </summary>
    public void Reset()
    {
        lock (lockObject)
        {
            value = default;
            IsValueCreated = false;
        }
    }
}
