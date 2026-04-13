namespace Moedelo.Infrastructure.Consul.Internals;

internal class ConsulQueryParams
{
    public const long UndefinedIndex = -1L;
    
    private long index;
    private readonly int waitOnConsulSideTimeoutSec;
    private readonly bool recurse;

    public ConsulQueryParams(bool recurse, int waitOnConsulSideTimeoutSec)
    {
        this.index = UndefinedIndex;
        this.recurse = recurse;
        this.waitOnConsulSideTimeoutSec = waitOnConsulSideTimeoutSec;
    }

    public long Index
    {
        get => index;
        set
        {
            if (value > this.index && value > 0)
            {
                // если индекс стал меньше текущего, то надо сбросить его значение я начать загружать данные без индекса.
                // аналогично индекс надо сбросить, если он не больше 0.
                // (см. https://developer.hashicorp.com/consul/api-docs/features/blocking)
                this.index = value;
                IsSetAtLeastOnce = true;
            }
            else
            {
                this.index = UndefinedIndex;
            }
        }
    }

    public bool IsUndefined => index == UndefinedIndex;

    public bool IsSetAtLeastOnce { get; private set; } = false;

    public string GetQueryParamsString()
    {
        if (recurse)
        {
            return IsUndefined ? "recurse" : $"recurse&wait={waitOnConsulSideTimeoutSec}s&index={index}";
        }
        
        return IsUndefined ? string.Empty : $"wait={waitOnConsulSideTimeoutSec}s&index={index}";
    }

    public bool HasTheSameIndex(long indexValue)
    {
        if (IsUndefined == false && indexValue == this.index)
        {
            return true;
        }

        return false;
    }
}
