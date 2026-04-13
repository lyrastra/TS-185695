using System.Collections.Concurrent;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.EqualityComparers;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Driver.GridFS;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    [InjectAsSingleton]
    public sealed class GridFSBucketPool : IGridFSBucketPool
    {
        private readonly IMongoDatabasePool mongoDatabasePool;
        
        private readonly ConcurrentDictionary<MongoConnection, IGridFSBucket> pool =
            new ConcurrentDictionary<MongoConnection, IGridFSBucket>(new MongoConnectionEqualityComparer());

        public GridFSBucketPool(IMongoDatabasePool mongoDatabasePool)
        {
            this.mongoDatabasePool = mongoDatabasePool;
        }
        
        public IGridFSBucket GetGridFSBucket(MongoConnection connection)
        {
            var gridFsBucket = pool.GetOrAdd(connection, AddGridFSBucketToDictionary);

            return gridFsBucket;
        }

        private IGridFSBucket AddGridFSBucketToDictionary(MongoConnection connection)
        {
            var mongoDatabase = mongoDatabasePool.GetMongoDatabase(connection);
            var gridFsBucket = new GridFSBucket(mongoDatabase);

            return gridFsBucket;
        }
    }
}