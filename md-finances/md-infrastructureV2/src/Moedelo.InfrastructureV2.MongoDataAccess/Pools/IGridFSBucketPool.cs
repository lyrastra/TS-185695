using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Driver.GridFS;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Pools
{
    public interface IGridFSBucketPool : IDI
    {
        IGridFSBucket GetGridFSBucket(MongoConnection connection);
    }
}