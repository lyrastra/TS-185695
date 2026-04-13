#nullable enable
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.FileStorage;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.UploadedFiles.Client.Extensions;
using Moedelo.UploadedFiles.Dto;

namespace Moedelo.UploadedFiles.Client.UploadedFiles
{
    [InjectAsSingleton(typeof(IUploadedFilesStorageApiClient))]
    internal sealed class UploadedFilesStorageApiClient : BaseApiClient, IUploadedFilesStorageApiClient
    {
        private class ResultId
        {
            public int Id { get; set; }
        }

        private readonly SettingValue apiEndPoint;
        
        public UploadedFilesStorageApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("FileStoragePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public async Task<HttpFileStream> GetFileAsync(
            int fileStorageId,
            bool includeData = true,
            HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default)
        {
            if (!includeData)
            {
                var url = $"/v1/files/{fileStorageId}";

                var response = await GetAsync<UploadedFileShortInfoDto>(
                    url, setting: setting, cancellationToken: cancellationToken).ConfigureAwait(false);

                return new HttpFileStream(response.FileName, response.ContentType, Stream.Null);
            }
            else
            {
                var url = $"/v1/files/{fileStorageId}/data";
                var queryParams = new { };
                using var response = await DownloadFileByGetMethodAsync(
                    url,
                    queryParams,
                    setting: setting,
                    cancellationToken: cancellationToken).ConfigureAwait(false);

                return new HttpFileStream(
                    response.FileName,
                    response.ContentType,
                    response.ReleaseStream());
            }
        }

        public async Task<HttpFileStream?> GetFileDataNoErrorAsync(int fileStorageId,
            HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default)
        {
            var url = $"/v1/files/{fileStorageId}/data";
            var queryParams = new { ignoreNotFoundError = true };
            setting ??= new HttpQuerySetting();
            setting.DontThrowOn404 = true;

            using var response = await DownloadFileByGetMethodAsync(
                url,
                queryParams,
                setting: setting,
                cancellationToken: cancellationToken).ConfigureAwait(false);

            if (response == null || response.Stream.Length == 0)
            {
                return null;
            }

            return new HttpFileStream(
                response.FileName,
                response.ContentType,
                response.ReleaseStream());
        }

        public async Task<int> SaveFileAsync(
            int firmId,
            HttpFileModel model,
            FileStorageFileType type,
            int? createUserId = null,
            HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default)
        {
            model.EnsureFileStreamAtStartPosition();

            var url = $"/v1/files?fileType={(int)type}&firmId={firmId}&createUserId={createUserId}";

            var id = await SendFileAsync<ResultId>(url, model, setting: setting, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return id.Id;
        }

        public async Task<int> SaveFileAsync(int firmId, HttpFileStream file, FileStorageFileType type, int? createUserId = null,
            HttpQuerySetting? setting = null, CancellationToken cancellationToken = default)
        {
            file.EnsureFileStreamAtStartPosition();

            var url = $"/v1/files?fileType={(int)type}&firmId={firmId}&createUserId={createUserId}";

            var id = await SendFileAsync<ResultId>(url, file, setting: setting, cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return id.Id;
        }

        public Task DeleteFileAsync(int fileStorageId, HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default)
        {
            var url = $"/v1/files/{fileStorageId}";

            return DeleteAsync(url, setting: setting, cancellationToken: cancellationToken);
        }

        public Task UpdateFileAsync(int fileStorageId, HttpFileModel model, HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default)
        {
            model.EnsureFileStreamAtStartPosition();
            var url = $"/v1/files/{fileStorageId}/data";

            return PutFileAsync(url, model, null, setting, cancellationToken);
        }
        
        public Task UpdateFileAsync(int fileStorageId, HttpFileStream model, HttpQuerySetting? setting = null,
            CancellationToken cancellationToken = default)
        {
            model.EnsureFileStreamAtStartPosition();
            var url = $"/v1/files/{fileStorageId}/data";

            return PutFileAsync(url, model, null, setting, cancellationToken);
        }


        Task<List<FileStorageFileAttributeType>> IUploadedFilesStorageApiClient.GetFileAttributesAsync(int fileStorageId,
            CancellationToken cancellationToken)
        {
            var url = $"/v1/files/{fileStorageId}/attributes";

            return GetAsync<List<FileStorageFileAttributeType>>(url, null, cancellationToken: cancellationToken);
        }

        Task<string> IUploadedFilesStorageApiClient.GetFileAttributeAsync(int fileStorageId, FileStorageFileAttributeType attributeId,
            CancellationToken cancellationToken)
        {
            var url = $"/v1/files/{fileStorageId}/attributes/{(int) attributeId}";

            return GetAsync<string>(url, null, cancellationToken: cancellationToken);
        }

        public Task SaveFileAttributeAsync(int fileStorageId, FileStorageFileAttributeType attributeId, string data,
            CancellationToken cancellationToken = default)
        {
            var url = $"/v1/files/{fileStorageId}/attributes/{(int) attributeId}";
            var request = new UploadedFileAttributeValueDto
            {
                Value = data
            };

            return PostAsync(url, request, cancellationToken: cancellationToken);
        }
    }
}
