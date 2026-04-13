using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Providing.ApiClient.Abstractions.ProvidingState;
using Moedelo.Providing.ApiClient.Abstractions.ProvidingState.Models;
using System.Threading.Tasks;

namespace Moedelo.Providing.ApiClient
{
    [InjectAsSingleton(typeof(IProvidingStateApiClient))]
    internal class ProvidingStateApiClient : BaseApiClient, IProvidingStateApiClient
    {
        public ProvidingStateApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IProvidingStateApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("ProvidingApiEndpoint"),
                logger)
        {
        }

        public async Task<int> GetActiveCountAsync()
        {
            var response = await GetAsync<DataResponse<int>>(
                "/api/v1/Providing/State/Active/Count");
            return response.Data;
        }

        public async Task<long> SetAsync(SetStateRequestDto request, HttpQuerySetting setting = null)
        {
            var response = await PostAsync<SetStateRequestDto, DataResponse<long>>(
                "/api/v1/Providing/State", request, setting: setting);
            return response.Data;
        }

        public Task UnsetAsync(long stateId)
        {
            return DeleteAsync($"/api/v1/Providing/State/{stateId}");
        }

        public Task UnsetByBaseIdAsync(long baseId)
        {
            return PostAsync($"/api/v1/Providing/State/DeleteByBaseId/{baseId}");
        }
    }
}
