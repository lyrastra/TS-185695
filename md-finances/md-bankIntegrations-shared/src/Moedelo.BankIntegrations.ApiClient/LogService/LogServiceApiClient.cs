using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Dto.LogService;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.LogService;

[InjectAsSingleton(typeof(ILogServiceApiClient))]
public class LogServiceApiClient : BaseApiClient, ILogServiceApiClient
{
    private const string ControllerName = "/private/api/v1/integration/log";

    public LogServiceApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<LogServiceApiClient> logger)
        : base(
            httpRequestExecuter,
            uriCreator,
            auditTracer,
            authHeadersGetter,
            auditHeadersGetter,
            settingRepository.Get("IntegrationApiNetCore"),
            logger)
    {
    }

    public Task<string> AppendAsync(AppendLogRequestDto requestDto, CancellationToken cancellationToken)
    {
        return PostAsync<AppendLogRequestDto, string>($"{ControllerName}/Append", requestDto, cancellationToken: cancellationToken);
    }
}
