using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto.Balances;
using Moedelo.Accounting.Enums;
using Moedelo.Common.Types;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IBalancesApiClient))]
    internal sealed class BalancesApiClient : BaseLegacyApiClient, IBalancesApiClient
    {
        public BalancesApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<BalancesApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<DateTime?> GetDateAsync(FirmId firmId, UserId userId, bool useReadOnlyDb = false)
        {
            var uri = $"/Balances/Date?firmId={firmId}&userId={userId}&useReadOnlyDb={useReadOnlyDb}";
            return GetAsync<DateTime?>(uri);
        }

        public Task<IReadOnlyDictionary<int, DateTime>> GetDateByFirmIds(
            IReadOnlyCollection<int> firmIds)
        {
            var uri = $"/BalancesSummary/DateByFirmIds";
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, DateTime>>(uri, firmIds);
        }

        public Task<List<CurrencyRemainsDto>> GetCurrencyRemainsAsync(FirmId firmId, UserId userId, IReadOnlyList<CurrencySyntheticAccountCode> codes)
        {
            if (codes?.Any() == false)
            {
                return Task.FromResult(new List<CurrencyRemainsDto>());
            }

            var uri = $"/Balances/ByAccountCodes?firmId={firmId}&userId={userId}";
            return PostAsync<IReadOnlyList<CurrencySyntheticAccountCode>, List<CurrencyRemainsDto>>(uri, codes);
        }

        public Task<IReadOnlyDictionary<int, AccountBalanceDto[]>> GetBalancesByFirmIdsAndAccountCodesAsync(GetBalanceByFirmIdsRequestDto request)
        {
            return PostAsync<GetBalanceByFirmIdsRequestDto, IReadOnlyDictionary<int, AccountBalanceDto[]>>("/BalancesSummary/ByFirmIdsAndAccountCodes", request);
        }

        public Task<AdvanceStatementBalanceDocumentDto[]> GetAdvanceStatementBalanceDocumentsAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<AdvanceStatementBalanceDocumentDto[]>($"/Balances/GetAdvanceStatementBalanceDocuments?firmId={firmId}&userId={userId}");
        }
    }
}
