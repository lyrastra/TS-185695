using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Finances.ApiClient.Abstractions.Legacy;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Finances.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IMoneyClosedPeriodsApiClient))]
    internal sealed class MoneyClosedPeriodsApiClient : BaseLegacyApiClient, IMoneyClosedPeriodsApiClient
    {
        public MoneyClosedPeriodsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<MoneyClosedPeriodsApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("FinancesPrivateApiEndpoint"),
                logger)
        {
        }

        public async Task<DateTime> GetLastClosedDateAsync(FirmId firmId, UserId userId)
        {
            var response = await GetAsync<string>($"/ClosedPeriods/LastClosedDate?firmId={firmId}&userId={userId}")
                .ConfigureAwait(false);

            return response.ToIsoDateTime();
        }
    }
}