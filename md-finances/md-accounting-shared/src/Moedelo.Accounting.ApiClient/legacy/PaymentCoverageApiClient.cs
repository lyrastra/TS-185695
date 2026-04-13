using System;
using System.Collections.Generic;
using System.Threading;
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
using Moedelo.Infrastructure.Http.Abstractions.Models;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IPaymentCoverageApiClient))]
    internal sealed class PaymentCoverageApiClient : BaseLegacyApiClient, IPaymentCoverageApiClient
    {
        private static readonly HttpQuerySetting DefaultSettingValue = new HttpQuerySetting(TimeSpan.FromSeconds(30));
        
        public PaymentCoverageApiClient(
            IHttpRequestExecuter httpRequestExecutor,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<PaymentCoverageApiClient> logger)
            : base(
                httpRequestExecutor,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<IReadOnlyList<PaymentCoverageSummaryDto>> GetSummaryAsync(
            PaymentCoverageSummaryRequestDto dto,
            HttpQuerySetting httpQuerySetting = null,
            CancellationToken ct = default) =>
            PostAsync<PaymentCoverageSummaryRequestDto, IReadOnlyList<PaymentCoverageSummaryDto>>(
            $"/PaymentCoverage/GetSummary", dto, setting: httpQuerySetting ?? DefaultSettingValue, cancellationToken: ct);
    }
}