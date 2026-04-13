using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Mongo;

public interface IMongoDbMapper : IDI
{
    void Map<T>() where T : IMongoObject;
}