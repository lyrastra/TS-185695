using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationDisableDetails;
using Moedelo.BankIntegrations.ApiClient.Dto.IntegrationSetup;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.IntegrationDisableDetails;
using Moedelo.BankIntegrations.Dto;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.IntegrationDisableDetails
{
    [InjectAsSingleton(typeof(IIntegrationDisableDetailsClient))]
    public class IntegrationDisableDetailsClient : BaseCoreApiClient, IIntegrationDisableDetailsClient
    {
        private readonly SettingValue endpoint;
        
        public IntegrationDisableDetailsClient(
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
        
        public async Task<List<IntegrationDisableDetailsResponseDto>> GetByIntegrationRequestIdsAsync(IntegrationDisableDetailsRequestDto dto)
        {
            var result = await PostAsync<IntegrationDisableDetailsRequestDto, ApiDataResult<List<IntegrationDisableDetailsResponseDto>>>(
                    "/private/api/v1/IntegrationDisableDetails/GetByIntegrationRequestIds", dto).ConfigureAwait(false);
            return result.data;
        }
    }
}