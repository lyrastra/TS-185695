using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.FileStorageV2.Client
{
    [InjectAsSingleton]
    public class ContractFilesClient : BaseApiClient, IContractFilesClient
    {
        private readonly SettingValue apiEndpoint;
        
        public ContractFilesClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FileStorageApiEndpoint");
        }

        public Task DeleteDocumentFilesAsync(int firmId, int userId, long baseId, bool ignoreError)
        {
            return PostAsync($"/ContractApi/DeleteDocumentFiles?{baseId}?firmId={firmId}&userId={userId}&ignoreError={ignoreError}");
        }

        public Task DeleteDocumentFilesAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, bool ignoreError)
        {
            return PostAsync($"/ContractApi/DeleteDocumentsFiles?firmId={firmId}&userId={userId}&ignoreError={ignoreError}", baseIds);
        }

        public Task UpdateJustCreatedDocumentScansPathsAsync(int firmId, int userId, long temporaryBaseId, long baseId)
        {
            return PostAsync($"/ContractApi/UpdateJustCreatedDocumentFilePaths?firmId={firmId}&userId={userId}&temporaryBaseId={temporaryBaseId}&baseId={baseId}");
        }

        public Task<Dto.FileDto> DownloadZipAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<Dto.FileDto>($"/ContractApi/DownloadAsZip?firmId={firmId}&userId={userId}&baseId={baseId}");
        }

        public Task<Dto.FileDto> DownloadPdfAsync(int firmId, int userId, long baseId, string fileName)
        {
            return GetAsync<Dto.FileDto>($"/ContractApi/DownloadAsPdf?firmId={firmId}&userId={userId}&baseId={baseId}&fileName={fileName}");
        }

        public Task<Dto.FileDto> GetFileAsync(int firmId, int userId, long baseId, string fileName,
            HttpQuerySetting querySetting = null)
        {
            return GetAsync<Dto.FileDto>(
                $"/ContractApi/GetFile?firmId={firmId}&userId={userId}&baseId={baseId}&fileName={fileName}",
                setting: querySetting);
        }

        public Task<IEnumerable<string>> GetFilesNamesAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<IEnumerable<string>>($"/ContractApi/GetFilesNames?firmId={firmId}&userId={userId}&baseId={baseId}");
        }

        public Task UploadFileAsync(int firmId, int userId, long baseId, Dto.FileDto file,
            HttpQuerySetting querySetting = null)
        {
            return PostAsync($"/ContractApi/UploadFile?firmId={firmId}&userId={userId}&baseId={baseId}", file,
                setting: querySetting);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
    }
}