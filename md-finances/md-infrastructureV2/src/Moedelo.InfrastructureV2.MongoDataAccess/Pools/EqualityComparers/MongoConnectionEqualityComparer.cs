using System.Collections.Generic;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools.EqualityComparers
{
    internal sealed class MongoConnectionEqualityComparer : IEqualityComparer<MongoConnection>
    {
        public bool Equals(MongoConnection x, MongoConnection y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.ConnectionString == y.ConnectionString && x.DatabaseName == y.DatabaseName;
        }

        public int GetHashCode(MongoConnection obj)
        {
            return $"{obj.ConnectionString}_{obj.DatabaseName}".GetHashCode();
        }
    }
}