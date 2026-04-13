using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.FileStorageV2.Client
{
    [InjectAsSingleton]
    public class DocumentScansClient : BaseApiClient, IDocumentScansClient
    {
        private readonly SettingValue apiEndpoint;
        
        public DocumentScansClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FileStorageApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }
        
        public Task DeleteDocumentScansAsync(int firmId, int userId, long baseId, bool ignoreError)
        {
            return DeleteAsync($"/ScanApi/{baseId}?firmId={firmId}&userId={userId}&ignoreError={ignoreError}");
        }

        public Task DeleteDocumentsScansAsync(int firmId, int userId, IReadOnlyCollection<long> baseIds, bool ignoreError)
        {
            return DeleteByRequestAsync($"/ScanApi/?firmId={firmId}&userId={userId}&ignoreError={ignoreError}", baseIds);
        }

        public Task<List<string>> GetDocumentScansAsync(int firmId, int userId, long baseId)
        {
            return GetAsync<List<string>>($"/ScanApi/DocumentScans/{baseId}", new {firmId, userId});
        }

        public Task UpdateJustCreatedDocumentScansPathsAsync(int firmId, int userId, long temporaryBaseId, long baseId)
        {
            return PostAsync(
                $"/ScanApi/UpdateJustCreatedDocumentScansPaths?firmId={firmId}&userId={userId}&temporaryBaseId={temporaryBaseId}&baseId={baseId}");
        }
    }
}