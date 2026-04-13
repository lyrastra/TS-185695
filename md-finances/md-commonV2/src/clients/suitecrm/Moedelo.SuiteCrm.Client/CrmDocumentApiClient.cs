using System;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.SuiteCrm.Client
{
    [InjectAsSingleton]
    public class CrmDocumentApiClient : BaseApiClient, ICrmDocumentApiClient
    {
        private readonly HttpQuerySetting setting = new HttpQuerySetting {Timeout = new TimeSpan(0, 0, 10, 0)};
        private readonly SettingValue apiEndPoint;

        public CrmDocumentApiClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndPoint = settingRepository.Get("SuiteCrmApiUrl");
        }

        public Task GetDocumentFileAsync(string documentId)
        {
            return GetAsync($"/Document/{documentId}");
        }

        public Task GetDocumentPreviewFileAsync(string documentId, int pageIndex)
        {
            return GetAsync($"/Document/{documentId}/preview/{pageIndex}");
        }

        public Task CreateDocumentPreviewAsync(string documentId)
        {
            return PostAsync($"/Document/{documentId}/preview/create");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndPoint.Value;
        }
    }
}