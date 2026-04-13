using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy;
using Moedelo.HistoricalLogs.ApiClient.Abstractions.Legacy.Dto;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.Common.Http.Abstractions.Headers;

namespace Moedelo.HistoricalLogs.ApiClient.Legacy
{
    [InjectAsSingleton(typeof(IOutsourceCalendarLogApiClient))]
    internal sealed class OutsourceCalendarLogApiClient : BaseLegacyApiClient, IOutsourceCalendarLogApiClient
    {
        public OutsourceCalendarLogApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<OutsourceCalendarLogApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("HistoricalLogsApiEndpoint"),
                logger)
        {
        }

        public Task LogAsync(OutsourceCalendarLogDto data)
        {
            return PostAsync("/OutsourceCalendar/Log", data);
        }
    }
}