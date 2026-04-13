using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Driver;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    public interface IMongoCollectionPool : IDI
    {
        IMongoCollection<T> GetMongoCollection<T>(MongoCollectionConnection connection) where T : class, IMongoObject;
    }
}