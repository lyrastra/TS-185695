using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(ISettlementSummaryApiClient))]
    internal sealed class SettlementSummaryApiClient : BaseLegacyApiClient, ISettlementSummaryApiClient
    {
        public SettlementSummaryApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<SettlementSummaryApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<List<SettlementBalanceDto>> GetAsync(int firmId, int userId, DateTime? onDate)
        {
            var uri = $"/SettlementSummary/Get?firmId={firmId}&userId={userId}&onDate={onDate}";
            return GetAsync<List<SettlementBalanceDto>>(uri);
        }
    }
}