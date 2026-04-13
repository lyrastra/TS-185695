using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.TerritorialCondition;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(ITerritorialConditionApiClient))]
    internal sealed class TerritorialConditionApiClient : BaseLegacyApiClient, ITerritorialConditionApiClient
    {
        public TerritorialConditionApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<UnboundPaymentsApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<IReadOnlyCollection<WorkerTerritorialConditionOnDateResponseDto>> GetByWorkersOnDateAsync(
            int firmId, int userId, IReadOnlyCollection<TerritorialConditionOnDateRequestDto> request)
        {
            return PostAsync<IReadOnlyCollection<TerritorialConditionOnDateRequestDto>,
                IReadOnlyCollection<WorkerTerritorialConditionOnDateResponseDto>>(
                $"/TerritorialCondition/GetByWorkersOnDate?firmId={firmId}&userId={userId}", request);
        }
    }
}