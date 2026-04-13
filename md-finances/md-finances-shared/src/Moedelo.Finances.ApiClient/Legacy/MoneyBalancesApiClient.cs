using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy;
using Moedelo.Finances.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Finances.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IMoneyBalancesApiClient))]
    internal sealed class MoneyBalancesApiClient : BaseLegacyApiClient, IMoneyBalancesApiClient
    {
        public MoneyBalancesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MoneyBalancesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FinancesPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<IReadOnlyList<MoneySourceBalanceDto>> GetBalancesAsync(FirmId firmId, UserId userId,
            BalanceRequestDto request)
        {
            return PostAsync<BalanceRequestDto, IReadOnlyList<MoneySourceBalanceDto>>(
                $"/Money/Balances?firmId={firmId}&userId={userId}", request);
        }

        public Task ReconcileWithServiceAsync(FirmId firmId, UserId userId, ReconcileRequestDto request)
        {
            return PostAsync($"/Money/Balances/ReconcileWithService?firmId={firmId}&userId={userId}", request);
        }
    }
}