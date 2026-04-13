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
    [InjectAsSingleton(typeof(IMoneyOperationsApiClient))]
    internal sealed class MoneyOperationsApiClient : BaseLegacyApiClient, IMoneyOperationsApiClient
    {
        public MoneyOperationsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MoneyOperationsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FinancesPrivateApiEndpoint"),
                logger)
        {
        }

        public Task<IEnumerable<BudgetaryPaymentForReportDto>> GetBudgetaryPaymentsV2Async(FirmId firmId, UserId userId, 
            BudgetaryAccPaymentsRequestDto request)
        {
            return PostAsync<BudgetaryAccPaymentsRequestDto, IEnumerable<BudgetaryPaymentForReportDto>>(
                $"/Money/Operations/PaymentsForReport/V2/GetBudgetaryPayments?firmId={firmId}&userId={userId}",
                request);
        }
    }
}