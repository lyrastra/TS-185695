using System.Threading;
using System.Threading.Tasks;
using Moedelo.ErptV2.Dto.ErptUploadedFiles;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client.UploadFile
{
    [InjectAsSingleton(typeof(IErptUploadedDocumentsApiClient))]
    internal sealed class ErptUploadedDocumentsApiClient : BaseApiClient, IErptUploadedDocumentsApiClient
    {
        private readonly SettingValue apiEndpoint;

        public ErptUploadedDocumentsApiClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer, IAuditScopeManager auditScopeManager)
                : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<ErptUploadedDocumentMetadataDto[]> GetLinkedUploadedDocumentsMetadataListAsync(int reportId, ErptUploadedFileType fileType,
            CancellationToken cancellationToken)
        {
            const string url = "/UploadedFiles/metadata/find";
            var queryParams = new { reportId, fileType = (int)fileType };

            return GetAsync<ErptUploadedDocumentMetadataDto[]>(url, queryParams, cancellationToken: cancellationToken);
        }

        public Task SetNeformalDocumentVisibleCrossValueAsync(int uploadedFileId, bool isVisible, CancellationToken cancellationToken)
        {
            var url = $"/UploadedFiles/{uploadedFileId}/NeformalDocumentVisibleCross?isVisible={isVisible}";

            return PutAsync(url, new {}, cancellationToken: cancellationToken);
        }

        public Task SetFormalDocumentVisibleCrossValueAsync(int uploadedFileId, bool isVisible, CancellationToken cancellationToken)
        {
            var url = $"/UploadedFiles/{uploadedFileId}/FormalDocumentVisibleCross?isVisible={isVisible}";

            return PutAsync(url, new {}, cancellationToken: cancellationToken);
        }
    }
}
