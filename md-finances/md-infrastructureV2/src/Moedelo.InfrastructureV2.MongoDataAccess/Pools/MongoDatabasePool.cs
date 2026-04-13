using System.Collections.Concurrent;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.EqualityComparers;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Driver;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    [InjectAsSingleton]
    public sealed class MongoDatabasePool : IMongoDatabasePool
    {
        private readonly IMongoClientPool mongoClientPool;
        
        private readonly ConcurrentDictionary<MongoConnection, IMongoDatabase> pool =
            new ConcurrentDictionary<MongoConnection, IMongoDatabase>(new MongoConnectionEqualityComparer());

        public MongoDatabasePool(IMongoClientPool mongoClientPool)
        {
            this.mongoClientPool = mongoClientPool;
        }

        public IMongoDatabase GetMongoDatabase(MongoConnection connection)
        {
            var mongoDatabase = pool.GetOrAdd(connection, MongoDatabaseFactory);

            return mongoDatabase;
        }

        private IMongoDatabase MongoDatabaseFactory(MongoConnection connection)
        {
            var mongoClient = mongoClientPool.GetMongoClient(connection.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(connection.DatabaseName);

            return mongoDatabase;
        }
    }
}