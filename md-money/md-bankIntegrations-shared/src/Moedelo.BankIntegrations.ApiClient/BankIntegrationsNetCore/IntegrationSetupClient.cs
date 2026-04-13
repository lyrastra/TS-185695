using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moedelo.BankIntegrations.ApiClient.Abstractions.BankIntegrationsNetCore;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;
using Moedelo.BankIntegrations.Dto;
using Moedelo.Common.Audit.Abstractions.Interfaces;
using Moedelo.Common.Http.Abstractions;
using Moedelo.Common.Http.Abstractions.Headers;
using Moedelo.Common.Settings.Abstractions;
using Moedelo.Infrastructure.DependencyInjection.Abstractions;
using Moedelo.Infrastructure.Http.Abstractions.Interfaces;

namespace Moedelo.BankIntegrations.ApiClient.BankIntegrationsNetCore
{
    [InjectAsSingleton(typeof(IIntegrationSetupClient))]
    public class IntegrationSetupClient : BaseApiClient, IIntegrationSetupClient
    {
        public IntegrationSetupClient(
            IHttpRequestExecuter httpRequestExecuter,
            IUriCreator uriCreator,
            IAuditTracer auditTracer,
            IAuthHeadersGetter authHeadersGetter,
            IAuditHeadersGetter auditHeadersGetter,
            ISettingRepository settingRepository,
            ILogger<IntegrationSetupClient> logger)
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
        
        public async Task<IntegrationSetupResponseDto> RunEnableAsync(IntegrationEnableSetupRequestDto dto)
        {
            var result =
                await PostAsync<IntegrationEnableSetupRequestDto,
                    ApiDataResult<IntegrationSetupResponseDto>>(
                    "/private/api/v1/IntegrationSetup/RunEnable", dto);

            return result.data;
        }
        
        public async Task<IntegrationDisableResponseDto> RunDisableAsync(IntegrationDisableRequestDto dto)
        {
            var result =
                await PostAsync<IntegrationDisableRequestDto,
                    ApiDataResult<IntegrationDisableResponseDto>>(
                    "/private/api/v1/IntegrationSetup/RunDisable", dto);

            return result.data;
        }
    }
}