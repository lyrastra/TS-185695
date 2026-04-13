using System.Collections.Generic;

namespace Moedelo.InfrastructureV2.EventBus.Internal
{
    internal sealed class ExchangeConnectionEqualityComparer : IEqualityComparer<ExchangeConnection>
    {
        public bool Equals(ExchangeConnection x, ExchangeConnection y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.ConnectionString == y.ConnectionString && x.ExchangeName == y.ExchangeName;
        }

        public int GetHashCode(ExchangeConnection obj)
        {
            return $"{obj.ConnectionString}_{obj.ExchangeName}".GetHashCode();
        }
    }
}