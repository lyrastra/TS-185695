using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Internal
{
    public interface IInternalMongoDbExecutor : IDI
    {
        Task<T> FindByIdAsync<T>(MongoCollectionConnection connection, string id)
            where T : class, IMongoObject;

        Task InsertAsync<T>(MongoCollectionConnection connection, T document)
            where T : class, IMongoObject;

        Task UpdateAsync<T>(MongoCollectionConnection connection, T document)
            where T : class, IMongoObject;

        Task DeleteByIdAsync<T>(MongoCollectionConnection connection, string id)
            where T : class, IMongoObject;
    }
}