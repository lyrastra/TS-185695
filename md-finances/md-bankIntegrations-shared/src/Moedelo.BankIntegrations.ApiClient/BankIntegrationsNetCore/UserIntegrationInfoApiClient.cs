using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore;

[InjectAsSingleton(typeof(IUserIntegrationInfoApiClient))]
public class UserIntegrationInfoApiClient : BaseApiClient, IUserIntegrationInfoApiClient
{
    public UserIntegrationInfoApiClient(
        IHttpRequestExecuter httpRequestExecuter,
        IUriCreator uriCreator,
        IAuditTracer auditTracer,
        IAuthHeadersGetter authHeadersGetter,
        IAuditHeadersGetter auditHeadersGetter,
        ISettingRepository settingRepository,
        ILogger<UserIntegrationInfoApiClient> logger)
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
        
    public async Task<UserIntegrationInfoDto> GetDataAsync(int firmId, int userId)
    {
        var queryParams = new { firmId, userId };
        var response = await GetAsync<UserIntegrationInfoDto>("/private/api/v1/UserIntegrationInfo", queryParams: queryParams);
        return response;
    }
    
    public async Task<UserIntegrationInfoToRequisitesDto> GetDataToMobileRequisitesAsync(int firmId, int userId)
    {
        var queryParams = new { firmId, userId };
        var response = await GetAsync<UserIntegrationInfoToRequisitesDto>("/private/api/v1/UserIntegrationInfo/GetDataToMobileRequisites", queryParams: queryParams);
        return response;
    }
}