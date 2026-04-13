using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances;
using Moedelo.Money.ApiClient.Abstractions.legacy.Finances.Dtos.Reconciliation;

namespace Moedelo.Money.ApiClient.legacy.Finances
{
    [InjectAsSingleton(typeof(IMoneyReconciliationClient))]
    public class MoneyReconciliationClient : BaseApiClient, IMoneyReconciliationClient
    {
        public MoneyReconciliationClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MoneyReconciliationClient> logger)
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

        public Task<ReconciliationResponseDto[]> GetStatusesAsync(FirmId firmId, UserId userId, ReconciliationStatusRequestDto requestDto)
        {
            return PostAsync<ReconciliationStatusRequestDto, ReconciliationResponseDto[]>($"/Money/Reconciliation/GetStatuses?firmId={firmId}&userId={userId}", requestDto);
        }

        public Task<ReconciliationResponseDto[]> GetLastWithDiffAsync(FirmId firmId, UserId userId, LastReconciliationWithDiffRequestDto requestDto)
        {
            if (requestDto?.SettlementAccountIds?.Any() == false)
            {
                return Task.FromResult(Array.Empty<ReconciliationResponseDto>());
            }

            return PostAsync<LastReconciliationWithDiffRequestDto, ReconciliationResponseDto[]>($"/Money/Reconciliation/GetLastWithDiff?firmId={firmId}&userId={userId}", requestDto);
        }
    }
}
