using System;
using System.Collections.Concurrent;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using MongoDB.Driver;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    [InjectAsSingleton]
    public sealed class MongoClientPool : IMongoClientPool
    {
        private readonly ConcurrentDictionary<string, IMongoClient> pool =
            new ConcurrentDictionary<string, IMongoClient>();

        public IMongoClient GetMongoClient(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentException(nameof(connectionString));
            }

            var mongoClient = pool.GetOrAdd(connectionString, MongoClientFactory);

            return mongoClient;
        }

        private static IMongoClient MongoClientFactory(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);

            return mongoClient;
        }
    }
}