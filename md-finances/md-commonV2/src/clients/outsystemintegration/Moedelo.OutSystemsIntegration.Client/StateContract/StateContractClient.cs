using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.StateContract;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.OutSystemsIntegrationV2.Client.StateContract
{
    [InjectAsSingleton]
    public class StateContractClient : BaseApiClient, IStateContractClient
    {
        private readonly SettingValue apiEndpoint;
        
        public StateContractClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("StateContractApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/V2";
        }
        
        public Task<StateContractResponseDto> GetContractsAsync(StateContractsRequestDto request)
        {
            return GetAsync<StateContractResponseDto>("/Get", request);
        }
    }
}