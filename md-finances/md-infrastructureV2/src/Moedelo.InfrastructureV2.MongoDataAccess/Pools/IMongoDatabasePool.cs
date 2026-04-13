using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Driver;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    public interface IMongoDatabasePool : IDI
    {
        IMongoDatabase GetMongoDatabase(MongoConnection connection);
    }
}