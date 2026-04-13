using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.DistributedFileSystem;
using Moedelo.InfrastructureV2.Domain.Interfaces.Logging;
using Moedelo.InfrastructureV2.Domain.Models.Mongo;

namespace Moedelo.InfrastructureV2.Domain.Interfaces.Mongo;

public interface IMongoFileStorageFactory : IDI
{
    IFileStorageBase GetExecuter(ILogger logger, MongoFileStorageSettings settings);
}