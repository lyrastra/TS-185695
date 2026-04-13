using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.LinkedDoocuments.Client.Abstractions.BulkOperations;

namespace Moedelo.LinkedDoocuments.Client.Implementations.BulkOperations
{
    [InjectAsSingleton(typeof(IMoneyAndDocumentsAutoLinkingApiClient))]
    public class MoneyAndDocumentsAutoLinkingApiClient : BaseCoreApiClient, IMoneyAndDocumentsAutoLinkingApiClient
    {
        private readonly SettingValue endPoint;

        public MoneyAndDocumentsAutoLinkingApiClient(
            IHttpRequestExecutor httpRequestExecutor, 
            IUriCreator uriCreator, 
            IResponseParser responseParser, 
            ISettingRepository settingRepository, 
            IAuditTracer auditTracer, 
            IAuditScopeManager auditScopeManager) 
            : base(
                httpRequestExecutor, 
                uriCreator, 
                responseParser, 
                settingRepository, 
                auditTracer, 
                auditScopeManager)
        {
            endPoint = settingRepository.Get("LinkedDocumentsApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return endPoint.Value;
        }

        public async Task<bool> IsEnabledAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<DataResponse<bool>>("/api/v1.0/BulkOperations/MoneyAndDocuments/AutoLinking/IsEnabled", queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
            return response.Data;
        }

        public async Task RelinkAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await PostAsync(
                    "/api/v1/BulkOperations/MoneyAndDocuments/AutoLinking/Relink",
                    new { },
                    queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
        }
    }
}