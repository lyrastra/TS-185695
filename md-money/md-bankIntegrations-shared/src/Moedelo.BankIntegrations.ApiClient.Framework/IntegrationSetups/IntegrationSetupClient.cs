using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationSetups;
using Moedelo.BankIntegrations.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationSetups
{
    [InjectAsSingleton(typeof(IIntegrationSetupClient))]
    public class IntegrationSetupClient : BaseCoreApiClient, IIntegrationSetupClient
    {
        private readonly SettingValue endpoint;
        
        public IntegrationSetupClient(
            IHttpRequestExecutor httpRequestExecutor,
            IUriCreator uriCreator,
            IResponseParser responseParser,
            ISettingRepository settingRepository,
            IAuditTracer auditTracer,
            IAuditScopeManager auditScopeManager)
            : base(
                httpRequestExecutor,
                uriCreator,
                responseParser,
                settingRepository,
                auditTracer,
                auditScopeManager)
        {
            this.endpoint = settingRepository.Get("IntegrationApiNetCore");
        }
        
        protected override string GetApiEndpoint()
        {
            return endpoint.Value;
        }
        
        public async Task<IntegrationDisableResponseDto> RunDisableAsync(IntegrationDisableRequestDto dto)
        {
            var result =
                await PostAsync<IntegrationDisableRequestDto,
                    ApiDataResult<IntegrationDisableResponseDto>>(
                    "/private/api/v1/IntegrationSetup/RunDisable", dto).ConfigureAwait(false);

            return result.data;
        }
    }
}