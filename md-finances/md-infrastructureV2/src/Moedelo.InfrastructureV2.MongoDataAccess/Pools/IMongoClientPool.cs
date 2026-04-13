using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using MongoDB.Driver;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    public interface IMongoClientPool : IDI
    {
        IMongoClient GetMongoClient(string connectionString);
    }
}