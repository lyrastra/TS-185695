using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;
using Moedelo.Providing.Client.Abstractions.ProvidingState;
using Moedelo.Providing.Client.Abstractions.ProvidingState.Models;

namespace Moedelo.Providing.Client.Implementations.ProvidingState
{
    [InfrastructureV2.Domain.Attributes.Injection.InjectAsSingleton(typeof(IProvidingStateApiClient))]
    public class ProvidingStateApiClient : BaseCoreApiClient, IProvidingStateApiClient
    {
        private readonly SettingValue endpoint;

        public ProvidingStateApiClient(
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
            endpoint = settingRepository.Get("ProvidingApiEndpoint");
        }

        public async Task<int> GetActiveCountAsync(int firmId, int userId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await GetAsync<DataResponse<int>>("/api/v1/Providing/State/Active/Count", queryHeaders: tokenHeaders)
                .ConfigureAwait(false);
            return response.Data;
        }

        public async Task<int> SetAsync(int firmId, int userId, SetStateRequestDto request)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            var response = await PostAsync<SetStateRequestDto, DataResponse<int>>(
                "/api/v1/Providing/State", request, queryHeaders: tokenHeaders);
            return response.Data;
        }

        public async Task UnsetAsync(int firmId, int userId, long stateId)
        {
            var tokenHeaders = await GetPrivateTokenHeaders(firmId, userId).ConfigureAwait(false);
            await DeleteAsync($"/api/v1/Providing/State/{stateId}", queryHeaders: tokenHeaders).ConfigureAwait(false);
        }

        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }
    }
}