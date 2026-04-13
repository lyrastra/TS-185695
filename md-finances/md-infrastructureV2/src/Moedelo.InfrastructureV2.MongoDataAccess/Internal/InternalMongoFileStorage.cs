using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.Models.MongoFiles;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools;
using Moedelo.InfrastructureV2.MongoDataAccess.Pools.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using FileNotFoundException = Moedelo.InfrastructureV2.Domain.Exceptions.DistributedFileSystem.FileNotFoundException;

namespace Moedelo.InfrastructureV2.MongoDataAccess.Internal
{
    [InjectAsSingleton(typeof(IInternalMongoFileStorage))]
    public sealed class InternalMongoFileStorage : IInternalMongoFileStorage
    {
        private const string IdFieldName = "_id";

        private readonly IGridFSBucketPool gridFsBucketPool;

        public InternalMongoFileStorage(IGridFSBucketPool gridFsBucketPool)
        {
            this.gridFsBucketPool = gridFsBucketPool;
        }

        public async Task<byte[]> GetFileDataByIdAsync(MongoConnection connection, string id,
            CancellationToken cancellationToken)
        {
            try
            {
                var bucket = gridFsBucketPool.GetGridFSBucket(connection);
                var result = await bucket
                    .DownloadAsBytesAsync(new ObjectId(id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                return result;
            }
            catch (GridFSFileNotFoundException ex)
            {
                throw new FileNotFoundException(ex);
            }
        }

        public async Task<Stream> GetFileDataStreamByIdAsync(
            MongoConnection connection,
            string id,
            CancellationToken cancellationToken)
        {
            try
            {
                var bucket = gridFsBucketPool.GetGridFSBucket(connection);

                return await bucket
                    .OpenDownloadStreamAsync(new ObjectId(id), cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (GridFSFileNotFoundException ex)
            {
                throw new FileNotFoundException(ex);
            }
        }

        public async Task<byte[]> GetFileDataByFileNameAsync(MongoConnection connection, string fileName,
            CancellationToken cancellationToken)
        {
            try
            {
                var bucket = gridFsBucketPool.GetGridFSBucket(connection);
                return await bucket
                    .DownloadAsBytesByNameAsync(fileName, cancellationToken: cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (GridFSFileNotFoundException ex)
            {
                throw new FileNotFoundException(ex);
            }
        }

        public async Task<bool> HasDataByIdAsync(MongoConnection connection, string id,
            CancellationToken cancellationToken)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(IdFieldName, new ObjectId(id));
            var bucket = gridFsBucketPool.GetGridFSBucket(connection);

            using (var cursor = await bucket.FindAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var fileInfo = await cursor.FirstOrDefaultAsync(cancellationToken).ConfigureAwait(false);

                return fileInfo != null;
            }
        }

        public async Task<string> SaveFileAsync(MongoConnection connection, string fileName, byte[] fileData,
            CancellationToken cancellationToken)
        {
            var bucket = gridFsBucketPool.GetGridFSBucket(connection);
            var id = await bucket
                .UploadFromBytesAsync(fileName, fileData, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return id.ToString();
        }

        public Task DeleteFileByIdAsync(MongoConnection connection, string id,
            CancellationToken cancellationToken)
        {
            var bucket = gridFsBucketPool.GetGridFSBucket(connection);

            return bucket.DeleteAsync(new ObjectId(id), cancellationToken: cancellationToken);
        }

        public async Task<MongoFileInfo> GetFileNameByIdAsync(MongoConnection connection, string id,
            CancellationToken cancellationToken)
        {
            var filter = Builders<GridFSFileInfo>.Filter.Eq(IdFieldName, new ObjectId(id));
            var bucket = gridFsBucketPool.GetGridFSBucket(connection);

            using (var cursor = await bucket.FindAsync(filter, cancellationToken: cancellationToken).ConfigureAwait(false))
            {
                var fileInfo = await cursor
                    .FirstOrDefaultAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (fileInfo == null)
                {
                    return null;
                }

                return MapToDomain(fileInfo);
            }
        }

        private static MongoFileInfo MapToDomain(GridFSFileInfo fileInfo)
        {
            return new MongoFileInfo
            {
                ObjectId = fileInfo.Id.ToString(),
                Name = fileInfo.Filename,
                DateUpload = fileInfo.UploadDateTime,
                ContentSize = fileInfo.Length
            };
        }

        public async Task<MongoFileInfoTable> GetFileNamesByPathPatternAsync(MongoConnection connection,
            string pathPattern, int? skip, int? take, CancellationToken cancellationToken)
        {
            if (take <= 0)
            {
                return new MongoFileInfoTable();
            }

            var filter = Builders<GridFSFileInfo>.Filter.Regex(x => x.Filename, pathPattern);
            var sort = Builders<GridFSFileInfo>.Sort.Descending(x => x.UploadDateTime);
            var options = new GridFSFindOptions
            {
                Limit = take,
                Skip = skip,
                Sort = sort
            };
            var bucket = gridFsBucketPool.GetGridFSBucket(connection);

            using (var cursor = await bucket.FindAsync(filter, options, cancellationToken).ConfigureAwait(false))
            {
                var fileInfos = await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);

                return new MongoFileInfoTable
                {
                    TotalCount = fileInfos.Count,
                    FileInfos = fileInfos.Select(MapToDomain).ToList()
                };
            }
        }

        public async Task<MongoFileInfoTable> GetFileNamesByPathAsync(MongoConnection connection,
            string path, int? skip, int? take,
            CancellationToken cancellationToken)
        {
            if (take <= 0)
            {
                return new MongoFileInfoTable();
            }

            var filter = Builders<GridFSFileInfo>.Filter.Eq(x => x.Filename, path);
            var sort = Builders<GridFSFileInfo>.Sort.Descending(x => x.UploadDateTime);
            var options = new GridFSFindOptions
            {
                Limit = take,
                Skip = skip,
                Sort = sort
            };
            var bucket = gridFsBucketPool.GetGridFSBucket(connection);

            using (var cursor = await bucket.FindAsync(filter, options, cancellationToken).ConfigureAwait(false))
            {
                var fileInfos = await cursor.ToListAsync(cancellationToken).ConfigureAwait(false);

                return new MongoFileInfoTable
                {
                    TotalCount = fileInfos.Count,
                    FileInfos = fileInfos.Select(MapToDomain).ToList()
                };
            }
        }
    }
}