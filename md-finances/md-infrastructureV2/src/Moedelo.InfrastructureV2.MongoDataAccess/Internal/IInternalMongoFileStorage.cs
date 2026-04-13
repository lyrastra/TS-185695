using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Models.MongoFiles;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Internal
{
    public interface IInternalMongoFileStorage
    {
        Task<byte[]> GetFileDataByIdAsync(MongoConnection connection, string id, CancellationToken cancellationToken);

        Task<Stream> GetFileDataStreamByIdAsync(MongoConnection connection, string id, CancellationToken cancellationToken);

        Task<byte[]> GetFileDataByFileNameAsync(MongoConnection connection, string fileName,
            CancellationToken cancellationToken);

        Task<MongoFileInfo> GetFileNameByIdAsync(MongoConnection connection, string id,
            CancellationToken cancellationToken);

        Task<MongoFileInfoTable> GetFileNamesByPathPatternAsync(MongoConnection connection, string pathPattern,
            int? skip = null, int? take = null, CancellationToken cancellationToken = default);

        Task<MongoFileInfoTable> GetFileNamesByPathAsync(MongoConnection connection, string path, int? skip = null, int? take = null, CancellationToken cancellationToken = default);

        Task<bool> HasDataByIdAsync(MongoConnection connection, string id, CancellationToken cancellationToken);

        Task<string> SaveFileAsync(MongoConnection connection, string fileName, byte[] fileData,
            CancellationToken cancellationToken);

        Task DeleteFileByIdAsync(MongoConnection connection, string id, CancellationToken cancellationToken);
    }
}