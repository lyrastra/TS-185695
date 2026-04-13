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
    [InjectAsSingleton(typeof(IOperationLogApiClient))]
    internal sealed class OperationLogApiClient : BaseLegacyApiClient, IOperationLogApiClient
    {
        public OperationLogApiClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<OperationLogApiClient> logger)
            : base(
                httpRequestExecuter,
                uriCreator,
                auditTracer,
                auditHeadersGetter,
                settingRepository.Get("HistoricalLogsApiEndpoint"),
                logger)
        {
        }

        public Task LogAsync(LogOperationDto data)
        {
            return PostAsync("/Operations/LogAsync", data);
        }
    }
}