using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Internal
{
    [InjectAsSingleton]
    public sealed class InternalMongoDbExecutor : IInternalMongoDbExecutor
    {
        private readonly IMongoCollectionPool mongoCollectionPool;

        public InternalMongoDbExecutor(IMongoCollectionPool mongoCollectionPool)
        {
            this.mongoCollectionPool = mongoCollectionPool;
        }

        public async Task<T> FindByIdAsync<T>(MongoCollectionConnection connection, string id) where T : class, IMongoObject
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return null;
            }

            var collection = mongoCollectionPool.GetMongoCollection<T>(connection);
            var cursor = await collection
                .FindAsync(FilterById<T>(id))
                .ConfigureAwait(false);
            var result = cursor.FirstOrDefault() as T;

            return result;
        }

        public Task InsertAsync<T>(MongoCollectionConnection connection, T document) where T : class, IMongoObject
        {
            if (document == null)
            {
                throw new ArgumentException(nameof(document));
            }

            if (string.IsNullOrWhiteSpace(document.Id))
            {
                document.Id = ObjectId.GenerateNewId().ToString();
            }

            var collection = mongoCollectionPool.GetMongoCollection<T>(connection);

            return collection.InsertOneAsync(document);
        }

        public Task UpdateAsync<T>(MongoCollectionConnection connection, T document) where T : class, IMongoObject
        {
            if (document == null || string.IsNullOrWhiteSpace(document.Id))
            {
                throw new ArgumentException(nameof(document));
            }

            var collection = mongoCollectionPool.GetMongoCollection<T>(connection);

            return collection.ReplaceOneAsync(FilterById<T>(document.Id),
                document);
        }

        public Task DeleteByIdAsync<T>(MongoCollectionConnection connection, string id) where T : class, IMongoObject
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return Task.CompletedTask;
            }

            var collection = mongoCollectionPool.GetMongoCollection<T>(connection);

            return collection.DeleteOneAsync(FilterById<T>(id));
        }

        private static ExpressionFilterDefinition<T> FilterById<T>(string id) where T : class, IMongoObject
        {
            return new ExpressionFilterDefinition<T>(x => x.Id == id);
        }
    }
}