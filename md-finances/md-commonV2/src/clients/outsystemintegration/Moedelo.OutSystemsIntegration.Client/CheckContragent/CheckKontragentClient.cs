using System.Collections.Generic;
using System.Threading.Tasks;
using Moedelo.InfrastructureV2.Domain.AbstractClasses.ApiClient;
using Moedelo.InfrastructureV2.Domain.Attributes.Injection;
using Moedelo.InfrastructureV2.Domain.AuditModule.Interfaces;
using Moedelo.InfrastructureV2.Domain.Interfaces.ApiClient;
using Moedelo.InfrastructureV2.Domain.Interfaces.Setting;
using Moedelo.InfrastructureV2.Domain.Models.ApiClient;
using Moedelo.OutSystemsIntegrationV2.Dto;
using Moedelo.InfrastructureV2.Domain.Models.Setting;

namespace Moedelo.OutSystemsIntegrationV2.Client.CheckContragent
{
    [InjectAsSingleton]
    public class CheckKontragentClient : BaseApiClient, ICheckKontragentClient
    {
        private readonly SettingValue apiEndpoint;
        
        public CheckKontragentClient(IHttpRequestExecutor httpRequestExecutor, IUriCreator uriCreator, IResponseParser responseParser, IAuditTracer auditTracer, IAuditScopeManager auditScopeManager, ISettingRepository settingRepository)
            : base(httpRequestExecutor, uriCreator, responseParser, auditTracer, auditScopeManager)
        {
            apiEndpoint = settingRepository.Get("CheckKontragentApiEndpoint");
        }

        protected override string GetApiEndpoint()
        {
            return apiEndpoint.Value + "/V2";
        }
        
        public Task<List<CheckKontragentResponseDto>> CheckAsync(IList<CheckKontragentRequestDto> requestDto)
        {
            return PostAsync<IList<CheckKontragentRequestDto>, List<CheckKontragentResponseDto>>("/CheckList", requestDto);
        }

        public Task<CheckKontragentResponseDto> CheckAsync(CheckKontragentRequestDto requestDto)
        {
            return PostAsync<CheckKontragentRequestDto, CheckKontragentResponseDto>("/Check", requestDto);
        }
        
        public Task UpdateKontragentStatusAsync(List<CheckKontragentRequestDto> requestDtoList)
        {
            return PostAsync<List<CheckKontragentRequestDto>, List<CheckKontragentResponseDto>>("/UpdateKontragentStatus", requestDtoList);
        }
    }
}