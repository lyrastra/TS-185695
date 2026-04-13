using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Http.Abstractions.Models;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos;

namespace Moedelo.Money.ApiClient.legacy.Finances
{
    [InjectAsSingleton(typeof(IMoneyBalancesApiClient))]
    internal sealed class MoneyBalancesApiClient : BaseApiClient, IMoneyBalancesApiClient
    {
        public MoneyBalancesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MoneyBalancesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("FinancesPrivateApiEndpoint"),
                logger)
        {
        }
        
        public Task<MoneySourceBalanceDto[]> GetAsync(FirmId firmId, UserId userId, BalanceRequestDto request, HttpQuerySetting setting = default)
        {
            return PostAsync<BalanceRequestDto, MoneySourceBalanceDto[]>(
                $"/Money/Balances?firmId={firmId}&userId={userId}",
                request,
                setting: setting);
        }
    }
}