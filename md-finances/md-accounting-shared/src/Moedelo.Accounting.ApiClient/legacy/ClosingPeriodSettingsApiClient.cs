using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Accounting.ApiClient.Abstractions.legacy;
using Moedelo.Accounting.ApiClient.Abstractions.legacy.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.Accounting.ApiClient.legacy
{
    [InjectAsSingleton(typeof(IClosingPeriodSettingsApiClient))]
    internal sealed class ClosingPeriodSettingsApiClient : BaseLegacyApiClient, IClosingPeriodSettingsApiClient
    {
        public ClosingPeriodSettingsApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ClosedPeriodApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("AccountingApiEndpoint"),
                logger)
        {
        }

        public Task<ClosingPeriodSettingsDto> GetAsync(FirmId firmId, UserId userId)
        {
            return GetAsync<ClosingPeriodSettingsDto>("/ClosingPeriodSettings", new { firmId, userId });
        }

        public Task<IReadOnlyDictionary<int, int?>> GetByFirmIdsAsync(
            IReadOnlyCollection<int> firmIds)
        {
            return PostAsync<IReadOnlyCollection<int>, IReadOnlyDictionary<int, int?>>(
                "/ClosingPeriodSettingsSummary/GetByFirmIds", firmIds);
        }
    }
}