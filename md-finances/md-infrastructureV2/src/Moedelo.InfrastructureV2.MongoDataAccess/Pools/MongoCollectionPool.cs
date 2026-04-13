using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.EqualityComparers;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Driver;
using System.Collections.Concurrent;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    [InjectAsSingleton]
    public sealed class MongoCollectionPool : IMongoCollectionPool
    {
        private readonly IMongoDatabasePool mongoDatabasePool;

        private readonly ConcurrentDictionary<MongoCollectionConnection, object> pool =
            new ConcurrentDictionary<MongoCollectionConnection, object>(
                new MongoCollectionConnectionEqualityComparer());

        public MongoCollectionPool(IMongoDatabasePool mongoDatabasePool)
        {
            this.mongoDatabasePool = mongoDatabasePool;
        }

        public IMongoCollection<T> GetMongoCollection<T>(MongoCollectionConnection connection) where T : class, IMongoObject
        {
            var mongoCollection = pool.GetOrAdd(connection, MongoCollectionFactory<T>) as IMongoCollection<T>;

            return mongoCollection;
        }

        private IMongoCollection<T> MongoCollectionFactory<T>(MongoCollectionConnection connection) where T : class, IMongoObject
        {
            var mongoDatabase = mongoDatabasePool.GetMongoDatabase(connection);
            var mongoCollection = mongoDatabase.GetCollection<T>(connection.CollectionName);

            if (mongoCollection != null)
            {
                SetupMapping<T>();
                return mongoCollection;
            }
            
            mongoDatabase.CreateCollection(connection.CollectionName);
            mongoCollection = mongoDatabase.GetCollection<T>(connection.CollectionName);

            SetupMapping<T>();
            return mongoCollection;
        }

        private static void SetupMapping<T>() where T : class, IMongoObject
        {
            if (BsonClassMap.IsClassMapRegistered(typeof(T)))	
            {	
                return;	
            }

            BsonClassMap.RegisterClassMap<T>(x =>
            {
                x.AutoMap();
                x.MapIdProperty(p => p.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                x.SetIgnoreExtraElements(true);
            });
        }
    }
}