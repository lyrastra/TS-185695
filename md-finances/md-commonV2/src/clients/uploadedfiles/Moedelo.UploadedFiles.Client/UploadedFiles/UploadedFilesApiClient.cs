using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Moedelo.Common.Enums.Enums.Uploaded;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.UploadedFiles.Dto;

namespace Moedelo.UploadedFiles.Client.UploadedFiles
{
    [InjectAsSingleton(typeof(IUploadedFilesApiClient))]
    internal sealed class UploadedFilesApiClient : BaseApiClient, IUploadedFilesApiClient
    {
        private readonly SettingValue apiEndPoint;
        
        public UploadedFilesApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager
                )
        {
            apiEndPoint = settingRepository.Get("FileStoragePrivateApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }

        public Task<List<UploadedFileDto>> GetByEntityTypeAndIdAsync(int firmId, EntityType entityType, long entityId,
            CancellationToken cancellationToken = default)
        {
            const string url = "/v1/uploadedFiles/GetByEntityTypeAndId";
            var queryParams = new { firmId, entityType, entityId };

            return GetAsync<List<UploadedFileDto>>(url, queryParams, cancellationToken: cancellationToken);
        }

        public Task<List<UploadedFileDto>> GetByEntityTypeAndIdListAsync(int firmId,
            GetByEntityTypeAndIdListRequestDto requestDto,
            CancellationToken cancellationToken = default)
        {
            var url = $"/v1/uploadedFiles/GetByEntityTypeAndIdList?firmId={firmId}";

            return PostAsync<GetByEntityTypeAndIdListRequestDto, List<UploadedFileDto>>(url,
                requestDto, cancellationToken: cancellationToken);
        }

        public Task DeleteAsync(int firmId, int id, CancellationToken cancellationToken = default)
        {
            var url = $"/v1/uploadedFiles/{id}?firmId={firmId}";

            return DeleteAsync(url, cancellationToken: cancellationToken);
        }

        public async Task<int> CreateAsync(CreateUploadedFileDto fileInfo, HttpFileModel file)
        {
            const string url = "/v1/uploadedFiles"; 
            
            var response = await SendFileAsync<CreateUploadedFileDto, CreateUploadedFileResultDto>(
                url, fileInfo, file).ConfigureAwait(false);

            return response.Id;
        }

        public Task MoveToFirm(int fromFirmId, int toFirmId, HttpQuerySetting setting = null, CancellationToken cancellationToken = default)
        {
            var url = $"/v1/uploadedFiles/MoveToFirm?fromFirmId={fromFirmId}&toFirmId={toFirmId}";

            return PostAsync(url, setting: setting, cancellationToken: cancellationToken);
        }
    }
}