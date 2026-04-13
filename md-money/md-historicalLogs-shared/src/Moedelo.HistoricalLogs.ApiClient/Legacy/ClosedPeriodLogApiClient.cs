using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Common.Settings.Abstractions.Models;
using Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy;
using Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.HistoricalLogs.Enums;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.HistoricalLogs.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IClosedPeriodLogApiClient))]
    public class ClosedPeriodLogApiClient : BaseLegacyApiClient, IClosedPeriodLogApiClient
    {
        public ClosedPeriodLogApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<ClosedPeriodLogApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("HistoricalLogsApiEndpoint"),
                logger)
        {}
        
        public Task<ClosedPeriodPageResponseDto> GetByCriteriaAsync(ClosedPeriodLogGetDto dto)
        {
            return PostAsync<ClosedPeriodLogGetDto,ClosedPeriodPageResponseDto>("/ClosedPeriodLog/GetBy", dto);
        }
    }
}
