using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BPM.Distribution.Client.Document
{
    [InjectAsSingleton]
    public class DocumentClient : BaseApiClient, IDocumentClient
    {
        private readonly SettingValue apiEndpoint;

        public DocumentClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("BPMApiUrl");
        }

        public Task<HttpFileModel> GetDocument(string documentId)
        {
            return GetFileAsync($"/Rest/Document/{documentId}", null);
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/distribution";
        }
    }
}