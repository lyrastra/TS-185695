using System.Collections.Generic;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools.EqualityComparers
{
    internal sealed class MongoCollectionConnectionEqualityComparer : IEqualityComparer<MongoCollectionConnection>
    {
        public bool Equals(MongoCollectionConnection x, MongoCollectionConnection y)
        {
            if (x == null && y == null)
            {
                return true;
            }

            if (x == null || y == null)
            {
                return false;
            }

            return x.ConnectionString == y.ConnectionString
                   && x.DatabaseName == y.DatabaseName
                   && x.CollectionName == y.CollectionName;
        }

        public int GetHashCode(MongoCollectionConnection obj)
        {
            return $"{obj.ConnectionString}_{obj.DatabaseName}_{obj.CollectionName}".GetHashCode();
        }
    }
}