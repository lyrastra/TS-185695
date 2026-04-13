using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.BankIntegrations.ApiClient.Dto.UserIntegrationInfos;
using Moedelo.BankIntegrations.ApiClient.Framework.Abstractions.UserIntegrationInfo;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.BankIntegrations.ApiClient.Framework.UserIntegrationInfo
{
    [InjectAsSingleton(typeof(IUserIntegrationInfoApiClient))]
    public class UserIntegrationInfoApiClient : BaseCoreApiClient, IUserIntegrationInfoApiClient
    {
        private readonly SettingValue endpoint;
        
        public UserIntegrationInfoApiClient(
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

        public async Task<List<ConnectIntegrationStateResponseDto>> GetConnectIntegrationState(ConnectIntegrationStateRequestDto dto)
        {
            var response = await PostAsync<ConnectIntegrationStateRequestDto, List<ConnectIntegrationStateResponseDto>>(
                    "/private/api/v1/UserIntegrationInfo/GetConnectIntegrationState", dto)
                .ConfigureAwait(false);
            return response;
        }
        
        public async Task<UserIntegrationInfoDto> GetDataAsync(int firmId, int userId)
        {
            var queryParams = new { firmId, userId };
            var response = await GetAsync<UserIntegrationInfoDto>(
                    $"/private/api/v1/UserIntegrationInfo", queryParams: queryParams)
                .ConfigureAwait(false);
            return response;
        }
        
        public async Task<UserIntegrationInfoToRequisitesDto> GetDataToRequisitesAsync(int firmId, int userId)
        {
            var queryParams = new { firmId, userId };
            var response = await GetAsync<UserIntegrationInfoToRequisitesDto>(
                    $"/private/api/v1/UserIntegrationInfo/GetDataToRequisites", queryParams: queryParams)
                .ConfigureAwait(false);
            return response;
        }
    }
}