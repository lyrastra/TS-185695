using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.IntegrationErrorLog;
using Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationErrorLogs;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.IntegrationErrorLog;

[InjectAsSingleton(typeof(IIntegrationErrorLogApiClient))]
public class IntegrationErrorLogApiClient : BaseApiClient, IIntegrationErrorLogApiClient
{
    private const string ControllerName = "/private/api/v1/integrationErrorLog";
    
    public IntegrationErrorLogApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<IntegrationErrorLogApiClient> logger)
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
    
    public async Task CreateAsync(IntegrationErrorLogDto dto)
    {
        await PostAsync($"{ControllerName}/Create", dto);
    }
}