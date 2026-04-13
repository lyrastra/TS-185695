using System.Collections.Generic;
using Moedelo.Infrastructure.Redis.Abstractions.Models;

namespace Moedelo.Infrastructure.Redis
{
    internal sealed class RedisConnectionEqualityComparer : IEqualityComparer<IRedisConnection>
    {
        public bool Equals(IRedisConnection x, IRedisConnection y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.ConnectionString == y.ConnectionString && x.DbNumber == y.DbNumber;
        }

        public int GetHashCode(IRedisConnection obj)
        {
            return $"{obj.ConnectionString}_{obj.DbNumber}".GetHashCode();
        }
    }
}