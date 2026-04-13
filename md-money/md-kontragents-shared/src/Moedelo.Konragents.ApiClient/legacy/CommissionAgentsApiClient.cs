using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Konragents.ApiClient.Abstractions.legacy;
using Moedelo.Konragents.ApiClient.Abstractions.legacy.Dtos;

namespace Moedelo.Konragents.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ICommissionAgentsApiClient))]
    public class CommissionAgentsApiClient : BaseLegacyApiClient, ICommissionAgentsApiClient
    {
        public CommissionAgentsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<CommissionAgentsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("KontragentsPrivateApiEndpoint"),
                logger)
        {
        }

        public async Task<CreateCommissionByInnResultDto> CreateCommissionAgentAsync(FirmId firmId, UserId userId, string inn)
        {
            var result = await PostAsync<CreateCommissionByInnResultDto>(
                    $"/CommissionAgents/CreateByInn?firmId={firmId}&userId={userId}&inn={inn}")
                .ConfigureAwait(false);

            return result;
        }
    }
}