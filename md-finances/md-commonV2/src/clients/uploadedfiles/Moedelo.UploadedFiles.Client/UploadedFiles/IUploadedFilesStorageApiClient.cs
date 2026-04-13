#nullable enable
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.FileStorage;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;

namespace Moedelo.UploadedFiles.Client.UploadedFiles
{
    public interface IUploadedFilesStorageApiClient
    {
        Task<HttpFileStream> GetFileAsync(int fileStorageId, bool includeData = true,
            HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default);
        Task<HttpFileStream?> GetFileDataNoErrorAsync(int fileStorageId,
            HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="firmId">идентификатор фирмы, в контексте которой сохраняется файл. Может быть равен 0</param>
        /// <param name="model">файл</param>
        /// <param name="type">тип файла</param>
        /// <param name="createUserId">идентификатор пользователя, создающего файл. Может быть равен null</param>
        /// <param name="setting">параметры запроса</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>идентификатор созданного файла</returns>
        Task<int> SaveFileAsync(
            int firmId,
            HttpFileModel model,
            FileStorageFileType type,
            int? createUserId = null,
            HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="firmId">идентификатор фирмы, в контексте которой сохраняется файл. Может быть равен 0</param>
        /// <param name="file">файл</param>
        /// <param name="type">тип файла</param>
        /// <param name="createUserId">идентификатор пользователя, создающего файл. Может быть равен null</param>
        /// <param name="setting">параметры запроса</param>
        /// <param name="cancellationToken">токен отмены операции</param>
        /// <returns>идентификатор созданного файла</returns>
        Task<int> SaveFileAsync(
            int firmId,
            HttpFileStream file,
            FileStorageFileType type,
            int? createUserId = null,
            HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default);
        Task UpdateFileAsync(int fileStorageId, HttpFileModel model, HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default);
        Task UpdateFileAsync(int fileStorageId, HttpFileStream model, HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default);
        Task DeleteFileAsync(int fileStorageId, HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default);

        Task<List<FileStorageFileAttributeType>> GetFileAttributesAsync(int fileStorageId,
            CancellationToken cancellationToken = default);
        Task<string> GetFileAttributeAsync(int fileStorageId, FileStorageFileAttributeType attributeId,
            CancellationToken cancellationToken = default);
        Task SaveFileAttributeAsync(int fileStorageId, FileStorageFileAttributeType attributeId, string data,
            CancellationToken cancellationToken = default);
    }
}