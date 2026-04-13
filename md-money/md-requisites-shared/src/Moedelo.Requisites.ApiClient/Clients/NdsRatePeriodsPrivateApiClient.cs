using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Dto;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using Moedelo.Requisites.ApiClient.Abstractions.Clients;
using Moedelo.Requisites.ApiClient.Abstractions.Clients.Dto;

namespace Moedelo.Requisites.ApiClient.Clients
{
    [InjectAsSingleton(typeof(INdsRatePeriodsPrivateApiClient))]
    public class NdsRatePeriodsPrivateApiClient: BaseApiClient, INdsRatePeriodsPrivateApiClient
    {
        public NdsRatePeriodsPrivateApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<NdsRatePeriodsPrivateApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                authHeadersGetter,
                auditHeadersGetter,
                settingRepository.Get("RequisitesHandlerEndpoint"),
                logger)
        {
        }

        public async Task<NdsRatePeriodByFirmIdDto[]> GetByFirmIdsFilterAsync(NdsRatePeriodFilterByFirmIdsDto filterDto, CancellationToken ct = default)
        {
            if (filterDto?.FirmIds == null || filterDto.FirmIds.Count == 0)
            {
                return [];
            }

            var result = await PostAsync<NdsRatePeriodFilterByFirmIdsDto, ApiDataDto<NdsRatePeriodByFirmIdDto[]>>(
                "/private/api/v1/NdsRatePeriods/GetByFirmIdsFilter", 
                filterDto,
                cancellationToken: ct);
            
            return result.data;
        }
    }
}