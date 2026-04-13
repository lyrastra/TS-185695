using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Types;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy;
using Moedelo.Payroll.ApiClient.Abstractions.Legacy.Dto.ChargeGeneration.InitialData;

namespace Moedelo.Payroll.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IChargeGenerationApiClient))]
    internal sealed class ChargeGenerationApiClient : BaseLegacyApiClient, IChargeGenerationApiClient
    {
        public ChargeGenerationApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ChargeGenerationApiClient> logger)
            : base(httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("PayrollPrivateApi"),
                logger)
        {
        }

        public Task<ChargeGenerationInitialDataDto> GetInitialDataAsync(FirmId firmId, UserId userId,
            CancellationToken token = default)
        {
            return GetAsync<ChargeGenerationInitialDataDto>("/ChargeGeneration/InitialData", new { firmId, userId },
                cancellationToken: token);
        }
    }
}