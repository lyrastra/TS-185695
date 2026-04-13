using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto.FmsRegistry;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.OutSystemsIntegrationV2.Client.FmsRegistry
{
    [InjectAsSingleton]
    public class FmsRegistryClient : BaseApiClient, IFmsRegistryClient
    {
        private readonly SettingValue apiEndpoint;
        
        public FmsRegistryClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("FmsRegistryApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/V2";
        }
        
        public Task<CheckIsBlockedPassportResponseDto> CheckIsBlockedPassportAsync(CheckIsBlockedPassportRequestDto request)
        {
            return PostAsync<CheckIsBlockedPassportRequestDto, CheckIsBlockedPassportResponseDto>("/CheckBlockedPassport", request);
        }
    }
}