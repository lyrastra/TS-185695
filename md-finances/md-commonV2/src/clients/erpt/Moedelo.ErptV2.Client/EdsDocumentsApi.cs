using System.Threading.Tasks;
using Moedelo.ErptV2.Client.EdsDocuments;
using Moedelo.ErptV2.Client.EdsDocuments.Sberbank;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.DependecyInjection;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.ErptV2.Client
{
    public interface IEdsDocumentsApi : IDI
    {
        Task<HttpFileModel> Get(EdsRequest model);
        Task<HttpFileModel> Get(AgreementForProcessPersonalData model);
        Task<HttpFileModel> GetSignatureRequestAsync(SignatureDocumentRequest signatureDocumentRequest);
    }

    [InjectAsSingleton]
    public class EdsDocumentsApi : BaseApiClient, IEdsDocumentsApi
    {
        private readonly SettingValue apiEndpoint;

        public EdsDocumentsApi(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager,
            ISettingRepository settingRepository) : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("ErptApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value;
        }

        public Task<HttpFileModel> Get(EdsRequest model)
            => DownloadFileByPostMethodAsHttpFileModelAsync("/eds/documents/GetSberbankEdsRequest", model);

        public Task<HttpFileModel> Get(AgreementForProcessPersonalData model)
            => DownloadFileByPostMethodAsHttpFileModelAsync("/eds/documents/GetSberbankAgreementForProcessPersonalData", model);

        public Task<HttpFileModel> GetSignatureRequestAsync(SignatureDocumentRequest signatureDocumentRequest)
            => DownloadFileByPostMethodAsHttpFileModelAsync($"/eds/documents/GetSignatureRequest", signatureDocumentRequest);
    }
}
