using System.Threading.Tasks;
using Moedelo.Edm.Client.Contracts;
using Moedelo.Edm.Dto.Configuration;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.Edm.Client.Implementations
{
    [InjectAsSingleton]
    public class EdmConfigurationApiClient : BaseApiClient, IEdmConfigurationApiClient
    {
        private readonly SettingValue apiEndpoint;

        public EdmConfigurationApiClient(IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("EdmPrivateApiEndpoint");
        }

        public Task<EdmDocumentFileDto> GetAsync(int firmId, int entityType)
        {
            return GetAsync <EdmDocumentFileDto> ($"/Get?firmId={firmId}&entityType={entityType}");
        }

        public Task<EdmDocumentPreviewFileDto> GetPreviewAsync(int firmId, int entityType)
        {
            return GetAsync<EdmDocumentPreviewFileDto>($"/GetPreview?firmId={firmId}&entityType={entityType}");
        }

        public Task<bool> IsUploadedAsync(int firmId, int entityType)
        {
            return GetAsync<bool>($"/IsUploaded?firmId={firmId}&entityType={entityType}");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/Rest/EdmUploadedDocument";
        }
    }
}
